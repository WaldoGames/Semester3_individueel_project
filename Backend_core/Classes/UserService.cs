using Backend_core.DTO;
using Backend_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    public class UserService
    {

        IUserRepository UserRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public SimpleResult RegisterLogin(NewUserDto LoginUser)
        {

            Result<bool> checkResult = UserRepository.DoesUserExist(LoginUser.SubId);

            if (checkResult.IsFailed){
                if(checkResult.IsFailedWarning) return new SimpleResult { WarningMessage = checkResult.WarningMessage };
                return new SimpleResult { ErrorMessage = checkResult.ErrorMessage };
            }

            if (checkResult.Data == true)
            {
                return new SimpleResult();
            }
            
            SimpleResult newUserResult = UserRepository.AddNewUser(LoginUser);

            if (newUserResult.IsFailed)
            {
                if (newUserResult.IsFailedWarning) return new SimpleResult { WarningMessage = newUserResult.WarningMessage };
                return new SimpleResult { ErrorMessage = newUserResult.ErrorMessage };
            }

            return new SimpleResult();
        }
    }
}
