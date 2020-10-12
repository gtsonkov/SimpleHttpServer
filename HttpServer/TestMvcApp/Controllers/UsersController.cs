using HttpServer.Http;
using HttpServer.Http.Constants;
using HttpServer.MvcFramework;
using System.IO;
using System.Text;

namespace TestMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            string responseHtml = File.ReadAllText("./View/Users/Login.html").ToString();

            byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);

            var response = new HttpResponse(MessagesResponse.ContentHTML, responseBodyBytes);
            response.Headers.Add(new Header(ConstantData.ServerNameHeader, MessagesResponse.FullServerInfo));

            return response;
        }

        public HttpResponse Register(HttpRequest request)
        {
            string responseHtml = File.ReadAllText("./View/Users/Register.html").ToString();

            byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);

            var response = new HttpResponse(MessagesResponse.ContentHTML, responseBodyBytes);
            response.Headers.Add(new Header(ConstantData.ServerNameHeader, MessagesResponse.FullServerInfo));

            return response;
        }

        private byte[] EncodingUtfToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
    }
}