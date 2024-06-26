﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    public partial class Song
    {

        public int Id { get; set; }
        public string name { get; set; }

        public DateTime Release_date { get; set; }

        //public string Information { get; set; }
        public virtual ICollection<Artist> Artists { get; set; } = new List<Artist>();

        public virtual ICollection<Show_song_played> SongsPlayed { get; set; }

        public virtual ICollection<Show_song> Shows { get; set; }
    }
}
