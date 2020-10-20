using HttpServer.Http;
using HttpServer.MvcFramework;

namespace TestMvcApp.Controllers
{
    public class StaticFilesController : Controller
    {
        public HttpResponse FavIco(HttpRequest request)
        {
            return this.File("wwwroot/favicon.ico", "image/vnd.microsoft.icon");
        }

        public HttpResponse BootstrapCss(HttpRequest reuqest)
        {
            return this.File("wwwroot/css/bootstrap.min.css", "text/css");
        }

        public HttpResponse CustomCss(HttpRequest reuqest)
        {
            return this.File("wwwroot/css/custom.css", "text/css");
        }

        public HttpResponse CustomJs(HttpRequest reuqest)
        {
            return this.File("wwwroot/js/custom.js", "text/javascript");
        }

        public HttpResponse BootstrapJs(HttpRequest reuqest)
        {
            return this.File("wwwroot/js/bootstrap.bundle.min.js", "text/javascript");
        }
    }
}