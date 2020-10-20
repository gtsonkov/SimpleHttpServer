using HttpServer.Http;
using HttpServer.MvcFramework;

namespace TestMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse About(HttpRequest request)
        {
            return this.View();
        }
    }
}