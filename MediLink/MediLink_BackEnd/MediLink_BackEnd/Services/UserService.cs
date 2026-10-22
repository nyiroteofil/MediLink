using MediLink_BackEnd.Data; 
using MediLink_BackEnd.UtilityClasses;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediLink_BackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly MediLinkContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<UserService> _logger;

        public UserService(MediLinkContext context, IConfiguration config, ILogger<UserService> logger)
        {
            _dbContext = context;
            _config = config;
            _logger = logger;
        }

        public async Task<Administrator> CreateAdministrator(StaffUserDTO dto)
        {
            Administrator admin = new Administrator
            {
                Username = dto.Username,
                Password = PwdEncryptionHelper.GetHashedPassword(dto.PasswordHash, out string salt),
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

        public async Task<Patient> CreatePatientUser(PatientDTO dto)
        {

            string passwordHash = PwdEncryptionHelper.GetHashedPassword(dto.PasswordHash, out string salt);

            // transfering data from DTO
            var patient = new Patient
            {
                Username = dto.Username,
                Password = passwordHash,
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

        public async Task<SpecialistDoctor> CreateDoctorUser(StaffUserDTO dto)
        {
            // getting the securely hashed password
            string passwordHash = PwdEncryptionHelper.GetHashedPassword(dto.PasswordHash, out string salt);

            // transfering data from DTO
            var specialistDoctor = new SpecialistDoctor
            {
                Username = dto.Username,
                Password = passwordHash,
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

        public async Task<MedicalAssistant> CreateMedicalAssistantUser(StaffUserDTO dto, List<SpecialistDoctor> attendingDoctors)
        {
            // getting the securely hashed password
            string passwordHash = PwdEncryptionHelper.GetHashedPassword(dto.PasswordHash, out string salt);

            // transferring data from DTO
            MedicalAssistant assistantUser = new MedicalAssistant()
            {
                Username = dto.Username,
                Password = passwordHash,
                PasswordSalt= salt,
                Status = UserStatus.Active,
                Role = UserRole.MedicalAssistant,
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
                },
                AttendingDoctors = attendingDoctors
            };

            try
            {
                await _dbContext.MedicalAsisstants.AddAsync(assistantUser);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while trying to save Medical Assistant", assistantUser);
                throw;
            }

            return assistantUser;
        }
        
        public async Task<List<UserSummaryDTO>> GetAllActiveAssistants()
        {
            try
            {
                List<MedicalAssistant> asistants = await _dbContext.MedicalAsisstants
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Active)
                    .ToListAsync(); ;
                List<UserSummaryDTO> DTOs = new List<UserSummaryDTO>();

                foreach (MedicalAssistant asistant in asistants)
                {
                    DTOs.Add(new UserSummaryDTO
                    {
                        ID = asistant.ID,
                        Username = asistant.Username,
                        FullName = $"{asistant.DataSheet.FirstName} {asistant.DataSheet.LastName}",
                        Role = asistant.Role,
                        Status = asistant.Status,
                    });
                }

                return DTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<UserSummaryDTO>();
            }
        }

        public async Task<List<UserSummaryDTO>> GetAllActiveDoctors()
        {
            try
            {
                List<SpecialistDoctor> doctors = await _dbContext.SpecialistDoctors
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Active)
                    .ToListAsync(); ;
                List<UserSummaryDTO> DTOs = new List<UserSummaryDTO>();

                foreach (SpecialistDoctor doctor in doctors)
                {
                    DTOs.Add(new UserSummaryDTO
                    {
                        ID = doctor.ID,
                        Username = doctor.Username,
                        FullName = $"{doctor.DataSheet.FirstName} {doctor.DataSheet.LastName}",
                        Role = doctor.Role,
                        Status = doctor.Status,
                    });
                }

                return DTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<UserSummaryDTO>();
            }
        }

        public async Task<List<UserSummaryDTO>> GetAllActivePatients()
        {
            try
            {
                List<Patient> patients = await _dbContext.Patients
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Active)
                    .ToListAsync(); ;
                List<UserSummaryDTO> DTOs = new List<UserSummaryDTO>();

                foreach (Patient patient in patients)
                {
                    DTOs.Add(new UserSummaryDTO
                    {
                        ID = patient.ID,
                        Username = patient.Username,
                        FullName = $"{patient.DataSheet.FirstName} {patient.DataSheet.LastName}",
                        Role = patient.Role,
                        Status = patient.Status,
                    });
                }

                return DTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<UserSummaryDTO>();
            }
        }

        async public Task<string> UserLogIn(LoginDTO dto)
        {

            // Validate user's username and password
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null || !PwdEncryptionHelper.VerifyPassword(dto.Password, user.Password, user.PasswordSalt)) return null;


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
        
        // Methods for providing info to users and front-end
        
        
        // gets the number of appointmetns a doctor has in the next 7 days
        public async Task<int> GetDocAppointmentsWeekAhead(int docID)
        {
            try
            {
                return await _dbContext.Appointments
                    .Where(a => a.SpecialistDoctorID == docID &&
                                a.StartTime >= DateTime.Now &&
                                a.StartTime <= DateTime.Now.AddDays(7))
                    .CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<DateTime?> GetNextAppointmentDate(int userID)
        {
            try
            {
                Appointment nextAppointment = await _dbContext.Appointments
                    .Where(a => (a.PatientID == userID || a.SpecialistDoctorID == userID) && a.StartTime >= DateTime.Now)
                    .OrderBy(a => a.StartTime)
                    .FirstOrDefaultAsync();

                return nextAppointment?.StartTime;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<int> DocActivePatientNumber(int docID)
        {
            try
            {
                int numOfActivePatients = await _dbContext.Patients
                    .Include(p => p.SpecialistDoctors)
                    .Include(p => p.Dignoses)
                    .CountAsync(p =>
                        p.SpecialistDoctors.Any(sp => sp.ID == docID) &&
                        p.Dignoses.Any(d =>
                            d.Status == DiagnosisStatus.Active ||
                            d.Status == DiagnosisStatus.Remission));

                return numOfActivePatients;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
