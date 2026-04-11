namespace MediLink_BackEnd.Models
{
    public class Institution
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public InstitutionType Type { get; set; }

    }

    public enum InstitutionType
    {
        OutpatientClinic,
        SpecialistOutpatientClinic,
        DiagnosticLaboratory,
    }
}
