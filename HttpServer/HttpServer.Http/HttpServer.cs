using HttpServer.Http.Contracts;
using System;
using System.Collections.Generic;
using HttpServer.Http.Constants;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HttpServer.Http
{
    public class HttpServer : IHttpServer
    {
        private IDictionary<string, Func<HttpRequest, HttpResponse>> routTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            if (this.routTable.ContainsKey(path))
            {
                this.routTable[path] = action;

                return;
            }

            this.routTable.Add(path, action);
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);

            tcpListener.Start();

            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();

                ProcessClientAsync(client);
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            using (NetworkStream clientStream = client.GetStream())
            {
                int position = 0;

                List<byte> clientData = new List<byte>();
                byte[] buffer = new byte[ConstantData.BufferLenght];

                while (true)
                {
                    int currentCountOfReadedBits = await clientStream.ReadAsync(buffer, position, buffer.Length);

                    if (currentCountOfReadedBits < buffer.Length)
                    {
                        byte[] restData = new byte[currentCountOfReadedBits];
                        Array.Copy(buffer, restData, currentCountOfReadedBits);

                        clientData.AddRange(restData);

                        break; //No more data to read
                    }
                    else
                    {
                        clientData.AddRange(buffer);
                    }

                    position += currentCountOfReadedBits;
                }
            }
        }
    }
}