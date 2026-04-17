namespace MediLink_BackEnd.Data.DTOs
{
    public class ChatSummaryDTO
    {
        public int ID { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
