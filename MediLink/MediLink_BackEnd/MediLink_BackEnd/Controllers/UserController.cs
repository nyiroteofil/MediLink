using System.Net;
using System.Security.Claims;
using Azure;
using MediLink_BackEnd.Data;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Models;
using MediLink_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Controllers;
/*
 * This controller is the organized aggregation of all the endpoints
 * that concerns the users in general. This means actions that all user should be
 * able to do who uses the software.
 */

[Authorize( Roles = "Admin, SpecialistDoctor, MedicalAssistant")]
[ApiController]
[Route("api/Users/[controller]")]
public class UserController : Controller
{
    private readonly MediLinkContext  _context;
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
    
    public UserController(MediLinkContext context, IUserService userService, ILogger<UserController> logger) 
    {
        _context = context;
        _userService = userService;
        _logger = logger;
    }
       
    [AllowAnonymous]
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
            return BadRequest("\"An error occured while trying to log in the user.\"");
        }

        return Ok(loginToken);

    }

    // Next endpoints to add is:
    // - Modifying user information
    // - Seraching for specific doctors, asistnts or patients by id
    
    
    [HttpGet]
    public async Task<IActionResult> GetActiveDoctors()
    {
        try
        {
            List<UserSummaryDTO> dtos = await _userService.GetAllActiveDoctors();

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
    public async Task<IActionResult> GetActiveAssistants()
    {
        try
        {
            List<UserSummaryDTO> dtos = await _userService.GetAllActiveAssistants();

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
    public async Task<IActionResult> GetActivePatients()
    {
        try
        {
            List<UserSummaryDTO> dtos = await _userService.GetAllActivePatients();

            if (dtos.Count != 0) return Ok(dtos);
            else return NotFound("No Active Patients Found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(500, "An internal server error occurred.");
        }
    }

    [Authorize(Roles = "Patient")]
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchPatient(int id, [FromBody] JsonPatchDocument<PatientUpdateDTO> patchDoc)
    {
        if (patchDoc == null) return BadRequest("Patch Document Missing!");

        string requestingUserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        if (!int.TryParse(requestingUserID, out int intUserID)) return BadRequest("Invalid User Token!");
        
        
        User requestingUser = await _context.Users.FirstOrDefaultAsync(u => u.ID == intUserID);
        Patient targetUser = await _context.Patients
            .Include(p => p.DataSheet)
            .Include(p => p.ContactInfo)
            .FirstOrDefaultAsync(p => p.ID == id);
        
        if (requestingUser == null) return NotFound("User ID that is contained in your token doesn't exist!");
        if (targetUser == null) return NotFound($"User with {id} doesn't exist!");
        if (requestingUser.Status == UserStatus.Suspended) return StatusCode(403, "Your account is suspended!");

        try
        {
            PatientUpdateDTO dto = new PatientUpdateDTO
            {
                Username = targetUser.Username,
                Address = targetUser.DataSheet.Address,
                Email = targetUser.ContactInfo.Email,
                PhoneNumber = targetUser.ContactInfo.PhoneNumber
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!TryValidateModel(dto)) return BadRequest("Invalid Values!");

            // Here, I branched the execution depending on what role the
            // user has as both need different evaluations for it to be safe.
            // For the admin role, I'll make a separate controller, as that needs
            // to have the ability for more extensive data modification by nature.
            
            string requesterRole = User.FindFirst(ClaimTypes.Role).Value;

            if (string.IsNullOrEmpty(requesterRole))
            {
                return StatusCode(403, "Access Dennied! Missing role in Token.");
            }
            else if (requesterRole == "Patient")
            {
                if (intUserID == id)
                {
                    return Ok(_userService.PatchPatientUser(targetUser, dto));
                }
                else return StatusCode(403, "User ID mismatch! You're not authorized to modify this user's data!");
            }
            else if (requesterRole == "SpecialistDoctor")
            {
                SpecialistDoctor requestingDoctor = await _context.SpecialistDoctors
                    .Include(sp => sp.Patients)
                    .Where(sp => sp.ID == intUserID)
                    .FirstOrDefaultAsync();

                if (!requestingDoctor.Patients.Any(p => p.ID == id))
                {
                    return Ok(_userService.PatchPatientUser(targetUser, dto));
                }
                else
                {
                    return StatusCode(403, "You don't have a patient with that ID. You cannot modify this patient's data!");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, "An internal server error occurred.");
        }
        
        // For some reason, the compiler says I missed a return statement.
        // I'll fix this later
        return StatusCode(500, "Unknown Error!");
    }
    
    [HttpPatch]
    public IActionResult RespondToAppointmentRequest(int AppointmentId, bool Accept)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult SetNewDiagnosis(int UserId, Diagnosis diagnosis)
    {
        return Ok(HttpStatusCode.Created);
    }

    [HttpPost]
    public IActionResult AddSymptomToDiagnosis(int userId, int diagnosisId, Symptom symptom)
    {
        return Ok(HttpStatusCode.Created);
    }

    [HttpDelete]
    public IActionResult RemoveSymtomFromDiagnosis(int userId, int diagnosisId, int symptomId)
    {
        return Ok(); 
    }

    [HttpPatch]
    public IActionResult ChangeDiagnosisStatus(int userId, int diagnosisId, DiagnosisStatus status)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateReferal(int userId, Referral referral)
    {
        return Ok(HttpStatusCode.Created);
    }

    [HttpPatch]
    public IActionResult UpdateReferal(int userId, Referral referral)
    {
        return Ok();
    }
    
    // Methods relating to user information
    
    [Authorize(Roles = "SpecialistDoctor,MedicalAssistant,Administrator")]
    [HttpGet]
    public async Task<IActionResult> DoctorsAppointmentsWeekAhead()
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userID))
        {
            return Unauthorized("Invalid User Token!");
        }
        
        int appointments;

        try
        {
            appointments = await _userService.GetDocAppointmentsWeekAhead(userID);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(500, "An internal occurred, while trying to get the appointment");
        }
            
        return Ok(appointments);
    }

    [Authorize(Roles = "Patient,SpecialistDoctor,MedicalAssistant,Administrator")]
    [HttpGet]
    public async Task<IActionResult> GetNextAppointmentDate(int userID)
    {
        DateTime? appDate = await _userService.GetNextAppointmentDate(userID);

        if (appDate == null) return null;
        else return Ok(appDate);
    }


    [Authorize(Roles = "SpecialistDoctor,MedicalAssistant,Administrator")]
    [HttpGet]
    public async Task<IActionResult> DocActivePatientsNumber(int docID)
    {
        try
        {
            int numberOfPatients = await _userService.DocActivePatientNumber(docID);

            if (numberOfPatients == -1) return StatusCode(((int)HttpStatusCode.InternalServerError), "InfoService returned with \"-1\" value (No such patients, or error)");
            else return Ok(numberOfPatients);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(500, "\"An internal server error occurred.\"");
        }
    }
}
