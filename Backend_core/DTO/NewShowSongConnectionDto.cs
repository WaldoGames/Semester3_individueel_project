using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class NewShowSongConnectionDto
    {
        public int songId;
        public int showId { get; set; }
        public string User_description { get; set; }
    }
}
