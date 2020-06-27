
using Newtonsoft.Json.Linq;
using System;
using System.Net;


namespace cinema_core.ErrorHandle
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public CustomException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public CustomException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public CustomException(HttpStatusCode statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public CustomException(HttpStatusCode statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }
    }
}
