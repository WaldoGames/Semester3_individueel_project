using Backend_core.DTO;
using Backend_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    public class SongService
    {
        IShowRepository showRepository { get; set; }
        ISongRepository songRepository { get; set; }
        IArtistRepository artistRepository { get; set; }

        public SongService(IShowRepository showRepository, ISongRepository songRepository, IArtistRepository artistRepository)
        {
            this.showRepository = showRepository;
            this.songRepository = songRepository;
            this.artistRepository = artistRepository;
        }

        public Result<SongsDto> GetSongsUsedInShow(int showId)
        {
            Result<bool> showExists = showRepository.DoesShowExist(showId);

            if (showExists.IsFailedError)
            {
                return new NullableResult<SongsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from showrepositroy->DoesShowExist" };
            }
            if (showExists.Data == false)
            {
                return new NullableResult<SongsDto> { WarningMessage = "Show does not exist" };
            }
            Result<SongsDto> songs = songRepository.GetSongsUsedByShow(showId);

            if (songs.IsFailedError)
            {
                return new NullableResult<SongsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from SongRepository->GetSongsUsedByShow" };
            }

            return songs;
        }

        public SimpleResult PostSongPlayed(PlaySongDto playSong)
        {
            
            Result<bool> showExists = showRepository.DoesShowExist(playSong.showId);
            if (showExists.Data == false)
            {
                return new SimpleResult { WarningMessage = "show doesn't exist" };
            }
            if (showExists.IsFailed)
            {
                return new SimpleResult { ErrorMessage = "SongService->PostSongPlayed something went wrong with checking if the show exists" };
            }

            Result<bool> songExists = songRepository.DoesSongExist(playSong.songId);
            if (songExists.Data == false)
            {
                return new SimpleResult { WarningMessage = "song doesn't exist" };
            }
            if (songExists.IsFailed)
            {
                return new SimpleResult { ErrorMessage = "SongService->PostSongPlayed something went wrong with checking if the song exists" };
            }

            return songRepository.PostPlayedSong(playSong);
        }

        public SimpleResult PostNewSong(NewSongDto newSongDto)
        {
            try
            {
                foreach (int id in newSongDto.CreatorIds)
                {
                    Result<bool> artistResult = artistRepository.DoesArtistExist(id);

                    if (artistResult.IsFailedError)
                    {
                        return new SimpleResult() { ErrorMessage = artistResult.ErrorMessage };
                    }else if (!artistResult.Data)
                    {
                        return new SimpleResult() { WarningMessage = "one or more artist could not be added to the song. try again" };
                    }
                }
                Result<bool> showExists = showRepository.DoesShowExist(newSongDto.showId);
                    if (showExists.IsFailed) {
                        return new SimpleResult() { ErrorMessage = showExists.ErrorMessage};
                    }
                    else if(showExists.Data==false)
                    {
                        return new SimpleResult() { WarningMessage = "can't find your show. try again later" };
                    }

                Result<int> addSongResult= songRepository.PostNewSong(newSongDto);
                    if (addSongResult.IsFailedError)
                    {
                        return new SimpleResult { ErrorMessage = addSongResult.ErrorMessage };
                    }else if (addSongResult.IsFailedWarning)
                    {
                        return new SimpleResult { WarningMessage = addSongResult.WarningMessage };
                    }

                SimpleResult showSongResult = songRepository.AddSongToShow(newSongDto, addSongResult.Data);
                    if (showSongResult.IsFailedError)
                    {
                        return new SimpleResult { ErrorMessage = showSongResult.ErrorMessage };
                    }

                return new SimpleResult();
            }
            catch (Exception e)
            {
                return new SimpleResult() { ErrorMessage = "SongService->PostNewSong " + e.Message };
            }
        }
    }
}
