using Backend_core.DTO;
using Backend_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    public class ArtistService
    {
        IShowRepository ShowRepository { get; set; }
        ISongRepository SongRepository { get; set; }
        IArtistRepository ArtistRepository { get; set; }

        public ArtistService(IShowRepository showRepository, ISongRepository songRepository, IArtistRepository artistRepository)
        {
            ShowRepository = showRepository;
            SongRepository = songRepository;
            ArtistRepository = artistRepository;
        }

        public Result<ArtistsDto> getArtistsUsedInShow(int showId)
        {
            Result<bool> showExists = ShowRepository.DoesShowExist(showId);

            if (showExists.IsFailed)
            {
                return new Result<ArtistsDto> { ErrorMessage= "core->ArtistService->getArtistsUsedInShow error taken from showrepositroy->DoesShowExist" };
            }
            Result<SongsDto> songs = SongRepository.GetSongsUsedByShow(showId);

            if (songs.IsFailed)
            {
                return new Result<ArtistsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from SongRepository->GetSongsUsedByShow" };
            }
            Result<ArtistsDto> artists= ArtistRepository.GetArtistsListFromSongList(songs.Data.Songs.Select(s => s.Id).ToList(), 20);
            
            if (artists.IsFailed)
            {
                return new Result<ArtistsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from ArtistRepository->GetArtistsListFromSongList" };
            }

            return new Result<ArtistsDto> { Data = artists.Data };

        }
    }
}
