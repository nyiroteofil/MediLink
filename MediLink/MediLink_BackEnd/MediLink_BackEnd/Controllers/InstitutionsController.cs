using MediLink_BackEnd.Data;
using MediLink_BackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediLink_BackEnd.Models;
using System.Net;

namespace MediLink_BackEnd.Controllers
{
    [Route("api/Institutions/")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {

        private readonly MediLinkContext _context;
        private readonly IInstitutionService _institutionService;

        public InstitutionsController(MediLinkContext context, IInstitutionService service)
        {
            _context = context;
            _institutionService = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddInstitution(Institution institution)
        {
            try
            {
                await _institutionService.AddInstiution(institution);
                return Ok(institution);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return StatusCode(500, "\"An internal server error occurred.\"");
            }
        }
    }
}
