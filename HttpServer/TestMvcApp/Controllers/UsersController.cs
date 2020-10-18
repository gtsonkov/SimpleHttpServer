using HttpServer.Http;
using HttpServer.Http.Constants;
using HttpServer.MvcFramework;
using System;
using System.IO;
using System.Text;
using TestMvcApp.Constants;

namespace TestMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            string path = GenerateInternPath(request.Path);
            return this.Viev(path);
        }

        public HttpResponse Register(HttpRequest request)
        {
            string path = GenerateInternPath(request.Path);
            return this.Viev(path);
        }

        private string GenerateInternPath(string requestPath)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ConstantData_TestMVC.DomainPath);
            sb.Append(requestPath);
            sb.Append(ConstantData_TestMVC.HtmlFileExtension);

            return sb.ToString();
        }
    }
}