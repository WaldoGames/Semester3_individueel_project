using Azure.Core;
using Backend_core.Classes;
using Backend_core.DTO;
using Backend_DAL.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Components;
using System.Web.Http;

namespace Backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : Controller
    {
        ArtistService artistService;

        [HttpGet]
        public IActionResult GetArtists([FromQuery(Name = "show")] int ShowId)
        {
            artistService = new ArtistService(new ShowRepository(), new SongRepository(), new ArtistRepository());

            try
            {
                Result<ArtistsDto> artistlist = artistService.getArtistsUsedInShow(ShowId);
                if (artistlist.IsFailed)
                {
                    if (artistlist.IsFailedError)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Error occurred while processing the request."
                        };

                        // Return BadRequest with the error message
                        return BadRequest(response);
                    }
                    else if (artistlist.IsFailedWarning)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = artistlist.WarningMessage
                        };
                    return BadRequest(response);
                    }
                }

                var artists = artistlist.Data.Artists
                   .Select(a => new ArtistResponse { key = a.Id, Name = a.name, playedCount = artistService.GetArtistPlayedCount(a.Id, ShowId).Data })
                   .ToList().OrderByDescending(a=>a.playedCount);

                return Ok(artists);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }
        [HttpGet]
        [Route("search")]
        public IActionResult GetArtistsByPartialName([FromQuery(Name = "search")] string name)
        {
            artistService = new ArtistService(new ShowRepository(), new SongRepository(), new ArtistRepository());

            try
            {
                Result<ArtistsDto> artistlist = artistService.getArtistsSearch(name.ToLower());
                if (artistlist.IsFailed)
                {
                    // Create the response message with an appropriate status code and error message
                    if (artistlist.IsFailedError)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Error occurred while processing the request."
                        };

                        // Return BadRequest with the error message
                        return BadRequest(response);
                    }
                    else if (artistlist.IsFailedWarning)
                    {
                        var response = new
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = artistlist.WarningMessage
                        };

                        // Return BadRequest with the error message
                        return BadRequest(response);
                    }
                }


                // var collection = new Dictionary<int, string>();

                if (artistlist.Data == null || artistlist.Data.Artists.Count == 0)
                {
                    return Ok();
                }

                var artists = artistlist.Data.Artists
                   .Select(a => new ArtistResponse { key = a.Id, Name = a.name })
                   .ToList();

                return Ok(artists);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally, you can return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public IActionResult AddArtist(NewArtistDto newArtists)
        {
            artistService = new ArtistService(new ShowRepository(), new SongRepository(),new ArtistRepository());

            SimpleResult result = artistService.addNewArtist(newArtists);

            if (result.IsFailedError)
            {
                return BadRequest();
            }
            return Ok();
        }
        private class ArtistResponse
        {
            public int key { get; set; }
            public string Name { get; set; }

            public int playedCount { get; set; }
        }

    }
}
