using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class Song
    {
        public int song_id { get; set; }
        public string name { get; set; }

        public DateTime Release_date { get; set; }

        public string Information { get; set; }
        public ICollection<Artist> Creators { get; set; }
    }
}
