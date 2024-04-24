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
        readonly MusicAppContext context = new MusicAppContext();

        public SimpleResult AddSongToShow(NewSongDto newSongDto,int songId)
        {
            return AddSongToShow(new NewShowSongConnectionDto { showId = newSongDto.showId, User_description= newSongDto.User_description, songId=songId });
        }

        public SimpleResult AddSongToShow(NewShowSongConnectionDto newSongDto)
        {
            try
            {
                Show_song show_Song = new Show_song()
                {
                    Information = newSongDto.User_description,
                    Show = context.Shows.Where(s => s.Id == newSongDto.showId).First(),
                    Song = context.Songs.Where(s => s.Id == newSongDto.songId).First(),
                };

                context.Show_Song.Add(show_Song);
                context.SaveChanges();
                return new SimpleResult();

            }
            catch (Exception e)
            {
                return new SimpleResult() { ErrorMessage = "SongRepository->AddSongToShow "+ e.Message };
            }
        }

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
                List<Song> songs;
                songs = context.Songs.Where(s => s.Shows.Select(sp => sp.Show.Id).Contains(showId)).ToList();

                SongsDto returnValue = new SongsDto();

                foreach (Song song in songs)
                {
                    string information = context.Show_Song.Where(s => s.Show.Id == showId && s.Song.Id == song.Id).Select(so=>so.Information).First();
                    int amountPlayed = context.Show_Song_Playeds.Where(s => s.show.Id == showId && s.song.Id == song.Id).Count(); 
                    DateTime lastPlayed = context.Show_Song_Playeds.Where(s => s.show.Id == showId && s.song.Id == song.Id).Select(s=>s.timePlayed).OrderByDescending(s=>s).FirstOrDefault();

                    returnValue.Songs.Add(new SongWithLastPlayedDto { Id = song.Id, User_description = information, name = song.name, Release_date = song.Release_date, AmountPlayed = amountPlayed, LastPlayed = lastPlayed });
                }
                return new Result<SongsDto>() { Data = returnValue};
            }
            catch (Exception e)
            {
                return new Result<SongsDto>() { ErrorMessage = "Dal->SongRepository->GetSongsUsedByShow error:" + e.Message };
            }
        }

        public Result<int> PostNewSong(NewSongDto newSongDto)
        {
            Result<int> result = new Result<int>();

            try
            {
                Song newSong = new Song()
                {
                    name = newSongDto.name,
                    Release_date = newSongDto.Release_date,
                    Creators = context.Artists.Where(a => newSongDto.CreatorIds.Contains(a.Id)).ToList()
                };

                if (newSong.Creators.Count != newSongDto.CreatorIds.Count)
                {
                    result.WarningMessage = "not all given creators where able to be added to a song";
                }

                context.Songs.Add(newSong);
                context.SaveChanges();
                result.Data = newSong.Id;
                return result;
            }
            catch (Exception e)
            {
                result.ErrorMessage= "Dal->SongRepository->PostNewSong error:" + e.Message;
                return result;
            }
            
        }
        public SimpleResult PostPlayedSong(PlaySongDto songPlayed)
        {
            try
            {
                context.Show_Song_Playeds.Add(new Show_song_played() {

                    show = context.Shows.Where(s => s.Id == songPlayed.showId).First(),
                    song = context.Songs.Where(s => s.Id == songPlayed.songId).First(),
                    timePlayed = songPlayed.timePlayed,
                });

                context.SaveChanges();


                return new SimpleResult();

            }
            catch (Exception e)
            {
                return new SimpleResult() { ErrorMessage = "Dal->SongRepository->PostPlayedSong error:" + e.Message };
            }
        }
    }
}
