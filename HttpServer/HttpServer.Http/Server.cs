using HttpServer.Http.Contracts;
using System;
using System.Collections.Generic;
using HttpServer.Http.Constants;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using IO;

namespace HttpServer.Http
{
    public class Server : IHttpServer
    {
        private Logger _logger;

        private IDictionary<string, Func<HttpRequest, HttpResponse>> routTable;

        public Server()
        {
            this._logger = new Logger();
            this.routTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();
        }

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

                string clientRequest = await EncodingClientData(clientData);

                await this._logger.WriteLineAsync(clientRequest);

                //Header
                string responseHtml = MessagesResponse.HtmlHeader + MessagesResponse.NewLine;

                byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);

                string responseHttp = MessagesResponse.HttpResponseOK + MessagesResponse.NewLine
                    + "Server: " + MessagesResponse.ServerName + " " + MessagesResponse.ServerVersion + MessagesResponse.NewLine
                    + "Content-Type: " + MessagesResponse.ContentHTML + MessagesResponse.NewLine
                    + "Content-Lenght: " + responseBodyBytes.Length + MessagesResponse.NewLine
                    + MessagesResponse.NewLine;

                var responseHeaderBytes = EncodingUtfToBytes(responseHttp);

                await clientStream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                await clientStream.WriteAsync(responseBodyBytes, 0, responseBodyBytes.Length);
            }

            client.Close();
        }

        private byte[] EncodingUtfToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        private async Task<string> EncodingClientData(List<byte> clientData)
        {
            return Encoding.UTF8.GetString(clientData.ToArray());
        }
    }
}