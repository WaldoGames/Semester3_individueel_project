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

        public void SendCurrentRoomStatus(string groupId, int playlistId, int showId, int index)
        {
            PlayListService service = new PlayListService(playlistRepository, songRepository, showRepository);

            var c = Context.ConnectionId;

            Result<PlaylistStatusDto> result = service.WebGetPlaylistStatus(playlistId, showId, index);

            if (!result.IsFailed)
            {
                Clients.Group(groupId).SendAsync("UpdateCurrentRoomstate", result.Data);
                return;
            }
            Clients.Group(groupId).SendAsync("Error", "something went wrong with updating room status");

        }

        public async Task JoinRoom(string groupId)
        {
            //string c = Context.ConnectionId + ";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            await Clients.Group(groupId).SendAsync("Host-SendCurrentRoomstate");
        }
        public Task LeaveRoom(string groupId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }
        public Task RequestCurrentRoomStatusFromHost(string GroupId)
        {
            return Clients.Group(GroupId).SendAsync("Host-SendCurrentRoomstate");
        }
        public void MoveIndex(string groupId, int amount)
        {
            if(amount > 0)
            {
                Clients.Group(groupId).SendAsync("Host-Next", amount);
            }
            else if(amount<0)
            {
                Clients.Group(groupId).SendAsync("Host-Previous", amount);

            }
            
        }

    }
}
