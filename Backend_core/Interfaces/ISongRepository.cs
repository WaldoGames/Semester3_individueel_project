using Backend_core.Classes;
using Backend_core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Interfaces
{
    public interface ISongRepository
    {
        public Result<SongsDto> GetSongsUsedByShow(int showId);

        public Result<bool> DoesSongExist(int songId);
    }
}
