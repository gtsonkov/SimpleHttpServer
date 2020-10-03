using System;
using System.Threading.Tasks;

namespace HttpServer.Http.Contracts
{
    public interface IHttpServer
    {
        void AddRoute(string path, Func<HttpRequest, HttpResponse> action);

       Task StartAsync(int port);
    }
}