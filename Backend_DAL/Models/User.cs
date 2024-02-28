using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class User
    {
        public int user_id { get; set; }

        public ICollection<RecordingPlaylist> CreatedPlayLists { get; set; }

        //todo figure out how to use contacts
        public ICollection<User_contact> Contacts { get; set; }
    }
}
