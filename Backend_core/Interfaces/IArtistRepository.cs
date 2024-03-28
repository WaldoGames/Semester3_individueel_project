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
        public Result<ArtistsDto> GetArtistsListFromSongList(List<int> songIds, int max, int Offset=0);
        public Result<ArtistsDto> GetArtistsFromSong(int songId);

        public Result<ArtistsDto> GetArtistsForSearch(string name);
        public NullableResult<ArtistDto> GetArtistById(int artistId);
        public SimpleResult AddNewArtist(NewArtistDto newArtist);
        public SimpleResult RemoveArtist(int artistId);
        public SimpleResult UpdateArtist(int artistId, UpdateArtistDto updateArtist);

        public Result<bool> DoesArtistExist(int artistId);
    }
}
