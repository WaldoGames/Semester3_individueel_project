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
                    var response = new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Error occurred while processing the request."
                    };

                    // Return BadRequest with the error message
                    return BadRequest(response);
                }

            return Ok(artistService.getArtistsUsedInShow(param1).Data.Artists.Select(a => a.name));
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally, you can return a generic error response
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

    }
}
