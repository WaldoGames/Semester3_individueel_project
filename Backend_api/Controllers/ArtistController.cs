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
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

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
        }

    }
}
