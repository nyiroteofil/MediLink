using MediLink_BackEnd.Data;
using MediLink_BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediLink_BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserManagerController : Controller
    {
        private readonly MediLinkContext _dbContext;

        public UserManagerController(MediLinkContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddUser()
        {
            return Ok();   
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
