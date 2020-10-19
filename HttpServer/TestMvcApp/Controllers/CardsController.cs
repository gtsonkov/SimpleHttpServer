using HttpServer.Http;
using HttpServer.MvcFramework;
using TestMvcApp.Common;

namespace TestMvcApp.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse All(HttpRequest request)
        {
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }

        public HttpResponse Add(HttpRequest request)
        {
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }

        public HttpResponse Collection(HttpRequest request)
        {
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }
    }
}