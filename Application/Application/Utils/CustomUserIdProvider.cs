using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using NLog;

namespace Application.Utils
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();

        public string GetUserId(IRequest request)
        {

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.User != null && request.User.Identity != null)
            {
                string name = request.User.Identity.Name;
                Logger.Debug(name);
                return name;
            }

            return null;
        }
    }
}