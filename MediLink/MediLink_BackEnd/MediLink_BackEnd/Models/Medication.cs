using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Models
{
    public enum MedicationForm
    {
        tablet,
        liquid,
        injection,
        inhaler
    }

    public class Medication
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public MedicationForm MedicationForm { get; set; }
        public string Manufacturer { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string SerialNumber { get; set; }
        public string LotNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Description { get; set; }
    }

    public class Ingredient
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Medication> UsedInMedications { get; set; }
    }
}
