using HttpServer.Http;
using HttpServer.MvcFramework;
using System.Text;
using TestMvcApp.Common;

namespace TestMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return this.View("Login");
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View("Register");
        }
    }
}