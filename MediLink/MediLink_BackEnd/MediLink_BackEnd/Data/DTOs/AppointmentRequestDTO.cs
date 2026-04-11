using MediLink_BackEnd.Models;

namespace MediLink_BackEnd.Data.DTOs
{
    public class AppointmentRequestDTO
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int SpecialistDoctorID { get; set; }
        public string ReasonOfRequest { get; set; }
        public RequestStatus Status { get; set; }
        public string? ReasonOfDenial { get; set; } // only if it's denied, else it's null
    }
}
