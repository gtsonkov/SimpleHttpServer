using HttpServer.Http;
using HttpServer.Http.Contracts;
using System.Threading.Tasks;
using TestMvcApp.Controllers;

namespace TestMvcApp
{
    class MvcApp
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new Server();
            server.AddRoute("/",new HomeController().Index);
            server.AddRoute("/about",new HomeController().About);
            server.AddRoute("/favicon.ico",new StaticFilesController().Favicon);
            await server.StartAsync(80);
        }
    }
}