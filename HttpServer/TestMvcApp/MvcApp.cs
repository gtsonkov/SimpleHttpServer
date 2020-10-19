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
            server.AddRoute("/Home/About",new HomeController().About);
            server.AddRoute("/favicon.ico",new StaticFilesController().Favicon);
            server.AddRoute("/Users/Login", new UsersController().Login);
            server.AddRoute("/Users/Register", new UsersController().Register);
            server.AddRoute("/Cards/All", new CardsController().All);
            server.AddRoute("/Cards/Add", new CardsController().Add);
            server.AddRoute("/Cards/Collection", new CardsController().Collection);

            await server.StartAsync(80);
        }
    }
}