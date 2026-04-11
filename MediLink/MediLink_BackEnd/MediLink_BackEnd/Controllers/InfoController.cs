using MediLink_BackEnd.Data;
using MediLink_BackEnd.Models;
using MediLink_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediLink_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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
        public async Task<DateTime?> GetNextAppointmentDate(int userID)
        {
            DateTime? appDate = await _infoService.GetNextAppointmentDate(userID);

            if (appDate == null) return null;
            else return appDate;
        }


    }
}
