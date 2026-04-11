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
    [Authorize(Roles = "Admin,SpecialistDoctor,MedicalAssistant")]
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
                return StatusCode(500, ex.ToString());
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
                return StatusCode(500, ex.ToString());
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
                return StatusCode(500, ex.ToString());
            }
        }

        
    }
}
