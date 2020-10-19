using HttpServer.Http;
using HttpServer.Http.Constants;
using System.IO;
using System.Text;

namespace HttpServer.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View (string viewPath)
        {
            string responseHtml = File.ReadAllText(
                ConstantData.DefaultViewFolder 
                + ConstantData.DefaultPathChar 
                + this.GetType().Name.Replace(ConstantData.ControllerHeader, string.Empty)
                + ConstantData.DefaultPathChar
                + viewPath
                + ConstantData.HtmlFileExtension)
                .ToString();

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