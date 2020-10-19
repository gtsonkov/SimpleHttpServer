using HttpServer.Http;
using HttpServer.MvcFramework;

namespace TestMvcApp.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse All(HttpRequest request)
        {
            return this.View("All");
        }

        public HttpResponse Add(HttpRequest request)
        {
            return this.View("Add");
        }

        public HttpResponse Collection(HttpRequest request)
        {
            return this.View("Collectons");
        }
    }
}