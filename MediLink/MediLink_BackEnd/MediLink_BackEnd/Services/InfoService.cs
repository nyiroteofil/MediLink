using MediLink_BackEnd.Data;
using MediLink_BackEnd.Models;

using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Services
{
    public interface IInfoService
    {
        public Task<int> GetDocAppointmentsWeekAhead(int docID);
        public Task<DateTime?> GetNextAppointmentDate(int suerID);
        public Task<int> DocActivePatientNumber(int docID);
    }

    public class InfoService : IInfoService
    {
        private readonly MediLinkContext _dbContext;

        public InfoService(MediLinkContext dbContext)
        {
            _dbContext = dbContext;
        }

        // gets the number of appointmetns a doctor has in the next 7 days
        public async Task<int> GetDocAppointmentsWeekAhead(int docID)
        {
            try
            {
                return await _dbContext.Appointments
                    .Where(a => a.SpecialistDoctorID == docID &&
                        a.StartTime >= DateTime.Now &&
                        a.StartTime <= DateTime.Now.AddDays(7))
                    .CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public async Task<DateTime?> GetNextAppointmentDate(int userID)
        {
            try
            {
                Appointment nextAppointment = await _dbContext.Appointments
                    .Where(a => (a.PatientID == userID || a.SpecialistDoctorID == userID) && a.StartTime >= DateTime.Now)
                    .OrderBy(a => a.StartTime)
                    .FirstOrDefaultAsync();

                return nextAppointment?.StartTime;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<int> DocActivePatientNumber(int docID)
        {
            try
            {
                int numOfActivePatients = await _dbContext.Patients
                    .Include(p => p.SpecialistDoctors)
                    .Include(p => p.Dignoses)
                    .CountAsync(p =>
                        p.SpecialistDoctors.Any(sp => sp.ID == docID) &&
                        p.Dignoses.Any(d =>
                        d.Status == DiagnosisStatus.Active ||
                        d.Status == DiagnosisStatus.Remission));

                return numOfActivePatients;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}
