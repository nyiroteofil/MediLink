using MediLink_BackEnd.Models;

namespace MediLink_BackEnd.Data.DTOs
{
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
