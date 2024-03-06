using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class RecordingPlaylist
    {
        [Key]
        public int recordingPlaylist_id { get; set; }

        // Foreign key
        public int creatorId { get; set; }

        // Navigation property
        [ForeignKey("creatorId")]
        public User Creator { get; set; }

        public ICollection<User> Guests { get; set; }
        public ICollection<PlaylistItem> PlaylistItems { get; set; }

    }
}
