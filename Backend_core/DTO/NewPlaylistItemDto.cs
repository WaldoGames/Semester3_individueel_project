using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class NewPlaylistItemDto
    {
        public int orderIndex { get; set; }
        public string discription { get; set; }
        public int? playlistId { get; set; }
        public int? playlistItemSongId { get; set; }
        public bool HasSong { get { if (playlistItemSongId == null) { return false; } else return true; } }
    }
}
