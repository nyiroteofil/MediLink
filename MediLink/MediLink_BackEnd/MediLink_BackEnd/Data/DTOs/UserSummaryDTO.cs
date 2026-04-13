using MediLink_BackEnd.Models;

namespace MediLink_BackEnd.Data.DTOs
{
    public class UserSummaryDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }
    }
}
