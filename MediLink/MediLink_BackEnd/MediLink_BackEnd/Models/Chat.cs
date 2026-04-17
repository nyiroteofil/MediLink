namespace MediLink_BackEnd.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
        public int DoctorID { get; set; }
        public SpecialistDoctor Doctor { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
    }

    public class Message
    {
        public int ID { get; set; }
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
        public int SenderID { get; set; }
        public User Sender { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
