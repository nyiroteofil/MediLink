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
        public string Id { get; set; }
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
        public List<Event> Events { get; set; } = new List<Event>();
        public List<MedicationReminder> AsministeringMedication {  get; set; } // later will remove, if I revise the model

    }

    public class  Patient : User
    {
        public List<Diagnosis> Dignoses { get; set; } = new List<Diagnosis>();
        public PatientDataSheet DataSheet { get; set; }
        public List<SpecialistDoctor> SpecialistDoctors { get; set; } = new List<SpecialistDoctor>();
        public List<AppointmentRequest> Requests { get; set; } = new List<AppointmentRequest>();
        public List<Referal> Referals { get; set; } = new List<Referal>();
        public List<MedicationReminder> MedicationReminders { get; set; } // later will remove, if I revise the model
        public List<Appointment> Appointments { get; set; } // later will remove, if I revise the model
        public List<CustomEvent> CustomEvents { get; set; } // later will remove, if I revise the model
    }

    public class SpecialistDoctor : User
    {
        public MedicalStaffDataSheet DataSheet { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public List<AppointmentRequest> PendingAppointments { get; set; } = new List<AppointmentRequest>();
        public List<MedicalAsisstant> MedicalAsisstants { get; set; }
        public List<Referal> IssuedReferals { get; set; } = new List<Referal>();
        public List<MedicationReminder> MedicationReminders { get; set; } // later will remove, if I revise the model
        public List<Appointment> Appointments { get; set; } // later will remove, if I revise the model
        public List<CustomEvent> CustomEvents { get; set; } // later will remove, if I revise the model
    }
     
    public class MedicalAsisstant : User
    {
        public SpecialistDoctor AttendingDoctor { get; set; }
        public MedicalStaffDataSheet DataSheet { get; set; }
        public List<MedicationReminder> MedicationReminders { get; set; } // later will remove, if I revise the model
        public List<Appointment> Appointments { get; set; } // later will remove, if I revise the model
        public List<CustomEvent> CustomEvents { get; set; } // later will remove, if I revise the model
    }

    public class Administrator : User
    {
        public List<MedicationReminder> MedicationReminders { get; set; } // later will remove, if I revise the model
        public List<Appointment> Appointments { get; set; } // later will remove, if I revise the model
        public List<CustomEvent> CustomEvents { get; set; } // later will remove, if I revise the model

    }
}
