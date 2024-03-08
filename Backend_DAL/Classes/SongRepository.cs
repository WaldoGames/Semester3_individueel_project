using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Classes
{
    public class SongRepository : ISongRepository
    {
        MusicAppContext context = new MusicAppContext();

        public Result<bool> DoesSongExist(int songId)
        {
            try
            {
                bool exists = context.Songs.Where(s => s.Id == songId).Any();

                return new Result<bool>() { Data = exists };
            }
            catch (Exception e)
            {
                return new Result<bool>() { ErrorMessage = "Dal->SongRepository->DoesSongExist error: " + e.Message };
            }
        }

        public Result<SongsDto> GetSongsUsedByShow(int showId)
        {
            try
            {
                List<Song> songs = new List<Song>();
                songs = context.Songs.Where(s=>s.SongsPlayed.Select(sp=>sp.show.Id).Contains(showId)).ToList();

                SongsDto returnValue = new SongsDto();

                foreach (Song s in songs)
                {
                    returnValue.Songs.Add(new SongDto { Id = s.Id, Information = s.Information, name = s.name, Release_date = s.Release_date });
                }
                return new Result<SongsDto>() { Data = returnValue};
            }
            catch (Exception e)
            {
                return new Result<SongsDto>() { ErrorMessage = "Dal->SongRepository->GetSongsUsedByShow error:" + e.Message };
            }
        }
    }
}
