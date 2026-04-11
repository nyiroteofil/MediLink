using System.Diagnostics.Eventing.Reader;

namespace MediLink_BackEnd.Models
{
    public abstract class Event
    {
        public int ID { get; set; }
        public EventStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }

    public class Appointment : Event
    {
        public int SpecialistDoctorID { get; set; }
        public SpecialistDoctor SpecialistDoctor { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
        public string ReasonOfVisit { get; set; }
        public int InstitutionID { get; set; }
        public Institution PlaceOfVisit { get; set; }
    }

    public class MedicationReminder : Event
    {
        public int MedicationID { get; set; }
        public Medication Medication {  get; set; }
        public User AdministratorOfMedicine { get; set; }
        public int AdministratorId { get; set; }
    }

    public class CustomEvent : Event
    {
        public string Label { get; set; }
        public List<User> Attnedees { get; set; } = new List<User>();
    }

    public enum EventStatus
    {
        Scheduled,
        Finished,
        Deleted,
        DataInputError,
        Unknown
    }
}
