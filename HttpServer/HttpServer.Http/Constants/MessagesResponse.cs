namespace HttpServer.Http.Constants
{
    public static class MessagesResponse
    {
        public static string HtmlHeaderWelcome = "<h1>Welcome!</h1>";

        public static string HtmlHeaderAbout = "<h1>About..............</h1>";

        public static string HtmlHeaderNotFound = "<h1>404 - Page Not Found!</h1>";

        public static string HttpVersion = "HTTP/1.1";

        public static string HttpResponseOK = HttpVersion + " " + "200 OK";

        public static string ServerName = "GT-HttpServer";

        public static string ServerVersion = "1.0";

        public static string ContentHTML = "text/html";

        public static string FullServerInfo = ServerName + " " + ServerVersion;
    }
}