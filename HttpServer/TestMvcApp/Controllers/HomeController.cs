using HttpServer.Http;
using HttpServer.Http.Constants;
using HttpServer.MvcFramework;
using System.IO;
using System.Linq;
using System.Text;

namespace TestMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            string responseHtml = File.ReadAllText("./View/Home/index.html").ToString();

            byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);

            var response = new HttpResponse(MessagesResponse.ContentHTML, responseBodyBytes);
            response.Headers.Add(new Header(ConstantData.ServerNameHeader, MessagesResponse.FullServerInfo));

            return response;
        }

        public HttpResponse About(HttpRequest request)
        {
            string responseHtml = MessagesResponse.HtmlHeaderAbout + ConstantData.NewLine
                        + request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;

            byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);

            var response = new HttpResponse(MessagesResponse.ContentHTML, responseBodyBytes);
            response.Headers.Add(new Header(ConstantData.ServerNameHeader, MessagesResponse.FullServerInfo));

            return response;
        }

        private static byte[] EncodingUtfToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
    }
}