using Backend_core.DTO;
using Backend_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    public class PlayListService
    {
        IPlaylistRepository playlistRepository;

        public PlayListService(IPlaylistRepository playlistRepository)
        {
            this.playlistRepository = playlistRepository;
        }

        public Result<PlayListDto> GetPlaylist(int playlistId)
        {

        }

        public SimpleResult CreatePlaylist(NewPlaylistDto newPlaylist)
        {      
            Result<int> result = playlistRepository.createPlaylist(newPlaylist);

            if (result.IsFailed)
            {
                return result;
            }
            foreach (var item in newPlaylist.playlistItems)
            {
                item.playlistId = result.Data;
            }
            foreach (NewPlaylistItemDto item in newPlaylist.playlistItems)
            {
                SimpleResult itemResult = playlistRepository.createPlaylistItem(item);
                if (itemResult.IsFailed)
                {
                    return new SimpleResult { ErrorMessage = "an error occured during the creation of the playlist items" };
                };
            }
            return new SimpleResult();

        }
        public SimpleResult UpdatePlaylist(UpdatePlaylistDto updatePlaylist)
        {
            return playlistRepository.updatePlaylist(updatePlaylist);
        }

        public void ResetPlayListOrderIndex(List<PlayListItemDto> playlist){

            for(int i = 0; i < playlist.Count; i++)
            {
                playlist[i].orderIndex = i;
            }
        }
    }
}
