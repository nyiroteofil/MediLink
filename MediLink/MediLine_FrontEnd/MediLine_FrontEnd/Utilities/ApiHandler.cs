using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediLine_FrontEnd.Utilities
{
    public class ApiHandler
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiHandler> _logger;

        public ApiHandler(ILogger<ApiHandler> loggerUtil)
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
            });
            _logger = loggerUtil;

        }

        public async Task<bool> LoginUser(LoginDTO dto)
        {
            HttpResponseMessage res = await _httpClient.PostAsync($"{Environment.GetEnvironmentVariable("MEDILINK_URL")}/Users/LogIn", JsonContent.Create(dto));

            if (res.IsSuccessStatusCode)
            {
                await SaveToken(res);
                await AttachToken();

                return true;
            }

            return false;
        }

        public async Task SaveToken(HttpResponseMessage res)
        {
            string token = await res.Content.ReadAsStringAsync();

            await SecureStorage.SetAsync("jwt_token", token);
        }

        public async Task<string> FetchToken()
        {
            string token = await SecureStorage.GetAsync("jwt_token");

            if (token == null) return null;

            return token;
        }

        // This function attaches a header value that will be there with ever http
        // request made through this instance (_httpClinet).
        public async Task AttachToken()
        {
            string token = await FetchToken();
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task RemoveToken()
        {
            SecureStorage.Remove("jwt_token");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> GetUserRole()
        {
            string tokenString = await SecureStorage.GetAsync("jwt_token");

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(tokenString);
            string role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == null) return null;

            return role;
        }

        public async Task<int> GetUserID()
        {

            try
            {
                string tokenString = await SecureStorage.GetAsync("jwt_token");

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.ReadJwtToken(tokenString);

                string idString = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                // we try to parse the value. If it's null it'll throw an error and we catch it.
                if (int.TryParse(idString, out int id))
                {
                    return id;
                }
                else return -1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get user ID from token in ApiHandler");
                return -1;
            }


        }

    }
}
