using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Classes
{
    public class PlaylistRepository : IPlaylistRepository
    {
        readonly MusicAppContext context;

        public PlaylistRepository()
        {
            context = new MusicAppContext();
        }
        public PlaylistRepository(MusicAppContext context)
        {
            this.context = context;
        }

        public Result<int> createPlaylist(NewPlaylistDto newPlaylistDTO)
        {
            RecordingPlaylist recordingPlaylist = new RecordingPlaylist();

            recordingPlaylist.recordingPlayListName = newPlaylistDTO.recordingPlayListName;
            recordingPlaylist.playListDescription = newPlaylistDTO.playListDescription;
            Show? u = context.Shows.Where(u => u.Id == newPlaylistDTO.ShowId).FirstOrDefault();

            if (u == null)
            {
                return new Result<int> { WarningMessage = "show doesn't exist" };
            }

            recordingPlaylist.Show = u;

            context.Recordings.Add(recordingPlaylist);
            context.SaveChanges();
            return new Result<int> { Data = recordingPlaylist.Id};
        }

        public SimpleResult createPlaylistItem(NewPlaylistItemDto newPlaylistItemDto)
        {
            try
            {
                PlaylistItem playlistItem = new PlaylistItem();

                RecordingPlaylist? p = context.Recordings.Where(u => u.Id == newPlaylistItemDto.playlistId).FirstOrDefault();

                if (p == null)
                {
                    return new Result<int> { WarningMessage = "User doesn't exist" };
                }

                playlistItem.playlist = p;
                playlistItem.discription = newPlaylistItemDto.description;
                playlistItem.orderIndex = newPlaylistItemDto.orderIndex;
                playlistItem.playlistItemSong = context.Songs.Where(s => s.Id == newPlaylistItemDto.playlistItemSongId).FirstOrDefault();

                context.PlaylistItems.Add(playlistItem);
                context.SaveChanges();
                return new SimpleResult{ };
            }
            catch (Exception e)
            {
                return new SimpleResult { ErrorMessage="PlaylistRepository->createPlaylistItem error: " + e.Message };
            }
        }

        public NullableResult<PlayListDto> getPlaylist(int playlistId)
        {
            try
            {
                RecordingPlaylist? playlist = context.Recordings.Include(p=>p.Show).Include(p => p.PlaylistItems).ThenInclude(u=>u.playlistItemSong).Where(p => p.Id == playlistId).FirstOrDefault();
                if(playlist == null)
                {
                    return new NullableResult<PlayListDto> { WarningMessage = "PlaylistRepository->getPlaylist playlist not found" };
                }

                List<PlayListItemDto> items = new List<PlayListItemDto>();

                foreach (PlaylistItem item in playlist.PlaylistItems)
                {
                    items.Add(new PlayListItemDto { discription = item.discription, Id = item.Id, orderIndex = item.orderIndex, playlistId = item.playlist.Id, playlistItemSongId = item.playlistItemSong?.Id });
                }
                return new NullableResult<PlayListDto> { Data = new PlayListDto
                {
                    Id = playlist.Id,
                    creatorId = playlist.Show.Id,
                    recordingPlayListName = playlist.recordingPlayListName,
                    items = items 
                }
                };
            }
            catch (Exception e)
            {
                return new NullableResult<PlayListDto> { ErrorMessage = "PlaylistRepository->getPlaylist error: " + e.Message };
            }
        }

        public Result<PlaylistOverviewDto> getPlaylistsOverview(int showId)
        {
            try
            {
                List<RecordingPlaylist> playlist = context.Recordings.Include(p => p.Show).Include(p => p.PlaylistItems).ThenInclude(u => u.playlistItemSong).Where(p => p.Show.Id == showId).ToList();

                PlaylistOverviewDto overviewDtos = new PlaylistOverviewDto ();
                overviewDtos.playListItems = new List<PlaylistOverviewDtoItem>();

                foreach (RecordingPlaylist item in playlist)
                {
                    overviewDtos.playListItems.Add(new PlaylistOverviewDtoItem { playListName = item.recordingPlayListName, playListId = item.Id });
                }
                return new Result<PlaylistOverviewDto> { Data= overviewDtos };
            }
            
            catch (Exception e)
            {
                return new Result<PlaylistOverviewDto> { ErrorMessage = "PlaylistRepository->getPlaylistsOverview error: " + e.Message };
            }
        }

        public SimpleResult removePlaylist(int playlistId)
        {
            try
            {
                List<PlaylistItem> items = context.PlaylistItems.Include(p => p.playlist).Where(p => p.playlist.Id == playlistId).ToList();

                foreach (PlaylistItem item in items)
                {
                    context.Remove(item);
                }
                context.Remove(context.Recordings.Where(p => p.Id == playlistId).FirstOrDefault());
                context.SaveChanges();
                return new SimpleResult();
            }
            catch (Exception e)
            {
                return new SimpleResult { ErrorMessage= "PlaylistRepository->removePlaylist: "+e.Message };
            }
        }

        public SimpleResult updatePlaylist(UpdatePlaylistDto updatePlaylistDTO)
        {
            throw new NotImplementedException();
        }
    }
}
