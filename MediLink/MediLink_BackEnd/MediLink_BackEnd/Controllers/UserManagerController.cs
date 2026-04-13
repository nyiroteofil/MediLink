using MediLink_BackEnd.Data;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using MediLink_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediLink_BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserManagerController : Controller
    {
        private readonly MediLinkContext _dbContext;
        private readonly IUserService _userService;

        public UserManagerController(MediLinkContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
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
                return StatusCode(500, $"An error occured during administrator creation: {ex.Message}");
            }
        }

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
                return StatusCode(500, ex.ToString());
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
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginDTO dto)
        {
            string loginToken;

            try
            {
                loginToken = await _userService.UserLogIn(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }

            return Ok(loginToken);

        }

        [HttpPatch]
        public async Task<IActionResult> ActivateUser(int userID)
        {
            try
            {
                if (await _userService.ActivateUser(userID)) return Ok();
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
                if (await _userService.SuspendUser(userID)) return Ok();
                else return NotFound($"User with ID \"{userID}\" not found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

    }

}
