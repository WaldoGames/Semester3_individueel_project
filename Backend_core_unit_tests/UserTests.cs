using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests
{
    public class UserTests
    {
        private UserService service;
        private IUserRepository userRepository;

        public UserTests()
        {
            TestVar.test = true;
            userRepository = A.Fake<IUserRepository>();

            service = new UserService(userRepository);
        }

        //RegisterLogin
        //DoesUserExist
        //AddNewUser
        [Fact]
        public void RegisterLogin_Happyflow_NewUserCreated()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   Data = false,
               }
            );
            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).Returns(
               new SimpleResult
               {

               }
            );

            SimpleResult result = service.RegisterLogin(new NewUserDto { Email = "mail.com", SubId = "subid" });

            A.CallTo(()=>userRepository.AddNewUser(A<NewUserDto>._)).MustHaveHappened();
            Assert.False(result.IsFailed);

        }

        [Fact]
        public void RegisterLogin_Happyflow_UserAlreadyExists()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );
            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).Returns(
               new SimpleResult
               {

               }
            );

            SimpleResult result = service.RegisterLogin(new NewUserDto { Email = "mail.com", SubId = "subid" });

            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).MustNotHaveHappened();
            Assert.False(result.IsFailed);

        }

        [Fact]
        public void RegisterLogin_UserError_ResultWithErrorMessage()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   ErrorMessage="error"
               }
            );
            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).Returns(
               new SimpleResult
               {

               }
            );

            SimpleResult result = service.RegisterLogin(new NewUserDto { Email = "mail.com", SubId = "subid" });

            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).MustNotHaveHappened();
            Assert.True(result.IsFailedError);

        }

        [Fact]
        public void RegisterLogin_UserWarning_ResultWithWarningMessage()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   WarningMessage = "error"
               }
            );
            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).Returns(
               new SimpleResult
               {

               }
            );

            SimpleResult result = service.RegisterLogin(new NewUserDto { Email = "mail.com", SubId = "subid" });

            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).MustNotHaveHappened();
            Assert.True(result.IsFailedWarning);

        }
        [Fact]
        public void RegisterLogin_UserCreationError_ResultWithErrorMessage()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   Data=false
               }
            );
            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).Returns(
               new SimpleResult
               {
                   ErrorMessage = "error"
               }
            );

            SimpleResult result = service.RegisterLogin(new NewUserDto { Email = "mail.com", SubId = "subid" });

            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).MustHaveHappened();
            Assert.True(result.IsFailedError);

        }
        [Fact]
        public void RegisterLogin_UserCreationWarning_ResultWithWarningMessage()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   Data = false
               }
            );
            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).Returns(
               new SimpleResult
               {
                   WarningMessage = "warning"
               }
            );

            SimpleResult result = service.RegisterLogin(new NewUserDto { Email = "mail.com", SubId = "subid" });

            A.CallTo(() => userRepository.AddNewUser(A<NewUserDto>._)).MustHaveHappened();
            Assert.True(result.IsFailedWarning);

        }
    }
}
