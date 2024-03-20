using Backend_core.DTO;
using Backend_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    public class SongService
    {
        IShowRepository ShowRepository { get; set; }
        ISongRepository SongRepository { get; set; }

        public SongService(IShowRepository showRepository, ISongRepository songRepository)
        {
            ShowRepository = showRepository;
            SongRepository = songRepository;
        }

        public Result<SongsDto> GetSongsUsedInShow(int showId)
        {
            Result<bool> showExists = ShowRepository.DoesShowExist(showId);

            if (showExists.IsFailedError)
            {
                return new NullableResult<SongsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from showrepositroy->DoesShowExist" };
            }
            if (showExists.Data == false)
            {
                return new NullableResult<SongsDto> { WarningMessage = "Show does not exist" };
            }
            Result<SongsDto> songs = SongRepository.GetSongsUsedByShow(showId);

            if (songs.IsFailedError)
            {
                return new NullableResult<SongsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from SongRepository->GetSongsUsedByShow" };
            }

            return songs;
        }

        public SimpleResult PostSongPlayed(PlaySongDto playSong)
        {
            return SongRepository.PostPlayedSong(playSong);
        }
    }
}
