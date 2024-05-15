using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    public partial class PlaylistItem
    {

        public int Id { get; set; }

        public int orderIndex { get; set; }
        public string discription { get; set; }
        public virtual RecordingPlaylist playlist { get; set; }
        public virtual Song? playlistItemSong { get; set; }
    }
}
