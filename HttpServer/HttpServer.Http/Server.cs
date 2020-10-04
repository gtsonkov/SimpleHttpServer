using HttpServer.Http.Contracts;
using System;
using System.Collections.Generic;
using HttpServer.Http.Constants;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using IO;
using System.Linq;

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

                var request = new HttpRequest(clientRequest);

                await this._logger.WriteLineAsync(clientRequest);

                //Header
                string responseHtml = MessagesResponse.HtmlHeader + ConstantData.NewLine
                    + request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;

                byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);

                string responseHttp = MessagesResponse.HttpResponseOK + ConstantData.NewLine
                    + "Server: " + MessagesResponse.ServerName + " " + MessagesResponse.ServerVersion + ConstantData.NewLine
                    + "Content-Type: " + MessagesResponse.ContentHTML + ConstantData.NewLine
                    + "Content-Lenght: " + responseBodyBytes.Length + ConstantData.NewLine
                    + ConstantData.NewLine;

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