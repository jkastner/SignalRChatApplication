using System.Windows;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Test_SignalR.Startup))]
namespace Test_SignalR
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}