using System.Net;

namespace CommonLib
{
    public class CommandResponse
    {
        public string Content { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }



        public static CommandResponse Create(HttpStatusCode code, string content)
        {
            return new CommandResponse { HttpStatusCode = code, Content = content };
        }

        public override string ToString()
        {
            return $"{HttpStatusCode}: {Content}";
        }

    }
}