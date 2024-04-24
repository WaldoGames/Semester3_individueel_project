using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests
{
    public class ShowTests
    {
        private ShowService service;
        private IShowRepository showRepository;
        private IUserRepository userRepository;
        public ShowTests()
        {
            TestVar.test = true;
            userRepository = A.Fake<IUserRepository>();
            showRepository = A.Fake<IShowRepository>();
            service = new ShowService(showRepository, userRepository);
        }

        [Fact] 
        public void GetAllShowsWithConnectionToUser_happyflow_Returns2Shows()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );

            A.CallTo(() => showRepository.GetAllShowsWithConnectionToUser(A<string>._)).Returns(
               new Result<ShowsDto>
               {
                   Data = new ShowsDto
                   {
                       shows = new List<ShowDto>
                       {
                           new ShowDto
                           {
                               show_name="s1",
                               show_description="s1",
                               show_language="en",
                               Id=1,
                           },
                           new ShowDto
                           {
                               show_name="s2",
                               show_description="s2",
                               show_language="nl",
                               Id=2,
                           },
                       }
                   }
               }
            );

            Result<ShowsDto> result= service.GetAllShowsWithConnectionToUser("id");

            Assert.False(result.IsFailed);
            Assert.True(result.Data.shows.Count == 2);
        }

        [Fact]
        public void GetAllShowsWithConnectionToUser_NoUser_ResultFailed()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   Data = false,
               }
            );

            A.CallTo(() => showRepository.GetAllShowsWithConnectionToUser(A<string>._)).Returns(
               new Result<ShowsDto>
               {
                   Data = new ShowsDto
                   {
                       shows = new List<ShowDto>
                       {
                           new ShowDto
                           {
                               show_name="s1",
                               show_description="s1",
                               show_language="en",
                               Id=1,
                           },
                           new ShowDto
                           {
                               show_name="s2",
                               show_description="s2",
                               show_language="nl",
                               Id=2,
                           },
                       }
                   }
               }
            );

            Result<ShowsDto> result = service.GetAllShowsWithConnectionToUser("id");

            Assert.True(result.IsFailed);
            Assert.Null(result.Data);
        }


        [Fact]
        public void CreateShow_happyflow_SimpleResultNoErrors()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   Data = true,
               }
            );

            A.CallTo(() => showRepository.CreateNewShow(A<NewShowDto>._)).Returns(
               new SimpleResult
               {
               }
            );

            SimpleResult result = service.CreateShow(new NewShowDto { });

            Assert.False(result.IsFailed);

        }
        [Fact]
        public void CreateShow_UserDoesNotExist_SimpleResultWarning()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   Data = false,
               }
            );

            A.CallTo(() => showRepository.CreateNewShow(A<NewShowDto>._)).Returns(
               new SimpleResult
               {
               }
            );

            SimpleResult result = service.CreateShow(new NewShowDto { });

            Assert.True(result.IsFailedWarning);

        }
        [Fact]
        public void CreateShow_UserDoesNotExistError_SimpleResultError()
        {
            A.CallTo(() => userRepository.DoesUserExist(A<string>._)).Returns(
               new Result<bool>
               {
                   ErrorMessage = "a"
               }
            );

            A.CallTo(() => showRepository.CreateNewShow(A<NewShowDto>._)).Returns(
               new SimpleResult
               {
               }
            );

            SimpleResult result = service.CreateShow(new NewShowDto { });

            Assert.True(result.IsFailedError);

        }

    }

}
