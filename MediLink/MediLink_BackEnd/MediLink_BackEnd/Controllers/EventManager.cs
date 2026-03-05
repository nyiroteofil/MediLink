using MediLink_BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediLink_BackEnd.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EventManager : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsersEvents(int userId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateUserAppointment(int userId, Appointment appointment)
        {
            return Ok(HttpStatusCode.Created);
        }

        [HttpPatch]
        public IActionResult UpdateAppointment(int userId, Appointment appointment)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateMedicationReminder(int userId, MedicationReminder reminder)
        {
            return Ok(HttpStatusCode.Created);
        }

        [HttpPost]
        public IActionResult CreateCustomEvent(int userId, CustomEvent appointment)
        {
            return Ok(HttpStatusCode.Created);
        }

        [HttpPatch]
        public IActionResult UpdateReminder(int userId, Appointment appointment)
        {
            return Ok();
        }
    }
}
