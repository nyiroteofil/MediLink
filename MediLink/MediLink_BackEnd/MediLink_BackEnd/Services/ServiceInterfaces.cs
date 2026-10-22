using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;

namespace MediLink_BackEnd.Services;

public interface IUserService
    {
        Task<Patient> CreatePatientUser(PatientDTO dto);
        Task<SpecialistDoctor> CreateDoctorUser(StaffUserDTO dto);
        Task<MedicalAssistant> CreateMedicalAssistantUser(StaffUserDTO dto, List<SpecialistDoctor> attendingDoctors);
        Task<Administrator> CreateAdministrator(StaffUserDTO dto);
        Task<string> UserLogIn(LoginDTO dto);
        Task<List<UserSummaryDTO>> GetAllActiveDoctors();
        Task<List<UserSummaryDTO>> GetAllActivePatients();
        Task<List<UserSummaryDTO>> GetAllActiveAssistants();
        public Task<int> GetDocAppointmentsWeekAhead(int docID);
        public Task<DateTime?> GetNextAppointmentDate(int suerID);
        public Task<int> DocActivePatientNumber(int docID);
    }
    
    public interface IAppointmentService
    {
        public Task<AppointmentRequest> RequestAppointment(AppointmentRequestDTO dto);
        public Task<List<AppointmentRequestResponseDTO>> GetPatientsAppointment(int id);
        public Task<List<AppointmentRequestResponseDTO>> GetDoctorsAppointment(int id);
        public Task<List<AppointmentRequestResponseDTO>> GetPendingAppointmentRequests(int userID);
    }
    
    public interface IAdministratorService
    {
        public Task<List<UserSummaryDTO>> GetAllSuspendedPatients();
        public Task<List<UserSummaryDTO>> GetAllSuspendedDoctors();
        public Task<List<UserSummaryDTO>> GetAllSuspendedAssistants();
        public Task<List<UserSummaryDTO>> GetAllSuspendedAdministrators();
        Task<List<UserSummaryDTO>> GetAllActiveAdministrators();
        Task<bool> ActivateUser(int userID);
        Task<bool> SuspendUser(int userID);
    }
    
    public interface IMessageService
    {
        public Task<Message> PostMessage(Message message);
        public Task<List<Message>> GetMessagesFromChat(int chatID);
        public Task<Chat> CreateChat(int docID, int patientID);
        public Task<List<Chat>> GetUserChats(int userID);
    }
    
    public interface IInstitutionService
    {
        public Task<Institution> AddInstiution(Institution institution);
    }