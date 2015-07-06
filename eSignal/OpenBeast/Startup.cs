using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(OpenBeast.Startup))]

namespace OpenBeast
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            /// To check Userwise update.
            //var idProvider = new MyIdProvider();            
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);
            
            HubConfiguration config = new HubConfiguration();
            config.EnableJSONP = true;
            app.MapSignalR(config);
        }
    }
}
