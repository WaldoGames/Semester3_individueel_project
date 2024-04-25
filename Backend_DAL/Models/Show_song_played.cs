using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    public partial class Show_song_played
    {

        public int Id { get; set; }


        public DateTime timePlayed { get; set; }

        public virtual Show show { get; set; }
        public virtual Song song { get; set; }
        
    }
}
