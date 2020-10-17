using HttpServer.Http;
using HttpServer.Http.Constants;
using System.IO;
using System.Text;

namespace HttpServer.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse Viev (string viewPath)
        {
            string responseHtml = File.ReadAllText(viewPath).ToString();

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