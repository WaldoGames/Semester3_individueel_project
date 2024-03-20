using Backend_core.Classes;
using Backend_core.DTO;
using Backend_DAL.Classes;
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
        [Route("/getSongs")]
        public IActionResult GetSongsUsedInShow(int ShowId)
        {
            SongService = new SongService(new ShowRepository(), new SongRepository());

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


                var collection = new Dictionary<int, string>();

                var songs = SongsList.Data.Songs
                  .Select(a => new { key = a.Id, Name = a.name })
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
        [Route("/playSongOnShow")]
        public IActionResult PlaySongOnShow(PlaySongDto playSong){

            SongService = new SongService(new ShowRepository(), new SongRepository());

            SimpleResult result = SongService.PostSongPlayed(playSong);

            if (result.IsFailedError)
            {
                return BadRequest();
            }
            return Ok();

        }


        
    }
}
