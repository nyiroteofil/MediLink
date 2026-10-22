using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using MediLink_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediLink_BackEnd.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Controllers
{
    [Authorize(Roles="Administrator")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;
        private readonly IUserService _userService;
        private readonly MediLinkContext _dbContext;
        private readonly ILogger _logger;

        public AdministratorController(IAdministratorService service, IUserService userService, MediLinkContext context, ILogger logger)
        {
            _administratorService = service;
            _userService = userService;
            _dbContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuspendedAdministrators()
        {
            try
            {
                List<UserSummaryDTO> dtos = await _administratorService.GetAllSuspendedAdministrators();

                if (dtos.Count != 0) return Ok(dtos);
                else return NotFound("No Active Administrators Found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSuspendedAssistants()
        {
            try
            {
                List<UserSummaryDTO> dtos = await _administratorService.GetAllSuspendedAssistants();

                if (dtos.Count != 0) return Ok(dtos);
                else return NotFound("No Active Assistants Found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSuspendedDoctors()
        {
            try
            {
                List<UserSummaryDTO> dtos = await _administratorService.GetAllSuspendedDoctors();

                if (dtos.Count != 0) return Ok(dtos);
                else return NotFound("No Suspended Doctors Found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSuspendedPatients()
        {
            try
            {
                List<UserSummaryDTO> dtos = await _administratorService.GetAllSuspendedPatients();

                if (dtos.Count != 0) return Ok(dtos);
                else return NotFound("No Suspended Patients Found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetActiveAdministrators()
        {
            try
            {
                List<UserSummaryDTO> dtos = await _administratorService.GetAllActiveAdministrators();

                if (dtos.Count != 0) return Ok(dtos);
                else return NotFound("No Active Administrators Found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddAdministrator(StaffUserDTO dto)
        {
            try
            {
                Administrator admin = await _userService.CreateAdministrator(dto);

                if (admin == null) return BadRequest("Could not create administrator");
                else return Ok(admin);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        // I should definitely think whether doctors or assistants should be
        // able to add patients themselves. If that is the case, I need to make
        // authorizations explicit
        
        [Authorize(Roles="Administrator, SpecialistDoctor, MedicalAssistant")]
        [HttpPost]
        public async Task<IActionResult> AddPatientUser(PatientDTO dto)
        {
            try
            {
                Patient patient = await _userService.CreatePatientUser(dto);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddSpecialistDoctorUser(StaffUserDTO dto)
        {
            try
            {
                SpecialistDoctor doc = await _userService.CreateDoctorUser(dto);
                return Ok(doc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddAssistantUser(StaffUserDTO dto)
        {
            try
            {
                List<SpecialistDoctor> attendingDoctors = new List<SpecialistDoctor>();

                if (dto.SpecialistDoctorIds != null && dto.SpecialistDoctorIds.Count() > 0)
                {
                    attendingDoctors = await _dbContext.SpecialistDoctors
                        .Where(sp => dto.SpecialistDoctorIds.Contains(sp.ID))
                        .ToListAsync();
                } 

                if (dto.SpecialistDoctorIds != null && dto.SpecialistDoctorIds.Count() != attendingDoctors.Count())
                {
                    _logger.LogError("Invalid Specialist Doctor ID(s) found in request: {InvalidIds}", dto.SpecialistDoctorIds);
                    return BadRequest("Invalid Specialist Doctor ID(s) found in request!");
                }
                    
                // AttendingDoctorsForAssistant argument may be null, if assistant lacs supervisors at the moment of
                // account creation
                MedicalAssistant createdAssistant = await _userService.CreateMedicalAssistantUser(dto, attendingDoctors);
                return Ok(createdAssistant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating specialist doctor for {dto.Username}", dto.Username);
                return StatusCode(500, "An internal server error occurred while creatin Medica Assistant.");
            }
        }
        
        [HttpPatch]
        public async Task<IActionResult> ActivateUser(int userID)
        {
            try
            {
                if (await _administratorService.ActivateUser(userID)) return Ok();
                else return NotFound($"User with ID \"{userID}\" not found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPatch]
        public async Task<IActionResult> SuspendUser(int userID)
        {
            try
            {
                if (await _administratorService.SuspendUser(userID)) return Ok();
                else return NotFound($"User with ID \"{userID}\" not found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
