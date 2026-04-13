using MediLink_BackEnd.Models;

namespace MediLink_BackEnd.Data.DTOs
{
    public class PatientDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserStatus UserStatus { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DateOfBirth { get; set; }
        public char Sex { get; set; }
        public string? Address { get; set; }
        public string TAJNumber { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class StaffUserDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserStatus UserStatus { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DateOfBirth { get; set; }
        public char Sex { get; set; }
        public string? Address { get; set; }
        public string? EmployeeID { get; set; }
        public string? Position { get; set; }
        public int InstitutionID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
