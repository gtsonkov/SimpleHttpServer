using HttpServer.Http;
using HttpServer.MvcFramework;
using TestMvcApp.Common;

namespace TestMvcApp.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse Add(HttpRequest request)
        {
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }
    }
}