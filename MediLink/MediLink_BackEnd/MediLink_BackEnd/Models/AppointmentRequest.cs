namespace MediLink_BackEnd.Models
{
    public class AppointmentRequest
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
        public int SpecialistDoctorID { get; set; }
        public SpecialistDoctor SpecialistDoctor { get; set; }
        public string ReasonOfRequest { get; set; }
        public RequestStatus Status { get; set; }
        public string? ReasonOfDenial { get; set; }
    }

    public enum RequestStatus
    {
        Pending,
        Accepted,
        Denied
    }
}
