using MediLink_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Data
{
    public class MediLinkContext : DbContext
    {
        public MediLinkContext(DbContextOptions<MediLinkContext> options) : base(options)
        { }

        public DbSet<Medication> Medications { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<User> Users { get; set; } // Adding the apstract class, as we can use the .UseTtpMappingStrategy() So we don't have to do it by hand for each sub-class
        public DbSet<Patient> Patients { get; set; }
        public DbSet<SpecialistDoctor> SpecialistDoctors { get; set; }
        public DbSet<MedicalAsisstant> MedicalAsisstants { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<DataSheet> DataSheets {  get; set; }
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
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        // Specifying abiguous database relationships with Fluent API

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<User>().UseTptMappingStrategy();
            mb.Entity<Event>().UseTptMappingStrategy();
            mb.Entity<DataSheet>().UseTptMappingStrategy();

            // Resolving cascade cycles and references to sub-class
            mb.Entity<Referal>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Referals)
                .HasForeignKey(r => r.PatientID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Referal>()
                .HasOne(r => r.IssuingDoctor)
                .WithMany(d => d.IssuedReferals)
                .HasForeignKey(r => r.SpecialistDoctorID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<AppointmentRequest>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Requests)
                .HasForeignKey(r => r.PatientID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<AppointmentRequest>()
                .HasOne(r => r.SpecialistDoctor)
                .WithMany(d => d.Requests)
                .HasForeignKey(r => r.SpecialistDoctorID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<MedicationReminder>()
                .HasOne(mr => mr.AdministratorOfMedicine)
                .WithMany()
                .HasForeignKey(mr => mr.AdministratorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Appointment>()
                .HasOne(a => a.SpecialistDoctor)
                .WithMany()
                .HasForeignKey(a => a.SpecialistDoctorID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<CustomEvent>()
                .HasMany(ce => ce.Attnedees)
                .WithMany()
                .UsingEntity(j => j.ToTable("CustomEventAttendees"));

            mb.Entity<MedicalAsisstant>()
                .HasMany(ma => ma.AttendingDoctors)
                .WithMany(sd => sd.MedicalAsisstants);

            mb.Entity<Medication>()
                .HasMany(m => m.Ingredients)
                .WithMany(i => i.UsedInMedications)
                .UsingEntity(j => j.ToTable("MedicationIngredient"));


            // Messageing set up
            mb.Entity<Chat>()
                .HasOne(c => c.Patient)
                .WithMany()
                .HasForeignKey(c => c.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Chat>()
                .HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatID)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
