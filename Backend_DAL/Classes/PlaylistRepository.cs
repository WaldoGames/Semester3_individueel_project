using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Models;
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
            User? u = context.Users.Where(u => u.Id == newPlaylistDTO.creatorId).FirstOrDefault();

            if (u == null)
            {
                return new Result<int> { WarningMessage = "User doesn't exist" };
            }

            recordingPlaylist.User = u;

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
                playlistItem.discription = newPlaylistItemDto.discription;
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

        public SimpleResult updatePlaylist(UpdatePlaylistDto updatePlaylistDTO)
        {
            throw new NotImplementedException();
        }
    }
}
