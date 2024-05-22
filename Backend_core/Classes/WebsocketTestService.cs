using Backend_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Backend_core.Classes
{
    public static class WebsocketTestService
    {
        static List<WebSocket> testSockets = new List<WebSocket>();

        public static void addTestSockets(WebSocket TS)
        {
            testSockets.Add((WebSocket)TS);
        }
        public static void removeTestSockets(WebSocket TS) 
        {
            testSockets.Remove((WebSocket)TS);
        }

        // send a message to all open sockets
        public static async Task SendAll(string message)
        {
            try
            {
                List<WebSocket> webSocketList;
                lock (testSockets) webSocketList = testSockets;

                byte[] byteMessage = Encoding.UTF8.GetBytes(message);

                webSocketList.ForEach(f =>
                {
                    if (f.State == WebSocketState.Open)
                    {
                        f.SendAsync(new ArraySegment<byte>(byteMessage), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                });
            }
            catch (Exception)
            {
                // log exp
            }
        }

    }
}
