using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.ErrorHandling;
using System.IO;

namespace MyNancy
{
    public class MyErrorHandler : IErrorHandler
    {
        private HttpStatusCode[] supportedStatusCodes = new[] { HttpStatusCode.NotFound, HttpStatusCode.InternalServerError };

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            if (context.Response == null)
            {
                context.Response = new Response() { StatusCode = statusCode };
            }
            context.Response.ContentType = "text/html";
            context.Response.Contents = s =>
            {
                using (var writer = new StreamWriter(s, Encoding.UTF8))
                {
                    if (statusCode == HttpStatusCode.NotFound)
                    {
                        writer.Write("Page not found: " + context.Request.Url);
                    }
                    else
                    {
                        writer.Write("error: " + context.Items["ERROR_TRACE"]);
                    }
                }
            };
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode)
        {
            return this.supportedStatusCodes.Any(s => s == statusCode);
        }

    }
}
