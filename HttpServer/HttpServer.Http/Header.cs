﻿using System.Linq;

namespace HttpServer.Http
{
    public class Header
    {
        public Header(string headerLine)
        {
            var headerParts = headerLine
                .Split(new string[] { ": " }
            , 2
            , System.StringSplitOptions.None)
            .ToArray();

            this.Name = headerParts[0];

            this.Value = headerParts[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}