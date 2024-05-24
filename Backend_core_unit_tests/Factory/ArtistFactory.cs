using Backend_core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests.Factory
{
    public class ArtistFactory
    {
        int id = 1;
        public ArtistDto GenerateFakeArtist(string name)
        {
            ArtistDto artist = new ArtistDto()
            {
                name = name,
                Id = id,
            };

            id++;

            return artist;
        }
    }
}
