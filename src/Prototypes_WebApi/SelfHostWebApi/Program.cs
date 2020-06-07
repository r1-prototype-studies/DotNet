using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverConfigs = new System.Web.Http.SelfHost.HttpSelfHostConfiguration("http://localhost:8080");

            //To determine which action to invoke, the framework uses a routing table. The Visual Studio project template for Web API creates a default route
            serverConfigs.Routes.MapHttpRoute(
                "API Default",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (var server = new System.Web.Http.SelfHost.HttpSelfHostServer(serverConfigs))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Waiting for requests...");
                Console.WriteLine();
                Console.WriteLine("Hit [Enter] to shutdown the server");
                Console.ReadLine();
            }
        }
    }
}
