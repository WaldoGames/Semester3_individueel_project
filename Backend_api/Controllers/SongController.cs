using Backend_core.Classes;
using Backend_core.DTO;
using Backend_DAL.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : Controller
    {
        SongService SongService;



        [HttpGet]
        public IActionResult GetSongsUsedInShow([FromQuery(Name = "show")] int ShowId)
        {
            SongService = new SongService(new ShowRepository(), new SongRepository(), new ArtistRepository());

            try
            {
                Result<SongsDto> SongsList = SongService.GetSongsUsedInShow(ShowId);
                if (SongsList.IsFailed)
                {
                    // Create the response message with an appropriate status code and error message
                    if (SongsList.IsFailedError)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Error occurred while processing the request."
                        };

                        // Return BadRequest with the error message
                        return BadRequest(response);
                    }
                    else if (SongsList.IsFailedWarning)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = SongsList.WarningMessage
                        };

                        // Return BadRequest with the error message
                        return BadRequest(response);
                    }
                }

                var songs = SongsList.Data.Songs
                  .Select(a => new { key = a.Id, Name = a.name, LastPlayed = a.LastPlayed })
                  .ToList();

                return Ok(songs);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally, you can return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public IActionResult AddNewSong(NewSongDto NewSong)
        {

            SongService = new SongService(new ShowRepository(), new SongRepository(), new ArtistRepository());

            SimpleResult result = SongService.PostNewSong(NewSong);

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

        [HttpPost]
        [Route("played")]
        public IActionResult PlaySongOnShow(PlaySongDto playSong)
        {

            SongService = new SongService(new ShowRepository(), new SongRepository(), new ArtistRepository());

            SimpleResult result = SongService.PostSongPlayed(playSong);

            if (result.IsFailedError)
            {
                return BadRequest();
            }
            return Ok();

        }



    }
}
