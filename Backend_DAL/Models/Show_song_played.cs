using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class Show_song_played
    {

        public int Id { get; set; }


        public DateTime timePlayed { get; set; }

        public Show show { get; set; }
        public Song song { get; set; }
        
    }
}
