using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class PlaylistOverviewDto
    {
        public List<PlaylistOverviewDtoItem> playListItems { get; set; }

    }
    public class PlaylistOverviewDtoItem
    {
        public string playListName { get; set; }

        public string playListDescription { get; set; }
        public int playListId { get; set; }

    }
}
