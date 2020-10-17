using HttpServer.Http;
using HttpServer.MvcFramework;

namespace TestMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.Viev(request.Path);
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.Viev(request.Path);
        }
    }
}