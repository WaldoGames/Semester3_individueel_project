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
        public SimpleResult createPlaylistItem(NewPlaylistItemDto newPlaylistItemDto);

        public Result<int> createPlaylist(NewPlaylistDto newPlaylistDTO);

        public SimpleResult updatePlaylist(UpdatePlaylistDto updatePlaylistDTO);
    }
}
