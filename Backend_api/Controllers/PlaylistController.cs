using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistController : Controller
    {

        PlayListService playListService;

        [HttpPost]
        public IActionResult AddPlaylist(NewPlaylistDto newPlaylist)
        {
            playListService = new PlayListService(new PlaylistRepository());

            SimpleResult result = playListService.CreatePlaylist(newPlaylist);

            //check for warning about show exisiting

            if (result.IsFailed)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPlaylists([FromQuery(Name = "show")] int ShowId)
        {
            playListService = new PlayListService(new PlaylistRepository());
            
            try
            {
                Result<PlaylistOverviewDto> playlists = playListService.GetPlaylists(ShowId);
                if (playlists.IsFailed)
                {
                    return BadRequest();
                }

                return Ok(playlists.Data);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPlaylistById([FromRoute(Name = "id")] int PlaylistId)
        {
            playListService = new PlayListService(new PlaylistRepository());

            NullableResult<PlayListDto> playlist = playListService.GetPlaylist(PlaylistId);


            if (playlist.IsFailed)
            {
                return BadRequest();
            }
            if (playlist.IsEmpty)
            {
                return NotFound();
            }
            return Ok(playlist.Data);

        }

    }
}
