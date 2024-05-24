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
        // send a message to all open sockets
        public async Task SendMessage(string group, string message)
        {
            try
            {
                await Clients.Group(group).SendAsync("ReceiveMessage", message);
            }
            catch (Exception)
            {
                // log exp
            }
        }
        public Task JoinRoom(string GroupId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, GroupId);
        }
        public Task LeaveRoom(string GroupId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupId);
        }

    }
}
