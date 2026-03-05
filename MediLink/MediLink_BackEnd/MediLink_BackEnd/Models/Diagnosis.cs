namespace MediLink_BackEnd.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public List<Symptom> Symptoms { get; set; }
        public DiagnosisStatus Status { get; set; }
        public List<Medication> PrescribedMedications { get; set; }
    }

    public class Symptom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SymptomSeverity Severity { get; set; }
    }

    public enum DiagnosisStatus
    {
        Active,
        Remission,
        Healed
    }

    public enum SymptomSeverity
    {
        Mild,
        Moderate,
        Severe
    }
}
