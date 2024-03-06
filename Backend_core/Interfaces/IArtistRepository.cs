using Backend_core.Classes;
using Backend_core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Interfaces
{
    public interface IArtistRepository
    {
        public Result<ArtistsDto> GetArtistsUsedInShow(int userId, int max);
        public Result<ArtistsDto> GetArtistsUsedInShow(int userId, int max, int Offset);
        public NullableResult<ArtistDto> GetArtistById(int artistId);
        public SimpleResult AddNewArtist(NewArtistsDto newArtist);
        public SimpleResult RemoveArtist(int artistId);
        public SimpleResult UpdateArtist(int artistId, UpdateArtistDto updateArtist);
    }
}
