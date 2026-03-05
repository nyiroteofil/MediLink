using MediLink_BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediLink_BackEnd.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUserMessage(int userId, Appointment appointment)
        {
            return Ok(HttpStatusCode.Created);
        }

        [HttpPatch]
        public IActionResult UpdateMessage(int userId, Appointment appointment)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateGroupMessage(int userId, MedicationReminder reminder)
        {
            return Ok(HttpStatusCode.Created);
        }

        [HttpPost]
        public IActionResult CreateCustomExample(int userId, CustomEvent appointment)
        {
            return Ok(HttpStatusCode.Created);
        }

        [HttpPatch]
        public IActionResult UpdateExample(int userId, Appointment appointment)
        {
            return Ok();
        }
    }
}
