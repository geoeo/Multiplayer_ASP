using System;
using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;

namespace Application.Hub
{
    public class LobbyHub : Microsoft.AspNet.SignalR.Hub
    {
        private static readonly Queue<string> WaitingPlayerQueue = new Queue<string>();
        private static readonly IGameMappable GameMapper = new GameMapper();
        private static readonly Object SyncLock = new object();

        public void Join()
        {            
            var id = Context.User.Identity.Name;

            AccessQueueWithNew(id, GameMapper);

            Clients.User(id).someMethodOnClient();
        }


        private static void AccessQueueWithNew(string id, IGameMappable gameMapper)
        {
            lock (SyncLock)
            {
                WaitingPlayerQueue.Enqueue(id);
                if (WaitingPlayerQueue.Count%2 != 0) return;
                var player1 = WaitingPlayerQueue.Dequeue();
                var player2 = WaitingPlayerQueue.Dequeue();

                gameMapper.Add(player1, player2);
            }
        }
    }
}