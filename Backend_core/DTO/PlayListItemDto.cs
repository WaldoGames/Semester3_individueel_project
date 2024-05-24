using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class PlayListItemDto
    {
        public int Id { get; set; }
        public int orderIndex { get; set; }
        public string discription { get; set; }
        public int playlistId { get; set; }
        public int? playlistItemSongId { get; set; }
        public bool HasSong { get { if (playlistItemSongId == null) { return false; } else return true; } }
    }
}
