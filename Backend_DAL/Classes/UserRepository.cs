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
    public class UserRepository : IUserRepository
    {
        MusicAppContext context = new MusicAppContext();
        public SimpleResult AddNewUser(NewUserDto newUser)
        {
            try
            {
                User user = new User() { auth0sub = newUser.SubId, user_email = newUser.Email };

                context.Users.Add(user);

                context.SaveChanges();

                return new SimpleResult();
            }
            catch (Exception e)
            {
                return new SimpleResult { ErrorMessage = "error: UserRepository->AddNewUser" + e.Message };
            }
        }

        public Result<bool> DoesUserExist(string Subid)
        {
            try
            {
                if(context.Users.Where(u => u.auth0sub == Subid).Count() == 1)
                {
                    return new Result<bool> { Data = true };
                }
                else
                {
                    return new Result<bool> { Data = false };
                }

            }
            catch (Exception e)
            {
                return new Result<bool> { ErrorMessage = "error: UserRepository->DoesUserExist error:" + e.Message };
            }
        }
    }
}
