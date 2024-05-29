using Backend_core.Classes;
using Backend_core.DTO;
using Backend_core.Interfaces;
using Backend_core_unit_tests.Factory;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core_unit_tests
{
    public class PlaylistTests
    {
        private PlayListService service;
        private IPlaylistRepository playlistRepository;

        public PlaylistTests()
        {
            TestVar.test = true;
            playlistRepository = A.Fake<IPlaylistRepository>();
            service = new PlayListService(playlistRepository);
        }

        [Fact]
        public void CreatePlaylist_happyflow_PlaylistCreated()
        {
            NewPlaylistDto dto = new NewPlaylistDto
            {
                playListDescription = "testPlaylist",
                recordingPlayListName = "playlist",
                ShowId = 1,
                playlistItems = new List<NewPlaylistItemDto>
                {
                    new NewPlaylistItemDto
                    {
                        orderIndex = 1,
                        description = "first item",
                    },
                    new NewPlaylistItemDto
                    {
                        orderIndex = 2,
                        description = "second item",
                    },
                    new NewPlaylistItemDto
                    {
                        orderIndex = 3,
                        description = "third item",
                        playlistItemSongId = 1,
                    },
                }
            };

            A.CallTo(() => playlistRepository.createPlaylist(A<NewPlaylistDto>._)).Returns(
                new Result<int>
                {
                    Data = 1,
                }
            );
            A.CallTo(() => playlistRepository.createPlaylistItem(A<NewPlaylistItemDto>._)).Returns(
                new SimpleResult
                {
                }
            );

            SimpleResult result = service.CreatePlaylist(dto);

            Assert.False(result.IsFailed);
           
        }
        [Fact]
        public void CreatePlaylist_FailedToCreatePlaylist_IsFailed()
        {
            NewPlaylistDto dto = new NewPlaylistDto
            {
                playListDescription = "testPlaylist",
                recordingPlayListName = "playlist",
                ShowId = 1,
                playlistItems = new List<NewPlaylistItemDto>
                {
                    new NewPlaylistItemDto
                    {
                        orderIndex = 1,
                        description = "first item",
                    },
                    new NewPlaylistItemDto
                    {
                        orderIndex = 2,
                        description = "second item",
                    },
                    new NewPlaylistItemDto
                    {
                        orderIndex = 3,
                        description = "third item",
                        playlistItemSongId = 1,
                    },
                }
            };

            A.CallTo(() => playlistRepository.createPlaylist(A<NewPlaylistDto>._)).Returns(
                new Result<int>
                {
                    ErrorMessage = "Failed"
                }
            );
            A.CallTo(() => playlistRepository.createPlaylistItem(A<NewPlaylistItemDto>._)).Returns(
                new SimpleResult
                {
                }
            );

            SimpleResult result = service.CreatePlaylist(dto);

            Assert.EndsWith("Failed", result.ErrorMessage);
            Assert.True(result.IsFailed);

        }

        [Fact]
        public void CreatePlaylist_FailedToCreatePlaylistItem_IsFailed()
        {
            NewPlaylistDto dto = new NewPlaylistDto
            {
                playListDescription = "testPlaylist",
                recordingPlayListName = "playlist",
                ShowId = 1,
                playlistItems = new List<NewPlaylistItemDto>
                {
                    new NewPlaylistItemDto
                    {
                        orderIndex = 1,
                        description = "first item",
                    },
                    new NewPlaylistItemDto
                    {
                        orderIndex = 2,
                        description = "second item",
                    },
                    new NewPlaylistItemDto
                    {
                        orderIndex = 3,
                        description = "third item",
                        playlistItemSongId = 1,
                    },
                }
            };

            A.CallTo(() => playlistRepository.createPlaylist(A<NewPlaylistDto>._)).Returns(
                new Result<int>
                {
                    Data = 1,
                }
            );
            A.CallTo(() => playlistRepository.createPlaylistItem(A<NewPlaylistItemDto>._)).Returns(
                new SimpleResult
                {
                    ErrorMessage="Failed"
                }
            );

            SimpleResult result = service.CreatePlaylist(dto);

            Assert.Contains("playlist items", result.ErrorMessage);
            Assert.True(result.IsFailed);

        }
    }
}
