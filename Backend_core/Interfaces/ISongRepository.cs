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
        public SimpleResult PostPlayedSong(PlaySongDto songPlayed);
        public Result<int> PostNewSong(NewSongDto newSongDto);

        public SimpleResult AddSongToShow(NewSongDto newSongDto, int songId);
        public SimpleResult AddSongToShow(NewShowSongConnectionDto newSongDto);
        public SimpleResult UpdateSong(UpdateSongDto newSongDto);

        public NullableResult<SongDto> GetSong(int songId);

    }
}
