using Backend_core.Classes;
using Backend_core.DTO;
using Backend_DAL.Classes;
using Microsoft.AspNetCore.Mvc;

namespace Backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowController : Controller
    {
        ShowService showService { get; set; }

        [HttpPost]
        public IActionResult AddNewShow(NewShowDto NewShow)
        {
            showService = new ShowService(new ShowRepository(), new UserRepository());


            SimpleResult result = showService.CreateShow(NewShow);

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

        [HttpGet]
        public IActionResult GetShowsWithConnectionToUser(string AuthSub)
        {
            showService = new ShowService(new ShowRepository(), new UserRepository());

            Result<ShowsDto> result = showService.GetAllShowsWithConnectionToUser(AuthSub);

            if (result.IsFailedError)
            {
                return BadRequest();
            }
            if (result.IsFailedWarning)
            {
                return BadRequest(result.WarningMessage);
            }

            var songs = result.Data.shows
                  .Select(a => new { id = a.Id, name = a.show_name, discription = a.show_description, language = a.show_language })
                  .ToList();
            return Ok(songs);
        }
    }
}
