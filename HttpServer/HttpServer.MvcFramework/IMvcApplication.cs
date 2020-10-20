using HttpServer.Http;
using System.Collections.Generic;

namespace HttpServer.MvcFramework
{
    public interface IMvcApplication
    {
        void ConfigureServices();

        void Configure(List<Route> routeTable);
    }
}