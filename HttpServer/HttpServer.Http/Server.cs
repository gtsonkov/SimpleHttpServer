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

        /// <summary>
        /// This method run the server, and wait for client request
        /// </summary>
        /// <param name="port">Port to lisen for client request</param>
        /// <returns></returns>
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

        /// <summary>
        /// Reading and processing the client request.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
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

                HttpResponse response;

                if (this.routTable.ContainsKey(request.Path))
                {
                    var action = this.routTable[request.Path];
                    response = action(request);
                }

                else
                {
                    string responseHtml = MessagesResponse.HtmlHeaderNotFound + ConstantData.NewLine
                        + request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;

                    byte[] responseBodyBytes = EncodingUtfToBytes(responseHtml);
                    // 404 Not Found
                    response = new HttpResponse(MessagesResponse.ContentHTML, responseBodyBytes, Enums.HttpStatusCode.NotFound);
                }

                var responseHeaderBytes = EncodingUtfToBytes(response.ToString());

                await clientStream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                await clientStream.WriteAsync(response.Body, 0, response.Body.Length);
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