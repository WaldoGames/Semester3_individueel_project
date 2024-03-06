using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class User
    {
        [Key]
        public int user_id { get; set; }

        // Navigation property for playlists created by the user
        public ICollection<RecordingPlaylist> CreatedPlaylists { get; set; }

        // Navigation property for playlists where the user is a guest
        public ICollection<RecordingPlaylist> RecordingGuests { get; set; }

        public ICollection<Show> Shows { get; set; }
        public ICollection<User_contact> Contacts { get; set; }
    }
}
