using HttpServer.Http;
using HttpServer.MvcFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestMvcApp.Controllers;

namespace TestMvcApp
{
    class MvcApp
    {
        static async Task Main(string[] args)
        {
            List<Route> routTable = new List<Route>();

            routTable.Add(new Route("/",new HomeController().Index));
            routTable.Add(new Route("/Home/About",new HomeController().About));
            routTable.Add(new Route("/favicon.ico",new StaticFilesController().Favicon));
            routTable.Add(new Route("/Users/Login", new UsersController().Login));
            routTable.Add(new Route("/Users/Register", new UsersController().Register));
            routTable.Add(new Route("/Cards/All", new CardsController().All));
            routTable.Add(new Route("/Cards/Add", new CardsController().Add));
            routTable.Add(new Route("/Cards/Collection", new CardsController().Collection));

            await Host.RunAsync(routTable, 80);
        }
    }
}