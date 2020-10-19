using System;
using System.Threading.Tasks;

namespace HttpServer.Http.Contracts
{
    public interface IHttpServer
    {
        Task StartAsync(int port);
    }
}