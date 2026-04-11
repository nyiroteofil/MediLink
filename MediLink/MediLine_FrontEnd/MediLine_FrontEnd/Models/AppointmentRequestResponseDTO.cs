namespace MediLine_FrontEnd.Models
{
    public enum RequestStatus
    {
        Pending,
        Accepted,
        Denied
    }

    public class AppointmentRequestResponseDTO
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int SpecialistDoctorID { get; set; }
        public string DoctorName { get; set; }
        public string ReasonOfRequest { get; set; }
        public RequestStatus Status { get; set; }
        public string? ReasonOfDenial { get; set; }
    }
}
