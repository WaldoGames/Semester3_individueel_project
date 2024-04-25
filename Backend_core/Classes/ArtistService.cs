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

        SongService SongService { get; set; }

        public ArtistService(IShowRepository showRepository, ISongRepository songRepository, IArtistRepository artistRepository)
        {
            ShowRepository = showRepository;
            SongRepository = songRepository;
            ArtistRepository = artistRepository;

            SongService = new SongService(showRepository, songRepository, artistRepository);
        }

        
         
        public Result<int> GetArtistPlayedCount(int artistId, int showId)
        {
            Result<SongsDto> songs = SongService.GetSongsUsedInShow(showId);

            if (songs.IsFailedError)
            {
                return new Result<int> { ErrorMessage = "core->ArtistService->GetArtistPlayedCount error taken from SongService.GetSongsUsedInShow" };
            }
            if (songs.IsFailedWarning)
            {
                return new Result<int> { WarningMessage = songs.WarningMessage };
            }

            int count = songs.Data.Songs.Where(s => s.Artists.Select(c => c.Id).Contains(artistId)).Sum(s => s.AmountPlayed);



            return new Result<int> { Data = count };

        }

        public Result<ArtistsDto> getArtistsUsedInShow(int showId)
        {

            Result<SongsDto> songs = SongService.GetSongsUsedInShow(showId);

            if (songs.IsFailedError)
            {
                return new NullableResult<ArtistsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from SongService.GetSongsUsedInShow" };
            }
            if(songs.IsFailedWarning)
            {
                return new NullableResult<ArtistsDto> { WarningMessage = songs.WarningMessage };
            }
            Result<ArtistsDto> artists= ArtistRepository.GetArtistsListFromSongList(songs.Data.Songs.Select(s => s.Id).ToList(), 20);
            
            if (artists.IsFailedError)
            {
                return new NullableResult<ArtistsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from ArtistRepository->GetArtistsListFromSongList" };
            }

            return new NullableResult<ArtistsDto> { Data = artists.Data };

        }

        public Result<ArtistsDto> getArtistsSearch(string name)
        {

            Result<ArtistsDto> artists = ArtistRepository.GetArtistsForSearch(name);

            if (artists.IsFailedError)
            {
                return new Result<ArtistsDto> { ErrorMessage = "core->ArtistService->getArtistsUsedInShow error taken from ArtistRepository->GetArtistsListFromSongList" };
            }

            return new Result<ArtistsDto> { Data = artists.Data };

        }
        public SimpleResult addNewArtist(NewArtistDto newArtists)
        {
            return ArtistRepository.AddNewArtist(newArtists);
        }
    }
}
