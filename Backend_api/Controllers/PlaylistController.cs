using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
using Microsoft.AspNetCore.Mvc;

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

            if (result.IsFailedError)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
