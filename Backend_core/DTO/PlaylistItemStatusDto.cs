using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class PlaylistItemStatusDto
    {
        public string discription { get; set; }

        public int itemIndex { get; set; }
        public SongWithShowConnectionDto? song { get; set; }
    }
}
