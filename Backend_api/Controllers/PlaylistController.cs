﻿using Backend_core.Classes;
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
            playListService = new PlayListService(new PlaylistRepository(), new SongRepository(), new ShowRepository());

            SimpleResult result = playListService.CreatePlaylist(newPlaylist);

            //check for warning about show exisiting

            if (result.IsFailed)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPlaylists([FromQuery(Name = "show")] int showId)
        {
            playListService = new PlayListService(new PlaylistRepository(), new SongRepository(), new ShowRepository());

            try
            {
                Result<PlaylistOverviewDto> playlists = playListService.GetPlaylists(showId);
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
        public IActionResult GetPlaylistById([FromRoute(Name = "id")] int playlistId)
        {
            playListService = new PlayListService(new PlaylistRepository(), new SongRepository(), new ShowRepository());

            NullableResult<PlayListDto> playlist = playListService.GetPlaylist(playlistId);


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
        [HttpGet]
        [Route("item/{id}")]
        public IActionResult GetPlaylistItemById([FromRoute(Name = "id")] int playlistId)
        {
            playListService = new PlayListService(new PlaylistRepository(), new SongRepository(), new ShowRepository());

            NullableResult<PlayListDto> playlist = playListService.GetPlaylist(playlistId);


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

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePlaylistById([FromRoute(Name = "id")] int playlistId)
        {
            playListService = new PlayListService(new PlaylistRepository(), new SongRepository(), new ShowRepository());

            SimpleResult result = playListService.RemovePlaylist(playlistId);

            //check for warning about show exisiting

            if (result.IsFailed)
            {
                return BadRequest();
            }
            return Ok();

        }

    }
}
