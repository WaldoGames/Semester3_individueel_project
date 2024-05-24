using Backend_core.Classes;
using Backend_core.DTO;
using Backend_DAL.Classes;
using Backend_DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : Controller
    {
        SongService SongService;

        [HttpGet]
        [Route("tmp")]
        public IActionResult websocketTestCall([FromQuery(Name = "Message")] string Message)
        {

            return Ok();
        }

        [HttpGet]
        [Route("fromshow")]
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
                  .Select(a => new { key = a.Id, Name = a.name, LastPlayed = a.LastPlayed, AmountPlayed = a.AmountPlayed })
                  .ToList().OrderByDescending(s=>s.LastPlayed).ThenBy(s=>s.Name);

                return Ok(songs);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally, you can return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSongById([FromRoute(Name ="id")]int songId)
        {
            SongService = new SongService(new ShowRepository(), new SongRepository(), new ArtistRepository());

            NullableResult<SongDto> song = SongService.GetSongById(songId);


            if (song.IsFailed)
            {
                return BadRequest();
            }
            if (song.IsEmpty)
            {
                return NotFound();
            }
            return Ok(song.Data);

        }

        [HttpGet]
        [Route("search")]
        public IActionResult GetSongByPartialName([FromQuery(Name = "search")] string name)
        {
            SongService = new SongService(new ShowRepository(), new SongRepository(), new ArtistRepository());
            try
            {
                Result<SongsSimpleDto> Songs = SongService.getSongSearch(name.ToLower());
                if (Songs.IsFailed)
                {
                    // Create the response message with an appropriate status code and error message
                    if (Songs.IsFailedError)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Error occurred while processing the request."
                        };

                        // Return BadRequest with the error message
                        return BadRequest(response);
                    }
                    else if (Songs.IsFailedWarning)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = Songs.WarningMessage
                        };

                        // Return BadRequest with the error message
                        return BadRequest(response);
                    }
                }


                // var collection = new Dictionary<int, string>();

                if (Songs.Data == null || Songs.Data.Songs.Count == 0)
                {
                    return Ok();
                }

                var songs = Songs.Data.Songs
                   .Select(a => new { id = a.Id, Name = a.name })
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

        [HttpGet]
        [Route("{id}/show/{showId}")]
        public IActionResult GetSongByIdWithShowConnection([FromRoute(Name = "id")] int songId, [FromRoute(Name = "showId")] int showId)
        {
            SongService = new SongService(new ShowRepository(), new SongRepository(), new ArtistRepository());
            NullableResult<SongDto> song = SongService.GetSongById(songId);

            if (song.IsFailed) return BadRequest();
            
            if (song.IsEmpty) return NotFound();
            
            NullableResult<SongWithShowConnectionDto> ShowSong = SongService.AddInformationToSongDto(song.Data, showId);

            if (ShowSong.IsFailed) return BadRequest();
            
            if (ShowSong.IsEmpty) return NotFound();
            
            return Ok(ShowSong.Data);

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
        [HttpPut]

        public IActionResult UpdateSong(UpdateSongDto NewSong)
        {

            SongService = new SongService(new ShowRepository(), new SongRepository(), new ArtistRepository());

            SimpleResult result = SongService.UpdateSong(NewSong);

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
