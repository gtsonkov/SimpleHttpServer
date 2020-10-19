using HttpServer.Http;
using HttpServer.Http.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpServer.MvcFramework
{
    public class Host
    {
        public static async Task RunAsync(List<Route> routTable, int port = 80)
        {
            IHttpServer server = new Server(routTable);
            await server.StartAsync(port);
        }
    }
}