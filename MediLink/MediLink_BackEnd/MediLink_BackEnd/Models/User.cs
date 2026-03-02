namespace MediLink_BackEnd.Models
{

    public enum UserStatus
    {
        Active,
        Inactive,
        Suspended,
        DataInputError,
        Unknown,
    }

    public class ContactInfo
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public abstract class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserStatus Status { get; set; }
        public ContactInfo ContactInfo { get; set; }

        public List<Events> Events { get; set; }

    }

    public class  Patient : User
    {
        public List<Diagnosis> Dignoses { get; set; }
        public PatientDataSheet DataSheet { get; set; }
        public List<SpecialistDoctor> SpecialistDoctors { get; set; }
        public List<AppointmentRequest> Requests { get; set; }
        public List<Referal> referals { get; set; }
    }

    public class SpecialistDoctor : User
    {
        public MedicalStaffDataSheet DataSheet { get; set; }
        public List<Patient> Patients { get; set; }
        public List<AppointmentRequest> PendingAppointments { get; set; }
        public List<Event> Events { get; set; }
    }
     
    public class MedicalAsisstant
    {
        public SpecialistDoctor AttendingDoctor { get; set; }
        public MedicalStaffDataSheet DataSheet { get; set; }
    }
}
