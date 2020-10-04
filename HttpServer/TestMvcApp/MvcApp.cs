using HttpServer.Http.Contracts;
using HttpServer.Http;
using System;
using System.Threading.Tasks;

namespace TestMvcApp
{
    class MvcApp
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new Server();
            await server.StartAsync(80);
        }
    }
}