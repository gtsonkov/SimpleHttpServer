using HttpServer.MvcFramework.ViewEngine.Common.ConstantData;
using HttpServer.MvcFramework.ViewEngine.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpServer.MvcFramework.ViewEngine.ViewModels
{
    public class ErrorView : IView
    {
        private readonly IEnumerable<string> _errorList;
        private readonly string _csharpCode;

        public ErrorView(IEnumerable<string> errorList, string csharpCode)
        {
            this._errorList = errorList;
            this._csharpCode = csharpCode;
        }

        public string GetHtml(object ViewModel)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(ErrorViewConstants.HtmlHeader);
            result.AppendLine(ErrorViewConstants.HtmlErrorCountHeader + this._errorList.Count());

            result.AppendLine("<ul>");
            foreach (var error in this._errorList)
            {
                result.AppendLine($"<li>{error}</li>");
            }
            result.AppendLine("</ul>");

            result.AppendLine($"<pre>{this._csharpCode}</pre>");
            
            return result.ToString();
        }
    }
}