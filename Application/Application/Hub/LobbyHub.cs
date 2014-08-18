using System;
using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;

namespace Application.Hub
{
    public class LobbyHub : Microsoft.AspNet.SignalR.Hub
    {
        private static readonly Stack<string> WaitingPlayerStack = new Stack<string>();
        private static readonly IGameMappable GameMapper = new GameMapper();
        private static readonly Object SyncLock = new object();

        public void Join()
        {            
            var id = Context.User.Identity.Name;

            AccessStackWithNew(id,GameMapper);

            Clients.User(id).someMethodOnClient();
        }


        private static void AccessStackWithNew(string id, IGameMappable gameMapper)
        {
            lock (SyncLock)
            {
                WaitingPlayerStack.Push(id);
                if (WaitingPlayerStack.Count%2 != 0) return;
                var player2 = WaitingPlayerStack.Pop();
                var player1 = WaitingPlayerStack.Pop();

                gameMapper.Add(player1, player2);
            }
        }
    }
}