using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class ArtistDto
    {
        public ArtistDto() { }
        public ArtistDto(int id, string name)
        {
            this.Id = id;
            this.name = name;
        }
        public int Id { get; set; }
        public string name { get; set; }

        //public ICollection<SongDto> songs { get; set; }
    }
}
