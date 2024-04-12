using Backend_core.DTO;
using Backend_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    public class ShowService
    {
        IShowRepository showRepository { get; set; }
        IUserRepository userRepository { get; set; }

        public ShowService(IShowRepository showRepository, IUserRepository userRepository)
        {
            this.showRepository = showRepository;
            this.userRepository = userRepository;
        }

        public Result<ShowsDto> GetAllShowsWithConnectionToUser(string auth_sub)
        {
            Result<bool> userResult = userRepository.DoesUserExist(auth_sub);

            if (userResult.IsFailed)
            {
                if (userResult.IsFailedWarning) return new Result<ShowsDto> { WarningMessage = userResult.WarningMessage };
                return new Result<ShowsDto> { ErrorMessage = userResult.ErrorMessage };
            }
            if (userResult.Data == false)
            {
                return new Result<ShowsDto> { WarningMessage = "user doesn't exists" };
            }

            return showRepository.GetAllShowsWithConnectionToUser(auth_sub);

        }
        public SimpleResult CreateShow(NewShowDto newShow)
        {
           Result<bool> userResult = userRepository.DoesUserExist(newShow.auth_sub);

           if (userResult.IsFailed)
           {
               if (userResult.IsFailedWarning) return new SimpleResult { WarningMessage = userResult.WarningMessage };
               return new SimpleResult { ErrorMessage = userResult.ErrorMessage };
           }
            if (userResult.Data == false)
            {
                return new SimpleResult { WarningMessage= "ShowService->CreateShow user doesn't exist" };
            }

            try
            {
                return showRepository.CreateNewShow(newShow);
            }
            catch (Exception e)
            {
                return new SimpleResult { ErrorMessage= "ShowService->CreateShow error: "+ e.Message };
            }

           
        }
    }
}
