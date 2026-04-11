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
    [Route("api/Users/[action]")]
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
        public IActionResult ChangeUserStatus(int UserId, UserStatus status)
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult UpdateUser(int UserId)
        {
            return Ok();
        }


    }
}
