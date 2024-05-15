using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    public class PlayListDto
    {
        public int Id { get; set; }

        public string recordingPlayListName { get; set; }

        public int creatorId { get; set; }

        public List<PlayListItemDto> items { get; set; }
    }
}
