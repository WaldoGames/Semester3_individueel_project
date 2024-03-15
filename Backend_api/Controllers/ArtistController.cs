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
        [Route("/get")]
        public IActionResult GetArtists(int param1)
        {
            artistService = new ArtistService(new ShowRepository(), new SongRepository(), new ArtistRepository());

            try
            {
                Result<ArtistsDto> artistlist = artistService.getArtistsUsedInShow(param1);
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

                
                var collection = new Dictionary<int, string>();

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
        private class ArtistResponse
        {
            public int key { get; set; }
            public string Name { get; set; }
        }

    }
}
