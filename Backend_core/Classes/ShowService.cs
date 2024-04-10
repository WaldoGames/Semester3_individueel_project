using Backend_core.DTO;
using Backend_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    internal class ShowService
    {
        IShowRepository showRepository { get; set; }
        IUserRepository userRepository { get; set; }

        public ShowService(IShowRepository showRepository, IUserRepository userRepository)
        {
            this.showRepository = showRepository;
            this.userRepository = userRepository;
        }

        public Result<ShowsDto> GetAllShowsWithConnectionToUser(string sub)
        {
            Result<bool> userResult = userRepository.DoesUserExist(sub);

            if (userResult.IsFailed)
            {
                if (userResult.IsFailedWarning) return new Result<ShowsDto> { WarningMessage = userResult.WarningMessage };
                return new Result<ShowsDto> { ErrorMessage = userResult.ErrorMessage };
            }

            return showRepository.GetAllShowsWithConnectionToUser(sub);

        }
    }
}
