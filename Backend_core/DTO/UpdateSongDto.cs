﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class UpdateSongDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime Release_date { get; set; }

        public List<int> CreatorIds { get; set; }

        public int showId { get; set; }
        public string User_description { get; set; }
    }
}
