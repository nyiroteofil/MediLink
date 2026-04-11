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
        public int ID { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public enum UserRole
    {
        Patient,
        SpecialistDoctor,
        MedicalAssistant,
        Administrator
    }

    public abstract class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // The hashed password cannot be reversed, so it is safe to store it's bytes in plain text
        public string PasswordSalt {  get; set; } // The salt is not reversable so it's save to store it this way
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        public int ContactInfoID { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();

    }

    public class  Patient : User
    {
        public List<Diagnosis> Dignoses { get; set; } = new List<Diagnosis>();
        public int DataSheetID { get; set; }
        public PatientDataSheet DataSheet { get; set; }
        public List<SpecialistDoctor> SpecialistDoctors { get; set; } = new List<SpecialistDoctor>();
        public List<AppointmentRequest> Requests { get; set; } = new List<AppointmentRequest>();
        public List<Referal> Referals { get; set; } = new List<Referal>();
    }

    public class SpecialistDoctor : User
    {
        public int DataSheetID { get; set; }
        public MedicalStaffDataSheet DataSheet { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public List<MedicalAsisstant> MedicalAsisstants { get; set; }
        public List<Referal> IssuedReferals { get; set; } = new List<Referal>();
        public List<AppointmentRequest> Requests { get; set; } = new List<AppointmentRequest>();
    }
     
    public class MedicalAsisstant : User
    {
        public List<SpecialistDoctor> AttendingDoctors { get; set; }
        public int DataSheetID { get; set; }
        public MedicalStaffDataSheet DataSheet { get; set; }
    }

    public class Administrator : User
    {
        public int DataSheetID { get; set; }
        public MedicalStaffDataSheet DataSheet { get; set; }
    }
}
