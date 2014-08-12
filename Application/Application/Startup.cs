using System;
using System.Threading.Tasks;
using Application.Utils;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Application.Startup))]

namespace Application
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            // Register CustomUserIdProvider - not custom but reimplmentation of PrincipalUserIdProvider

            IUserIdProvider myProvider = new CustomUserIdProvider();

            // Hook up to SingalR pipeline
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => myProvider);

            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();




        }
    }
}
