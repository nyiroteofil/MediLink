using MediLink_BackEnd.Data;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediLink_BackEnd.Services
{
    public interface IUserService
    {
        Task<Patient> CreatePatientUser(PatientDTO dto);
        Task<SpecialistDoctor> CreateDoctorUser(StaffUserDTO dto);
        Task<string> UserLogIn(LoginDTO dto);
        Task<Administrator> CreateAdministrator(StaffUserDTO dto);
        Task<bool> ActivateUser(int userID);
        Task<bool> SuspendUser(int userID);
    }

    public class UserService : IUserService
    {
        private readonly MediLinkContext _dbContext;
        private readonly IConfiguration _config;

        public UserService(MediLinkContext context, IConfiguration config)
        {
            _dbContext = context;
            _config = config;
        }

        public async Task<Administrator> CreateAdministrator(StaffUserDTO dto)
        {
            Administrator admin = new Administrator
            {
                Username = dto.Username,
                PasswordHash = PwdEncryptionHelper.GetHashedPassword(dto.PasswordHash, out string salt),
                PasswordSalt = salt,
                Status = UserStatus.Active,
                Role = UserRole.Administrator,
                ContactInfo = new ContactInfo 
                { 
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber
                },
                DataSheet =  new MedicalStaffDataSheet
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    EmployeeID = dto.EmployeeID,
                    Position = dto.Position,
                    InstitutionID = dto.InstitutionID,
                    DateOfBirth = dto.DateOfBirth,
                    Sex = dto.Sex,
                    Address = dto.Address
                }
            };

            try
            {
                _dbContext.Administrators.Add(admin);
                await _dbContext.SaveChangesAsync();

                return admin;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        async public Task<Patient> CreatePatientUser(PatientDTO dto)
        {

            string passwordHash = PwdEncryptionHelper.GetHashedPassword(dto.PasswordHash, out string salt);

            // transfering data from DTO
            var patient = new Patient
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                Status = UserStatus.Active,
                Role = UserRole.Patient,
                ContactInfo = new ContactInfo
                {
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email
                },
                DataSheet = new PatientDataSheet
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    TAJNumber = dto.TAJNumber,
                    DateOfBirth = dto.DateOfBirth,
                    Sex = dto.Sex,
                    Address = dto.Address
                }
            };

            try
            {
                _dbContext.Patients.Add(patient);
                await _dbContext.SaveChangesAsync();

                return patient;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        async public Task<SpecialistDoctor> CreateDoctorUser(StaffUserDTO dto)
        {
            // getting the securely hashed password
            string passwordHash = PwdEncryptionHelper.GetHashedPassword(dto.PasswordHash, out string salt);

            // transfering data from DTO
            var specialistDoctor = new SpecialistDoctor
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                PasswordSalt= salt,
                Status = UserStatus.Active,
                Role = UserRole.SpecialistDoctor,
                ContactInfo = new ContactInfo
                {
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email
                },
                DataSheet = new MedicalStaffDataSheet
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    EmployeeID = dto.EmployeeID,
                    Position = dto.Position,
                    InstitutionID = dto.InstitutionID,
                    DateOfBirth = dto.DateOfBirth,
                    Sex = dto.Sex,
                    Address = dto.Address
                }
            };

            try
            {
                _dbContext.SpecialistDoctors.Add(specialistDoctor);
                await _dbContext.SaveChangesAsync();

                return specialistDoctor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<bool> ActivateUser(int userID)
        {
            User user;

            try
            {
                user = await _dbContext.Users.Where(u => u.ID == userID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            if (user != null)
            {
                user.Status = UserStatus.Active;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }


        public async Task<bool> SuspendUser(int userID)
        {
            User user;

            try
            {
                user = await _dbContext.Users.Where(u => u.ID == userID).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            if (user != null)
            {
                user.Status = UserStatus.Suspended;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        async public Task<string> UserLogIn(LoginDTO dto)
        {

            // Validate user's username and password
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null || !PwdEncryptionHelper.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt)) return null;


            // Check if user's account is not suspended or inactive
            if (user.Status == UserStatus.Suspended)
                throw new UnauthorizedAccessException("Account is suspended.");

            if (user.Status != UserStatus.Active)
                throw new UnauthorizedAccessException("Account is not active.");

            string token = CreateJWTToken(user);

            return token;

        }

        private string CreateJWTToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            // Creating the encrypted signiture for our token
            SigningCredentials signedCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signedCredential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
