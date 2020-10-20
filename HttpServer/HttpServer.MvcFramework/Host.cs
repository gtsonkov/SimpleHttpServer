using HttpServer.Http;
using HttpServer.Http.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpServer.MvcFramework
{
    public class Host
    {
        public static async Task RunAsync(IMvcApplication application, int port = 80)
        {
            List<Route> routeTable = new List<Route>();
            application.ConfigureServices();
            application.Configure(routeTable);

            IHttpServer server = new Server(routeTable);
            await server.StartAsync(port);
        }
    }
}