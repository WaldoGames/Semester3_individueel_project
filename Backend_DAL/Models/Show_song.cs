using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Models
{
    internal class Show_song
    {
        public int Id { get; set; }

        public Show Show { get; set; }
        public Song Song { get; set; }

        public string Information { get; set; }

    }
}
