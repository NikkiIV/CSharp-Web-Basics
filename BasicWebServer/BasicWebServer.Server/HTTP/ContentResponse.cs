﻿using BasicWebServer.Server.Common;
using System.Text;

namespace BasicWebServer.Server.HTTP
{
    public class ContentResponse : Response
    {
        // имаме accept the action through the constructor as an optional parameter -> Action<Request, Response> preRenderAction = null
        // в контекста на FORM - да можем да проеним респонса
        public ContentResponse(string content, string contentType,
            Action<Request, Response> preRenderAction = null)
            : base(StatusCode.OK)
        {
            Guard.AgainstNull(content);
            Guard.AgainstNull(contentType);

            this.PreRenderAction = preRenderAction;

            this.Headers.Add(Header.ContentType, contentType);

            this.Body = content;
        }

        public override string ToString()
        {
            if (this.Body != null)
            {
                var contentLength = Encoding.UTF8.GetByteCount(this.Body).ToString();
                this.Headers.Add(Header.ContentLength, contentLength);
            }

            return base.ToString();
        }
    }
}