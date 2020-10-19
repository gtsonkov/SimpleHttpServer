﻿namespace HttpServer.Http.Constants
{
    public static class ConstantData
    {
        public static int BufferLenght = 4096; //Bits

        public static string NewLine = "\r\n";

        public static string CookieHeader = "Cookie";

        public static string ContenTypeHeader = "Content-Type";

        public static string ContentLegthHeader = "Content-Length";

        public static string ServerNameHeader = "Server";

        public static string DefaultPathChar = @"/";

        public static string SetCookieHeader = "Set-Cookie";

        public static string DefaultViewFolder = "View";

        public static string ControllerHeader = "Controller";

        public const string HtmlFileExtension = ".html";
    }
}