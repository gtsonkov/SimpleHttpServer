using HttpServer.Http;
using HttpServer.MvcFramework;
using System;
using System.Text;
using TestMvcApp.Common;

namespace TestMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }

        public HttpResponse About(HttpRequest request)
        {
            string path = Generator.GenerateInternPath(request.Path);
            return this.Viev(path);
        }
    }
}