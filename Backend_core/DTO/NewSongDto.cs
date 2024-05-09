using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class NewSongDto
    {

        public string name { get; set; }
        public DateTime Release_date { get; set; }

        public ICollection<int> CreatorIds { get; set; } = new List<int>();

        public int showId { get; set; }
        public string User_description { get; set; }


    }
}
