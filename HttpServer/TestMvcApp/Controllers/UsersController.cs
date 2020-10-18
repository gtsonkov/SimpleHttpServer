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
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }

        public HttpResponse Register(HttpRequest request)
        {
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }
    }
}