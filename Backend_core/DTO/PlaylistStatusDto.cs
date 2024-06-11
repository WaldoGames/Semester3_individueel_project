using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class PlaylistStatusDto
    {
        public bool LastItem { get; set; }

        public bool FirstItem { get; set; }

        public string recordingPlayListName { get; set; }

        public string playListDescription { get; set; }

        public PlaylistItemStatusDto currentItem { get; set; }
    }
}
