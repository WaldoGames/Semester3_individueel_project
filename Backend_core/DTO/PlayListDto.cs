using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.DTO
{
    internal class PlayListDto
    {
        public int Id { get; set; }

        public string recordingPlayListName { get; set; }

        public int creatorId { get; set; }

        PlayListItemDto items { get; set; }
    }
}
