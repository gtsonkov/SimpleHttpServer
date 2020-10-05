namespace HttpServer.Http
{
    public class Cookie
    {
        public Cookie(string value)
        {
            var currCookieParts = value.Split(new char[] { '=' }, 2);

            this.Name = currCookieParts[0];
            this.Value = currCookieParts[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}