using MediLink_BackEnd.Data;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Services
{

    public interface IAdministratorService
    {
        public Task<List<UserSummaryDTO>> GetAllActivePatients();
        public Task<List<UserSummaryDTO>> GetAllActiveDoctors();
        public Task<List<UserSummaryDTO>> GetAllActiveAssistants();
        public Task<List<UserSummaryDTO>> GetAllActiveAdministrators();
        public Task<List<UserSummaryDTO>> GetAllSuspendedPatients();
        public Task<List<UserSummaryDTO>> GetAllSuspendedDoctors();
        public Task<List<UserSummaryDTO>> GetAllSuspendedAssistants();
        public Task<List<UserSummaryDTO>> GetAllSuspendedAdministrators();
    }

    public class AdministratorService : IAdministratorService
    {
        private readonly MediLinkContext _dbContext;

        public AdministratorService(MediLinkContext context)
        {
            _dbContext = context;
        }

        public async Task<List<UserSummaryDTO>> GetAllActiveAdministrators()
        {
            try
            {
                // Fetching admins from database with their respective ContactInfo field
                // and DataSheet
                List<Administrator> admins = await _dbContext.Administrators
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Active)
                    .ToListAsync(); ;
                List<UserSummaryDTO> DTOs = new List<UserSummaryDTO>();

                foreach (Administrator admin in admins)
                {
                    DTOs.Add(new UserSummaryDTO
                    {
                        ID = admin.ID,
                        Username = admin.Username,
                        FullName = $"{admin.DataSheet.FirstName} {admin.DataSheet.LastName}",
                        Role = admin.Role,
                        Status = admin.Status,
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

        // Pretty self explenatory
        public async Task<List<UserSummaryDTO>> GetAllActiveAssistants()
        {
            try
            {
                List<MedicalAsisstant> asistants = await _dbContext.MedicalAsisstants
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Active)
                    .ToListAsync(); ;
                List<UserSummaryDTO> DTOs = new List<UserSummaryDTO>();

                foreach (MedicalAsisstant asistant in asistants)
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

        public async Task<List<UserSummaryDTO>> GetAllSuspendedAdministrators()
        {
            try
            {
                List<Administrator> admins = await _dbContext.Administrators
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Suspended)
                    .ToListAsync(); ;
                List<UserSummaryDTO> DTOs = new List<UserSummaryDTO>();

                foreach (Administrator admin in admins)
                {
                    DTOs.Add(new UserSummaryDTO
                    {
                        ID = admin.ID,
                        Username = admin.Username,
                        FullName = $"{admin.DataSheet.FirstName} {admin.DataSheet.LastName}",
                        Role = admin.Role,
                        Status = admin.Status,
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

        public async Task<List<UserSummaryDTO>> GetAllSuspendedAssistants()
        {
            try
            {
                List<MedicalAsisstant> asistants = await _dbContext.MedicalAsisstants
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Suspended)
                    .ToListAsync(); ;
                List<UserSummaryDTO> DTOs = new List<UserSummaryDTO>();

                foreach (MedicalAsisstant asistant in asistants)
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

        public async Task<List<UserSummaryDTO>> GetAllSuspendedDoctors()
        {
            try
            {
                List<SpecialistDoctor> doctors = await _dbContext.SpecialistDoctors
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Suspended)
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

        public async Task<List<UserSummaryDTO>> GetAllSuspendedPatients()
        {
            try
            {
                List<Patient> patients = await _dbContext.Patients
                    .Include(a => a.ContactInfo)
                    .Include(a => a.DataSheet)
                    .Where(a => a.Status == UserStatus.Suspended)
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
    }
}
