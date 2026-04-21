using MediLink_BackEnd.Data;
using MediLink_BackEnd.Models;
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
    public class InfoController : ControllerBase
    {
        private readonly IInfoService _infoService;

        public InfoController(IInfoService infoService)
        {
            _infoService = infoService;
        }

        [Authorize(Roles = "SpecialistDoctor,MedicalAssistant,Administrator")]
        [HttpGet]
        public async Task<int> DoctorsAppointmentsWeekAhead(int docID)
        {
            int appointments = await _infoService.GetDocAppointmentsWeekAhead(docID);

            if (appointments == -1) return 0;
            else return appointments;

        }

        [Authorize(Roles = "Patient,SpecialistDoctor,MedicalAssistant,Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetNextAppointmentDate(int userID)
        {
            DateTime? appDate = await _infoService.GetNextAppointmentDate(userID);

            if (appDate == null) return null;
            else return Ok(appDate);
        }


        [Authorize(Roles = "SpecialistDoctor,MedicalAssistant,Administrator")]
        [HttpGet]
        public async Task<IActionResult> DocActivePatientsNumber(int docID)
        {
            try
            {
                int numberOfPatients = await _infoService.DocActivePatientNumber(docID);

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
}
