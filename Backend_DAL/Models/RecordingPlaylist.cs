using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    public partial class RecordingPlaylist
    {
       
        public int Id { get; set; }

        public string recordingPlayListName { get; set; }

        // Foreign key
        public int creatorId { get; set; }

        // Navigation property
        [ForeignKey("creatorId")]
        public virtual User Creator { get; set; }

        public virtual ICollection<User> Guests { get; set; }
        public virtual ICollection<PlaylistItem> PlaylistItems { get; set; }

    }
}
