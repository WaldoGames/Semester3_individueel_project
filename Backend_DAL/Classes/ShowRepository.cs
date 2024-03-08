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
    public class ShowRepository : IShowRepository
    {
        MusicAppContext context = new MusicAppContext();

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

        public NullableResult<ShowDto> GetShowById(int id)
        {
            try
            {
                Show? show = context.Shows.Where(s => s.Id == id).FirstOrDefault();

                if (show == null)
                {
                    return new NullableResult<ShowDto>() { IsEmpty = true };
                }
                else
                {
                    return new NullableResult<ShowDto> { Data = new ShowDto() { Id = show.Id, show_description = show.show_description, show_language= show.show_language, show_name = show.show_name } };
                }


            }
            catch (Exception)
            {
                return new NullableResult<ShowDto> { ErrorMessage = "Error from: Dal->ArtistRepository->GetShowById->[ Id: " + id + "]" };
            }
        }
    }
}
