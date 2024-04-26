using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    public partial class Show_song
    {
        public int Id { get; set; }

        public int ShowId { get; set; }

        public int SongId { get; set; }


        public virtual Show Show { get; set; }
        public virtual Song Song { get; set; }

        public string Information { get; set; }

    }
}
