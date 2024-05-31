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

        ISongRepository songRepository;

        IShowRepository showRepository;

        public PlayListService(IPlaylistRepository playlistRepository, ISongRepository songRepository, IShowRepository showRepository)
        {
            this.playlistRepository = playlistRepository;
            this.songRepository = songRepository;
            this.showRepository = showRepository;
        }

        public NullableResult<PlayListDto> GetPlaylist(int playlistId)
        {
            return playlistRepository.getPlaylist(playlistId);
        }
        public Result<PlaylistOverviewDto> GetPlaylists(int showId)
        {
            return playlistRepository.getPlaylistsOverview(showId);
        }

        public Result<PlaylistStatusDto> WebGetPlaylistStatus(int playlistId, int showId, int index)
        {
            PlaylistStatusDto status = new PlaylistStatusDto();

            NullableResult<PlayListDto> Getplaylistresult = playlistRepository.getPlaylist(playlistId);
            if (Getplaylistresult.IsFailed || Getplaylistresult.IsEmpty)
            {
                return new Result<PlaylistStatusDto> { ErrorMessage = "playlist services->WebGetPlaylistStatus: error from playlistRepository.getPlaylist()" };
            }
            if (Getplaylistresult.Data.creatorId != showId)
            {
                return new Result<PlaylistStatusDto> { WarningMessage = "playlist services->WebGetPlaylistStatus: error playlist does not belong to user" };
            }
            NullableResult<SongDto> songResult;
            if (Getplaylistresult.Data.items[index].playlistItemSongId != null) {
                songResult = songRepository.GetSong((int)Getplaylistresult.Data.items[index].playlistItemSongId);
            }
            else
            {
                songResult = new NullableResult<SongDto>();
            }

            status.playListDescription = Getplaylistresult.Data.playListDescription;
            status.recordingPlayListName = Getplaylistresult.Data.recordingPlayListName;
            if(Getplaylistresult.Data.items.Count > 0) { 
                status.currentItem = new PlaylistItemStatusDto {
                    discription = Getplaylistresult.Data.items[index].discription,
                    itemIndex = index,
                };

                if (!songResult.IsEmpty)
                {
                    status.currentItem.song = new SongWithShowConnectionDto
                    {
                        Artists = songResult.Data.Artists,
                        Id = songResult.Data.Id,
                        name = songResult.Data.name,
                        Release_date = songResult.Data.Release_date,
                    };
                    NullableResult<string> ShowSongResult = showRepository.GetShowDiscriptionOfSong(songResult.Data.Id, showId);
                    if (!ShowSongResult.IsEmpty)
                    {
                        status.currentItem.song.User_description = ShowSongResult.Data;
                    }
                }
            }
            if (index + 1 >= Getplaylistresult.Data.items.Count)
            {
                status.LastItem = true;
            }
            else
            {
                status.LastItem = false;
            }
            if (index == 0)
            {
                status.FirstItem = true;
            }
            else
            {
                status.FirstItem = false;
            }
            

            return new Result<PlaylistStatusDto> { Data = status };
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

        public SimpleResult RemovePlaylist(int playlistId)
        {
            return playlistRepository.removePlaylist(playlistId);
        }

        

        public void ResetPlayListOrderIndex(List<PlayListItemDto> playlist){

            for(int i = 0; i < playlist.Count; i++)
            {
                playlist[i].orderIndex = i;
            }
        }
    }
}
