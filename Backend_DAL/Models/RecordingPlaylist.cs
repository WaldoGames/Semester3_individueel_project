using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class RecordingPlaylist
    {
        public int recordingPlaylist_id { get; set; }

        public User Creator { get; set; }
        public ICollection<User> guests { get; set; }
        public ICollection<PlaylistItem> playlistItems { get; set; }
    }
}
