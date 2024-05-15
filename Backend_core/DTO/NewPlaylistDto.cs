using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class NewPlaylistDto
    {
        public string recordingPlayListName { get; set; }

        public int creatorId { get; set; }

        public List<NewPlaylistItemDto> playlistItems { get; set; }
    }
}
