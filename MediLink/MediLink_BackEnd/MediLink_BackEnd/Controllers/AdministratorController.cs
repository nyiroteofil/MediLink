using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediLink_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService service)
        {
            _administratorService = service;
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

        [HttpGet]
        public async Task<IActionResult> GetActiveAssistants()
        {
            try
            {
                List<UserSummaryDTO> dtos = await _administratorService.GetAllActiveAssistants();

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
        public async Task<IActionResult> GetActiveDoctors()
        {
            try
            {
                List<UserSummaryDTO> dtos = await _administratorService.GetAllActiveDoctors();

                if (dtos.Count != 0) return Ok(dtos);
                else return NotFound("No Active Doctors Found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActivePatients()
        {
            try
            {
                List<UserSummaryDTO> dtos = await _administratorService.GetAllActivePatients();

                if (dtos.Count != 0) return Ok(dtos);
                else return NotFound("No Active Patients Found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
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
    }
}
