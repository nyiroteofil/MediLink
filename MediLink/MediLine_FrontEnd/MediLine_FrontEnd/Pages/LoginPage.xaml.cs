using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;

namespace MediLine_FrontEnd.Pages;

public partial class LoginPage : ContentPage
{

    private readonly ApiHandler _apiHandler;

    public LoginPage(ApiHandler apiHandler)
    {
        InitializeComponent();
        _apiHandler = apiHandler;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        string token = await SecureStorage.GetAsync("jwt_token");
        if (token != null && !IsTokenExpired(token))
        {
            await _apiHandler.AttachToken();
            await Shell.Current.GoToAsync("//main");
        }
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        LoginDTO loginDTO = new LoginDTO {
            Username = UsernameEntry.Text,
            Password = PasswordEntry.Text,
        };

        if (string.IsNullOrWhiteSpace(loginDTO.Username) || string.IsNullOrWhiteSpace(loginDTO.Password))
        {
            await DisplayAlert("Error", "Please enter username and password", "OK");
            return;
        }

        try
        {
            bool loginStatus = await _apiHandler.LoginUser(loginDTO);

            if (loginStatus)
            {
                // Navigate to main page
                await Shell.Current.GoToAsync("//main");
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Connection failed: {ex.Message}", "OK");
        }
    }
}