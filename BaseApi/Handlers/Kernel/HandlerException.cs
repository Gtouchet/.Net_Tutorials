using System.Net;

namespace BaseApi.Handlers.Kernel;

public class HandlerException : Exception
{
    public HttpStatusCode? StatusCode { get; }

    public HandlerException(HttpStatusCode? code, string message) : base(message)
    {
        StatusCode = code ?? HttpStatusCode.InternalServerError;
    }
}
