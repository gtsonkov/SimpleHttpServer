using HttpServer.Http;
using HttpServer.Http.Enums;
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

            routTable.Add(new Route("/", HttpMethod.Get ,new HomeController().Index));
            routTable.Add(new Route("/Home/About", HttpMethod.Get, new HomeController().About));
            routTable.Add(new Route("/Users/Login", HttpMethod.Get, new UsersController().Login));
            routTable.Add(new Route("/Users/Register", HttpMethod.Get, new UsersController().Register));
            routTable.Add(new Route("/Cards/All", HttpMethod.Get, new CardsController().All));
            routTable.Add(new Route("/Cards/Add", HttpMethod.Get, new CardsController().Add));
            routTable.Add(new Route("/Cards/Collection", HttpMethod.Get, new CardsController().Collection));

            routTable.Add(new Route("/favicon.ico", HttpMethod.Get, new StaticFilesController().FavIco));
            routTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.Get, new StaticFilesController().BootstrapCss));
            routTable.Add(new Route("/css/custom.css", HttpMethod.Get, new StaticFilesController().CustomCss));
            routTable.Add(new Route("/js/custom.js", HttpMethod.Get, new StaticFilesController().CustomJs));
            routTable.Add(new Route("/js/bootstrap.bundle.mini.js", HttpMethod.Get, new StaticFilesController().BootstrapJs));

            await Host.RunAsync(new StratUp(), 80);
        }
    }
}