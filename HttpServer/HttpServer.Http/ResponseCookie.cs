using HttpServer.Http.Constants;
using System.Text;

namespace HttpServer.Http
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value)
            : base(name, value)
        {
            this.Path = ConstantData.DefaultPathChar; 
        }

        public int MaxAge { get; set; }

        public bool HttpOnly { get; set; }

        public string Domain { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            StringBuilder cb = new StringBuilder();

            cb.Append($"{this.Name}={this.Value}");

            if (MaxAge != 0)
            {
                cb.Append($" {CookieData.MaxAgeHeader}={this.MaxAge};");
            }

            cb.Append($" {CookieData.PathHeader}={this.Path};");

            if (this.HttpOnly)
            {
                cb.Append($"{ CookieData.HttpOnlyHeader};");
            }

            return cb.ToString();
        }
    }
}