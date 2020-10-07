using HttpServer.Http;
using HttpServer.Http.Constants;
using HttpServer.Http.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMvcApp
{
    class MvcApp
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new Server();
            server.AddRoute("/", HomePage);
            server.AddRoute("/about", About);
            await server.StartAsync(80);
        }

        static HttpResponse HomePage(HttpRequest request)
        {
            string responseHtml = MessagesResponse.HtmlHeaderWelcome + ConstantData.NewLine
                        + request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;

            byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);

            var response = new HttpResponse(MessagesResponse.ContentHTML, responseBodyBytes);
            response.Headers.Add(new Header(ConstantData.ServerNameHeader, MessagesResponse.FullServerInfo));

            return response;
        }

        static HttpResponse About(HttpRequest request)
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