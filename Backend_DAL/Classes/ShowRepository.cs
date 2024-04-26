using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Backend_DAL.Classes
{
    public class ShowRepository : IShowRepository
    {
        readonly MusicAppContext context = new MusicAppContext();

        public SimpleResult CreateNewShow(NewShowDto newShow)
        {
            try
            {
                User creator = context.Users.Where(u => u.auth0sub == newShow.auth_sub).First();

                Show show = new Show
                {
                    show_description = newShow.show_discription,
                    show_name = newShow.show_name,
                    show_language = newShow.show_language,

                    hosts = [creator],
                };

                context.Shows.Add(show);
                context.SaveChanges();

                return new SimpleResult();
            }
            catch (Exception e)
            {
                return new SimpleResult { ErrorMessage = "ShowRepository->CreateNewShow error: " + e.Message };
            }
        }

        public Result<bool> DoesShowExist(int showId)
        {
            try
            {
                bool exists = context.Shows.Where(s => s.Id == showId).Any();

                return new Result<bool>() { Data = exists };
            }
            catch (Exception e)
            {
                return new Result<bool>() { ErrorMessage = "Dal->ShowRepository->DoesShowExist error: " + e.Message };
            }
        }

        public Result<ShowsDto> GetAllShowsWithConnectionToUser(string authSub)
        {
            try
            {
                List<Show> shows = context.Shows.Where(s => s.hosts.Select(h => h.auth0sub).Contains(authSub)).ToList();

                ShowsDto showsDto = new ShowsDto();

                foreach (Show item in shows)
                {
                    showsDto.shows.Add(new ShowDto { Id = item.Id, show_description = item.show_description, show_language = item.show_language, show_name = item.show_name });   
                }
                return new Result<ShowsDto> { Data = showsDto };
            }
            catch (Exception e)
            {

                return new Result<ShowsDto> { ErrorMessage = "ShowRepository->GetAllShowsWithConnectionToUser "+ e.Message };
            }
           
        }

        public NullableResult<ShowDto> GetShowById(int showId)
        {
            try
            {
                Show? show = context.Shows.Where(s => s.Id == showId).FirstOrDefault();

                if (show == null)
                {
                    return new NullableResult<ShowDto>() { };
                }
                else
                {
                    return new NullableResult<ShowDto> { Data = new ShowDto() { Id = show.Id, show_description = show.show_description, show_language= show.show_language, show_name = show.show_name } };
                }


            }
            catch (Exception)
            {
                return new NullableResult<ShowDto> { ErrorMessage = "Error from: Dal->ArtistRepository->GetShowById->[ Id: " + showId + "]" };
            }
        }

        public NullableResult<string> GetShowDiscriptionOfSong(int songId, int showId)
        {
            string Discription = context.Show_Song.Where(s => s.SongId == songId && s.ShowId == showId).First().Information;
            if (Discription == null)
            {
                return new NullableResult<string>();
            };

            return new NullableResult<string> { Data = Discription };
        }
    }
}
