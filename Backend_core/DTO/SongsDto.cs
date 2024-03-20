using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class SongsDto
    {
        public List<SongWithLastPlayedDto> Songs { get; set; } = new List<SongWithLastPlayedDto>();
    }
}
