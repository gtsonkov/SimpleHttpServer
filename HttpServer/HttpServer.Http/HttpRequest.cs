using HttpServer.Http.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpServer.Http
{
    public class HttpRequest
    {
        private HttpRequest()
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
        }

        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            RequestStringParsing(requestString);
        }

        public string Path { get; set; }

        public string Method { get; set; }

        public string RequestBoddy { get; set; }

        public List<Header> Headers { get; set; }

        public List<Cookie> Cookies { get; set; }

        private void RequestStringParsing(string requestString)
        {
            var lines = requestString.Split(new string[]
            { ConstantData.NewLine }
            , System.StringSplitOptions.None)
                .ToArray();

            var headerLine = lines[0];

            var headerLineParts = headerLine
                .Split(' ')
                .ToArray();

            this.Method = headerLineParts[0];

            this.Path = headerLineParts[1];

            bool inHeaders = true;

            StringBuilder bodyBuilder = new StringBuilder();

            for (int i = 1; i < lines.Length; i++)
            {
                var currLine = lines[i];

                if (string.IsNullOrEmpty(currLine))
                {
                    inHeaders = false;
                    continue;
                }

                if (inHeaders)
                {
                    this.Headers.Add(new Header(currLine));
                }

                else
                {
                    bodyBuilder.AppendLine(currLine);
                }
            }

            var cookies = this.Headers.FirstOrDefault(x => x.Name == ConstantData.CookieHeader);

            if (cookies != null)
            {
                ParseCookies(cookies.Value);
            }

            this.RequestBoddy = bodyBuilder.ToString();
        }

        private void ParseCookies(string cookies)
        {
            var cookieData = cookies.
                Split(new string[] { "; " }
                , System.StringSplitOptions
                .RemoveEmptyEntries)
                .ToArray();

            foreach (var c in cookieData)
            {
                Cookie currCookie = new Cookie(c);

                this.Cookies.Add(currCookie);
            }
        }
    }
}