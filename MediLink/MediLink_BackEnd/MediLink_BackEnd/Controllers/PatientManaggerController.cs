using MediLink_BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediLink_BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientManaggerController : ControllerBase
    {
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
        public IActionResult CreateReferal(int userId, Referal referal)
        {
            return Ok(HttpStatusCode.Created);
        }

        [HttpPatch]
        public IActionResult UpdateReferal(int userId, Referal referal)
        {
            return Ok();
        }
    }
}
