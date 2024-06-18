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
        public IActionResult AddNewShow(NewShowDto newShow)
        {
            showService = new ShowService(new ShowRepository(), new UserRepository());


            SimpleResult result = showService.CreateShow(newShow);

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
        public IActionResult GetShowsWithConnectionToUser(string authSub)
        {
            showService = new ShowService(new ShowRepository(), new UserRepository());

            Result<ShowsDto> result = showService.GetAllShowsWithConnectionToUser(authSub);

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

        [HttpDelete]
        public IActionResult DeleteShow(int ShowId)
        {

            showService = new ShowService(new ShowRepository(), new UserRepository());

            SimpleResult result = showService.RemovesShow(ShowId);

            if (result.IsFailedError)
            {
                return BadRequest();
            }
            return Ok();

        }
    }
}
