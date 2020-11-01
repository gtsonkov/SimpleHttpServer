using HttpServer.MvcFramework.Identity.Enums;

namespace SUS.MvcFramework
{
    public class IdentityUser<T>
    {
        public T Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IdentityRole Role { get; set; }
    }
}