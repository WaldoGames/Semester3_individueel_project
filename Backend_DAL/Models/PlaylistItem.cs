using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class PlaylistItem
    {
        public int playlist_id { get; set; }
        public string discription { get; set; }
        public RecordingPlaylist playlist { get; set; }
    }
}
