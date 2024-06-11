using Backend_core.Classes;
using Backend_core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Interfaces
{
    public interface IPlaylistRepository
    {
        public SimpleResult CreatePlaylistItem(NewPlaylistItemDto newPlaylistItemDto);

        public Result<int> CreatePlaylist(NewPlaylistDto newPlaylistDTO);

        public SimpleResult UpdatePlaylist(UpdatePlaylistDto updatePlaylistDTO);

        public SimpleResult RemovePlaylist(int playlistId);

        public SimpleResult RemovePlaylistWithSong(int songId);
        //RemovePlaylistWithSong

        public NullableResult<PlayListDto> GetPlaylist(int playlistId);
        public Result<PlaylistOverviewDto> GetPlaylistsOverview(int showId);
    }
}
