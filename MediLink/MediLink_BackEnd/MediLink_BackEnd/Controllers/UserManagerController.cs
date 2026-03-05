using MediLink_BackEnd.Data;
using Microsoft.AspNetCore.Mvc;

namespace MediLink_BackEnd.Controllers
{

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
        public IActionResult ChangeUserStatus(int UserId)
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
