using HttpServer.Http;
using HttpServer.MvcFramework;
using TestMvcApp.Common;

namespace TestMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            string pathIndex = ConstantData_TestMVC.PathIdexView;
            return this.Viev(pathIndex);
        }

        public HttpResponse About(HttpRequest request)
        {
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }
    }
}