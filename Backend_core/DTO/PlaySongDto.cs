using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class PlaySongDto
    {
        public int songId {  get; set; }
        public int showId { get; set; }
        public DateTime timePlayed { get; set; } = DateTime.Now;
    }
}
