namespace MediLink_BackEnd.Models
{
    public abstract class DataSheet
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DateOfBirth { get; set; }
        public char Sex { get; set; }
        public string? Address { get; set; }
    }

    public class PatientDataSheet : DataSheet
    {
        public string TAJNumber { get; set; }
    }

    public class MedicalStaffDataSheet : DataSheet
    {
        public string EmployeeID { get; set; }
        public string Position { get; set; }
        public Institution Institution { get; set; }
    }
}
