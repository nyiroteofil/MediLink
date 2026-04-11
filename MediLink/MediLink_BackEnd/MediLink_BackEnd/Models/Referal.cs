using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MediLink_BackEnd.Models
{
    public class Referal
    {
        public int ID { get; set; }
        public int SpecialistDoctorID { get; set; }
        public SpecialistDoctor IssuingDoctor { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
        public Institution ReferedInstitition { get; set; }
        public Diagnosis CauseDiagnosis { get; set; }
        public string? ConsiliumQuestion { get; set; }
        public string? ConsiliumAnswer { get; set; }
        public DateOnly IssuingDate { get; set; }
        public DateOnly? ExpirationDate { get; set; } = null; // If null, then it's 90 days counting from issuing
        public bool CausesInabilityToWork { get; set; } = false; 
    }
}
