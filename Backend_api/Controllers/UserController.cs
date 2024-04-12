using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace Backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        
        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser(NewUserDto user)
        {
            UserService service = new UserService(new UserRepository());

            SimpleResult result = service.RegisterLogin(user);

            if (result.IsFailedError)
            {
                return BadRequest();
            }
            if (result.IsFailedWarning)
            {
                return BadRequest(result.WarningMessage);
            }
            return Ok();
        }
    }
}
