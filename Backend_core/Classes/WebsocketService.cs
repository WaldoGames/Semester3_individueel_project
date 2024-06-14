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
    [Authorize]
    public class WebsocketService: Hub
    {
        IPlaylistRepository playlistRepository;
        ISongRepository songRepository;
        IShowRepository showRepository;


        public WebsocketService(IPlaylistRepository playlistRepository, ISongRepository songRepository, IShowRepository showRepository)
        {
            this.playlistRepository = playlistRepository;
            this.songRepository = songRepository;
            this.showRepository = showRepository;
        }
        public async Task SendMessage(string group, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public void SendCurrentRoomStatus(string GroupId, int playlistId, int showId, int index)
        {
            PlayListService service = new PlayListService(playlistRepository, songRepository, showRepository);

            var c = Context.ConnectionId;

            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(playlistId, showId, index);

            if (!result.IsFailed)
            {
                Clients.Group(GroupId).SendAsync("UpdateCurrentRoomstate", result.Data);
                return;
            }
            Clients.Group(GroupId).SendAsync("Error", "somethign went wrong with updating room status");

        }

        public async Task JoinRoom(string GroupId)
        {
            //string c = Context.ConnectionId + ";
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupId);
            await Clients.Group(GroupId).SendAsync("Host-SendCurrentRoomstate");
        }
        public Task LeaveRoom(string GroupId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupId);
        }
        public Task RequestCurrentRoomStatusFromHost(string GroupId)
        {
            return Clients.Group(GroupId).SendAsync("Host-SendCurrentRoomstate");
        }
        public void MoveIndex(string GroupId, int amount)
        {
            if(amount > 0)
            {
                Clients.Group(GroupId).SendAsync("Host-Next", amount);
            }
            else if(amount<0)
            {
                Clients.Group(GroupId).SendAsync("Host-Previous", amount);

            }
            
        }

    }
}
