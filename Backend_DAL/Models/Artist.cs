﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class Artist
    {
        [Key]
        public int artist_id { get; set; }
        public string name { get; set; }

        ICollection<Song> songs { get; set; }
    }
}
