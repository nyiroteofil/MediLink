using Microsoft.EntityFrameworkCore;
using MediLink_BackEnd.Models;

namespace MediLink_BackEnd.Data
{
    public class MediLinkContext: DbContext
    {
        public MediLinkContext(DbContextOptions<MediLinkContext> options) : base(options)
        {}

        public DbSet<Medication> Medications { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<User> Users { get; set; } // Adding the apstract class, as we can use the .UseTtpMappingStrategy() So we don't have to do it by hand for each sub-class
        public DbSet<Patient> Patients { get; set; }
        public DbSet<SpecialistDoctor> SpecialistDoctors { get; set; }
        public DbSet<MedicalAsisstant> MedicalAsisstants { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<PatientDataSheet> PatientDataSheets { get; set; }
        public DbSet<MedicalStaffDataSheet> MedicalStaffDataSheets { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Referal> Referals { get; set; }
        public DbSet<MedicationReminder> MedicationReminders { get; set; }
        public DbSet<Event> Events { get; set; } // Same reason as with "User" class
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<CustomEvent> CustomEvents { get; set; }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }

        // Specifying abiguous database relationships with Fluent API

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Resolving cascade cycles
            mb.Entity<Patient>()
                .HasMany(p => p.Referals)
                .WithOne(r => r.Patient)
                .OnDelete(DeleteBehavior.NoAction);

            mb.Entity<SpecialistDoctor>()
                .HasMany(p => p.IssuedReferals)
                .WithOne(r => r.IssuingDoctor)
                .OnDelete(DeleteBehavior.NoAction);

            mb.Entity<Patient>()
                .HasMany(p => p.Requests)
                .WithOne(r => r.Patient)
                .OnDelete(DeleteBehavior.NoAction);

            mb.Entity<MedicationReminder>()
                .HasOne(mr => mr.AdministratorOfMedicine)
                .WithMany(u => u.AsministeringMedication)
                .OnDelete(DeleteBehavior.NoAction);

            mb.Entity<Appointment>()
                .HasOne(a => a.SpecialistDoctor)
                .WithMany(sp => sp.Appointments)
                .OnDelete(DeleteBehavior.NoAction);

            mb.Entity<MedicalAsisstant>()
                .HasOne(ma => ma.AttendingDoctor)
                .WithMany(sd => sd.MedicalAsisstants)
                .OnDelete(DeleteBehavior.NoAction);


            mb.Entity<User>().UseTptMappingStrategy();
            mb.Entity<Event>().UseTptMappingStrategy();

        }
    }
}
