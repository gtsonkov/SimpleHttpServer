﻿using HttpServer.Http.Constants;
using HttpServer.Http.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpServer.Http
{
    public class HttpResponse
    {
        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.Ok)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.Cookies = new HashSet<Cookie>();

            this.StatusCode = statusCode; //Default value Ok
            this.Body = body;

            this.Headers = new HashSet<Header>();

            this.Headers.Add(new Header(ConstantData.ContenTypeHeader, contentType));
            this.Headers.Add(new Header(ConstantData.ContentLegthHeader, body.Length.ToString()));
        }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public byte[] Body { get; set; }

        public override string ToString()
        {
            StringBuilder rb = new StringBuilder();

            rb.Append($"{MessagesResponse.HttpVersion} {(int)this.StatusCode} {this.StatusCode}" + ConstantData.NewLine);
            foreach (var header in this.Headers)
            {
                rb.Append(header.ToString() + ConstantData.NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                rb.Append($"{ConstantData.SetCookieHeader}: {cookie}" + ConstantData.NewLine);
            }

            rb.Append(ConstantData.NewLine);

            return rb.ToString();
        }
    }
}