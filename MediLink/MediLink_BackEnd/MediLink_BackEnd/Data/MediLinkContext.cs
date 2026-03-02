using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Data
{
    public class MediLinkContext: DbContext
    {
        public MediLinkContext(DbContextOptions<MediLinkContext> options) : base(options)
        {
        }
        public DbSet<Models.Medication> Medications { get; set; }
        public DbSet<Models.Institution> Institutions { get; set; }
        public DbSet<Models.PatientDataSheet> PatientDataSheets { get; set; }
        public DbSet<Models.MedicalStaffDataSheet> MedicalStaffDataSheets { get; set; }

        // Specify database relationships with Fluent API

        protected override void OnModelCreating(ModelBuilder mb)
        {
            
        }
    }
}
