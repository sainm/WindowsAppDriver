using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CommonLib
{
    public class HttpResponseHelper
    {
        private const string JsonContentType = "application/json;charset=UTF-8";

        private const string PlainTextContentType = "text/plain";

        private static Dictionary<HttpStatusCode, string> _statusCodeDescriptors;

        public static Dictionary<HttpStatusCode, string> StatusCodeDescriptors
        {
            get
            {
                return _statusCodeDescriptors
                       ?? (_statusCodeDescriptors =
                           new Dictionary<HttpStatusCode, string>
                           {
                               {HttpStatusCode.OK, "OK"},
                               {HttpStatusCode.BadRequest, "Bad Request"},
                               {HttpStatusCode.NotFound, "Not Found"},
                               {HttpStatusCode.NotImplemented, "Not Implemented"}
                           });
            }
        }

        public static bool IsClientError(int code)
        {
            return code >= 400 && code < 500;
        }

        public static string ResponseString(HttpStatusCode statusCode, string content)
        {
            var contentType = IsClientError((int) statusCode) ? PlainTextContentType : JsonContentType;

            StatusCodeDescriptors.TryGetValue(statusCode, out var statusDescription);

            var responseString = new StringBuilder();
            responseString.AppendLine($"HTTP/1.1 {(int) statusCode} {statusDescription}");
            responseString.AppendLine($"Content-Type: {contentType}");
            responseString.AppendLine("Connection: close");
            responseString.AppendLine(string.Empty);
            responseString.AppendLine(content);

            return responseString.ToString();
        }
    }
}