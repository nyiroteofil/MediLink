using System.Data;
using MediLink_BackEnd.Data; 
using MediLink_BackEnd.UtilityClasses;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

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
                _logger.LogError(ex, "Exception while trying to save Medical Assistant ({User})", assistantUser);
                throw;
            }

            return assistantUser;
        }

        
        /*
         * The function of this method is update one or more properties of the specific user's class
         * the main question is how to go about this. If only one or a few and not all properties are changed
         * means that there are null records in the dto. How shall I put that into the persistent DB record?
         * 1. option:
         *      I should fetch the users information on the fornt end, put it into the DTO with the modified
         *      fields and send it back, before all the fields get to be UPDATED. This way the data that were not
         *      meant to be updated stays unchanged and the intended change will be applied.
         * 2. option:
         *      I find a way to check what are the fields that the user wants to change and only assign value to those
         *      (unlike in solution #1, as you assign even the unchanged values after fetching them.)
         *      This seems the far more efficient way to do this, but I'm not sure how to check for the correct property.
         *      I cannot iterate through properties and using 20+ if-else statements would constitute to horrible code.
         */
        public async Task<PatientUpdateDTO> PatchPatientUser(Patient trackedEntity, PatientUpdateDTO dto)
        {
            
            // A series of checks for valid inputs inside DTO
            if (!Regex.IsMatch(dto.Email, @"^([a-z]|[0-9])([a-z]|[0-9]|[_.])*@([a-z]|[0-9]|[_.])*\.[a-z]{2,4}$") ||
                !Regex.IsMatch(dto.PhoneNumber, @"^(\+36|06)[0-9]{2}[0-6]{6}$")) throw new InvalidExpressionException("Invalid String Format");

            try
            {
                trackedEntity.Username = dto.Username;
                trackedEntity.DataSheet.Address = dto.Address;
                trackedEntity.ContactInfo.Email = dto.Email;
                trackedEntity.ContactInfo.PhoneNumber = dto.PhoneNumber;
                
                /*
                 * An interesting observation as a learner, is that the entity, although passed by reference
                 * is tracked properly due to the shared and properly scoped MediLinkContext
                 */
                await _dbContext.SaveChangesAsync();
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while trying to patch Patient User ({Exception})", ex);
                throw;
            }
        }
        
        public async Task<List<UserSummaryDTO>> GetAllActiveAssistants()
        {
            try
            {
                List<MedicalAssistant> assistants = await _dbContext.MedicalAsisstants
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Active)
                    .ToListAsync(); ;
                List<UserSummaryDTO> DTOs = new List<UserSummaryDTO>();

                foreach (MedicalAssistant assistant in assistants)
                {
                    DTOs.Add(new UserSummaryDTO
                    {
                        ID = assistant.ID,
                        Username = assistant.Username,
                        FullName = $"{assistant.DataSheet.FirstName} {assistant.DataSheet.LastName}",
                        Role = assistant.Role,
                        Status = assistant.Status,
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
