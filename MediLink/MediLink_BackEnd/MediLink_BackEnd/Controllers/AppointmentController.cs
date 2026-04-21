using MediLink_BackEnd.Data;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using MediLink_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediLink_BackEnd.Controllers
{
    [Authorize(Roles = "Patient,Admin,SpecialistDoctor,MedicalAssistant")]
    [Route("api/Appointments/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly MediLinkContext _dbContext;
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(MediLinkContext dbContext, IAppointmentService appointmentService)
        {
            _dbContext = dbContext;
            _appointmentService = appointmentService;
        }

        // GET: api/<AppointmentController>
        [HttpPost]
        [AllowAnonymousAttribute]
        public async Task<IActionResult> RequestAppointment(AppointmentRequestDTO dto)
        {
            try
            {
                AppointmentRequest req = await _appointmentService.RequestAppointment(dto);
                return Ok(req);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientsAppointments(int ID)
        {
            try
            {
                List<AppointmentRequestResponseDTO> dtos = await _appointmentService.GetPatientsAppointment(ID);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorsAppointments(int ID)
        {
            try
            {
                List<AppointmentRequestResponseDTO> dtos = await _appointmentService.GetDoctorsAppointment(ID);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingAppointments(int userID)
        {
            List<AppointmentRequestResponseDTO> requests;

            try
            {
                requests = await _appointmentService.GetPendingAppointmentRequests(userID);
                return Ok(requests);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        // Doesn't need the doctor's ID as the request class has it
        [HttpPost]
        public async Task<IActionResult> AcceptAppointment(int requestID) 
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientPendingAppointments(int docID)
        {
            throw new NotImplementedException();
        }
    }
}
