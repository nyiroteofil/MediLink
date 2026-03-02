namespace MediLink_BackEnd.Models
{
    public class AppointmentRequest
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
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
