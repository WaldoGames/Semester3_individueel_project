using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class ArtistDto
    {
        public int Id { get; set; }
        public string name { get; set; }

        public ICollection<SongDto> songs { get; set; }
    }
}
