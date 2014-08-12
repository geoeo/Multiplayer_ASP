using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Application
{
    public class LobbyHub : Hub
    {
        public void Hello()
        {
            var id = Context.User.Identity.Name;
            Clients.User(id).someMethodOnClient();
        }
    }
}