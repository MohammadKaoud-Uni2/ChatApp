using ChatApp.Api.Data;
using ChatApp.Api.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly UserManager<CustomUser> _context;
        public BuggyController(UserManager<CustomUser> context)
        {
            _context = context;            
        }
        [HttpGet]
        [Route("GetServerError")]
        public async Task<ActionResult<string>> getServerError()
        {

          var user=  _context.Users.First(x => x.Id == "13");
            return user.ToString();
        }
        [HttpGet]
        [Route("GetErrorNotfound")]
        public async Task<IActionResult> GetErrorNotFound()
        {
            return NotFound("Not Found the Specific Data With This Related Type");
        }
        
        [HttpGet]
        [Route("Secret")]
        public async Task<ActionResult<string>> GetSecretError()
        {
            return "secretCannotBeMatched";
        }
        [HttpGet]
        [Route("UnAuthorized")]

        public async Task<IActionResult> GetUnAuthorized()
        {
            return Unauthorized("Cannot get the permission");
        }
        
    }
}
