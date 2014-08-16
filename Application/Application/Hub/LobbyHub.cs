using System;
using System.Collections.Generic;
using Application.Utils;

namespace Application.Hub
{
    public class LobbyHub : Microsoft.AspNet.SignalR.Hub
    {
        private Stack<string> WaitingPlayersList { get; set; } 
        private readonly Object _syncLock = new object();

        public void Join()
        {            
            var id = Context.User.Identity.Name;

            AccessListWithNewId(id);

            Clients.User(id).someMethodOnClient();
        }

        // TODO: Write Unit Test
        private void AccessListWithNewId(string id)
        {
            lock (_syncLock)
            {
                WaitingPlayersList.Push(id);
                if (WaitingPlayersList.Count%2 == 0)
                {
                    var player1 = WaitingPlayersList.Pop();
                    var player2 = WaitingPlayersList.Pop();

                    GameMapper.Add(player1, player2);
                }
            }
        }
    }
}