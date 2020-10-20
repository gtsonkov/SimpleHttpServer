using HttpServer.Http;
using HttpServer.Http.Constants;
using System.Runtime.CompilerServices;
using System.Text;

namespace HttpServer.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View ([CallerMemberName] string viewPath = null)
        {
            string layout = System.IO.File.ReadAllText(ConstantData.LayoutPath).ToString();

            string currViewHtml = System.IO.File.ReadAllText(
                ConstantData.DefaultViewFolder 
                + ConstantData.DefaultPathChar 
                + this.GetType().Name.Replace(ConstantData.ControllerHeader, string.Empty)
                + ConstantData.DefaultPathChar
                + viewPath
                + ConstantData.HtmlFileExtension)
                .ToString();

            string responseHtml = layout.Replace(ConstantData.RenderBodyHeader, currViewHtml);

           byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);

            var response = new HttpResponse(MessagesResponse.ContentHTML, responseBodyBytes);
            response.Headers.Add(new Header(ConstantData.ServerNameHeader, MessagesResponse.FullServerInfo));

            return response;
        }

        protected HttpResponse File(string filePath, string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, fileBytes);
            return response;
        }

        private byte[] EncodingUtfToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
    }
}