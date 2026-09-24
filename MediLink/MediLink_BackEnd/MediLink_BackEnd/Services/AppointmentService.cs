using MediLink_BackEnd.Data;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Services
{
    public interface IAppointmentService
    {
        public Task<AppointmentRequest> RequestAppointment(AppointmentRequestDTO dto);
        public Task<List<AppointmentRequestResponseDTO>> GetPatientsAppointment(int id);
        public Task<List<AppointmentRequestResponseDTO>> GetDoctorsAppointment(int id);
        public Task<List<AppointmentRequestResponseDTO>> GetPendingAppointmentRequests(int userID);
    }

    public class AppointmentService : IAppointmentService
    {
        private readonly MediLinkContext _dbContext;

        public AppointmentService(MediLinkContext context)
        {
            _dbContext = context;
        }

        public async Task<List<AppointmentRequestResponseDTO>> GetPendingAppointmentRequests(int userID)
        {
            try
            {
                var requests = await _dbContext.AppointmentRequests
                    .Include(r => r.Patient)
                        .ThenInclude(p => p.DataSheet)
                    .Include(r => r.SpecialistDoctor)
                        .ThenInclude(d => d.DataSheet)
                    .Where(ar => ar.SpecialistDoctorID == userID
                        && ar.Status == RequestStatus.Pending)
                    .ToListAsync();

                return requests.Select(r => new AppointmentRequestResponseDTO
                {
                    ID = r.ID,
                    PatientID = r.PatientID,
                    PatientName = $"{r.Patient.DataSheet.FirstName} {r.Patient.DataSheet.LastName}",
                    SpecialistDoctorID = r.SpecialistDoctorID,
                    DoctorName = $"{r.SpecialistDoctor.DataSheet.FirstName} {r.SpecialistDoctor.DataSheet.LastName}",
                    ReasonOfRequest = r.ReasonOfRequest,
                    Status = r.Status,
                    ReasonOfDenial = r.ReasonOfDenial
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        async public Task<AppointmentRequest> RequestAppointment(AppointmentRequestDTO dto)
        {
            Patient patient;
            SpecialistDoctor doc;

            try
            {
                patient = await _dbContext.Patients.FindAsync(dto.PatientID);
                doc = await _dbContext.SpecialistDoctors.FindAsync(dto.SpecialistDoctorID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            // test wheather there is either patient or doctor with the given IDs (Both required)
            if (patient == null || doc == null) return null;

            AppointmentRequest appointmentReq = new AppointmentRequest
            {
                PatientID = patient.ID,
                Patient = patient,
                SpecialistDoctorID = dto.SpecialistDoctorID,
                SpecialistDoctor = doc,
                ReasonOfRequest = dto.ReasonOfRequest,
                Status = RequestStatus.Pending,
                ReasonOfDenial = dto.ReasonOfDenial
            };

            
            try
            {
                _dbContext.AppointmentRequests.Add(appointmentReq);
                await _dbContext.SaveChangesAsync();
                return appointmentReq;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<List<AppointmentRequestResponseDTO>> GetPatientsAppointment(int id)
        {
            List<AppointmentRequest> requests;

            try
            {
                requests = await _dbContext.AppointmentRequests
                    .Include(r => r.Patient)
                        .ThenInclude(p => p.DataSheet)
                    .Include(r => r.SpecialistDoctor)
                        .ThenInclude(d => d.DataSheet)
                    .Where(r => r.PatientID == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            if (requests == null || requests.Count() == 0) return new List<AppointmentRequestResponseDTO>();

            List<AppointmentRequestResponseDTO> requestResponseDTOs = new List<AppointmentRequestResponseDTO>();

            foreach (AppointmentRequest req in requests)
            {
                requestResponseDTOs.Add(new AppointmentRequestResponseDTO
                {
                    ID = req.ID,
                    PatientID = req.PatientID,
                    PatientName = $"{req.Patient.DataSheet.FirstName} {req.Patient.DataSheet.LastName}",
                    SpecialistDoctorID = req.SpecialistDoctorID,
                    DoctorName = $"{req.SpecialistDoctor.DataSheet.FirstName}" + $" {req.SpecialistDoctor.DataSheet.LastName}",
                    ReasonOfRequest = req.ReasonOfRequest,
                    Status = req.Status,
                    ReasonOfDenial = req.ReasonOfDenial,
                });
            }

            return requestResponseDTOs;
        }

        public async Task<List<AppointmentRequestResponseDTO>> GetDoctorsAppointment(int id)
        {
            List<AppointmentRequest> requests;

            try
            {
                requests = await _dbContext.AppointmentRequests
                    .Include(r => r.Patient)
                        .ThenInclude(p => p.DataSheet)
                    .Include(r => r.SpecialistDoctor)
                        .ThenInclude(d => d.DataSheet)
                    .Where(r => r.SpecialistDoctorID == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            if (requests == null || requests.Count() == 0) return new List<AppointmentRequestResponseDTO>();

            List<AppointmentRequestResponseDTO> requestResponseDTOs = new List<AppointmentRequestResponseDTO>();

            foreach (AppointmentRequest req in requests)
            {
                requestResponseDTOs.Add(new AppointmentRequestResponseDTO
                {
                    ID = req.ID,
                    PatientID = req.PatientID,
                    PatientName = $"{req.Patient.DataSheet.FirstName} {req.Patient.DataSheet.LastName}",
                    SpecialistDoctorID = req.SpecialistDoctorID,
                    DoctorName = $"{req.SpecialistDoctor.DataSheet.FirstName}" + $"{req.SpecialistDoctor.DataSheet.LastName}",
                    ReasonOfRequest = req.ReasonOfRequest,
                    Status = req.Status,
                    ReasonOfDenial = req.ReasonOfDenial,
                });
            }

            return requestResponseDTOs;

        }
    }
}
