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
        
        // First of all, this controller needs to be finished with at least
        // the basic CRUD functions. Second of all, what is the only user that
        // should be able to manage registered institutions? It is obviously the administrator
        // thus the few controllers written here could be put into the AdministratorController.cs class
    }
}
