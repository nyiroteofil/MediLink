using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Data
{
    public class MediLinkContext: DbContext
    {
        public MediLinkContext(DbContextOptions<MediLinkContext> options) : base(options)
        {
        }
        public DbSet<Models.Medication> Medication { get; set; }
        public DbSet<Models.Institution> Institution { get; set; }
        public DbSet<Models.PatientDataSheet> PatientDataSheet { get; set; }
        public DbSet<Models.MedicalStaffDataSheet> MedicalStaffDataSheet { get; set; }
        public DbSet<Models.Diagnosis> Diagnosi { get; set; }
        public DbSet<Models.Referal> Referal { get; set; }

        // Specify database relationships with Fluent API

        protected override void OnModelCreating(ModelBuilder mb)
        {
            
        }
    }
}
