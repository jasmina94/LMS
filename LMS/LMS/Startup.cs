using Autofac.Integration.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LMS.Startup))]

namespace LMS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            //var resolver = new AutofacDependencyResolver(container);

            //app.MapSignalR(new HubConfiguration
            //{
            //    Resolver = resolver
            //});

            app.MapSignalR();
        }
    }
}
