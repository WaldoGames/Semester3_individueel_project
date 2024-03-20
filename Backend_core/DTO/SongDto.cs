﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class SongDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime Release_date { get; set; }

        public string User_description { get; set; }
        public List<ArtistDto>? Creators { get; set; }
    }

    public class SongWithLastPlayedDto : SongDto
    {
        public int AmountPlayed { get; set; }
        public DateTime LastPlayed { get; set;}
    }
}
