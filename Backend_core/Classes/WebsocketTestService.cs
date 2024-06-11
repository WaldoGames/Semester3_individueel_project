using Backend_core.DTO;
using Backend_core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    public class WebsocketTestService: Hub
    {
        IPlaylistRepository playlistRepository;
        ISongRepository songRepository;
        IShowRepository showRepository;


        public WebsocketTestService(IPlaylistRepository playlistRepository, ISongRepository songRepository, IShowRepository showRepository)
        {
            this.playlistRepository = playlistRepository;
            this.songRepository = songRepository;
            this.showRepository = showRepository;
        }
        public async Task SendMessage(string group, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public async Task SendCurrentRoomStatus(string GroupId, int playlistId, int showId, int index)
        {
            PlayListService service = new PlayListService(playlistRepository, songRepository, showRepository);

            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(playlistId, showId, index);

            if (!result.IsFailed)
            {
                await Clients.Group(GroupId).SendAsync("UpdateCurrentRoomstate", result.Data);
            }
            await Clients.Group(GroupId).SendAsync("Error", "somethign went wrong with updating room status");

        }

        public Task JoinRoom(string GroupId)
        {
            

            return Groups.AddToGroupAsync(Context.ConnectionId, GroupId);
        }
        public Task LeaveRoom(string GroupId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupId);
        }
        public Task RequestCurrentRoomStatusFromHost(string GroupId)
        {
            return Clients.Group(GroupId).SendAsync("Host-SendCurrentRoomstate");
        }

    }
}
