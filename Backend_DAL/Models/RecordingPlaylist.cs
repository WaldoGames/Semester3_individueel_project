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

        public virtual Show Show { get; set; }

        public virtual ICollection<PlaylistItem> PlaylistItems { get; set; }

    }
}
