using System.Net;
using Microsoft.AspNetCore.WebUtilities;

[Serializable]
public class HttpError : Exception
{
    public string ReasonPrase { get; set; } = "Error Interno del Servidor";

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

    public HttpError() : base() { }

    public HttpError(string mensaje) : base(mensaje) { }

    public HttpError(string mensaje, Exception causa) : base(mensaje, causa) { }

    public class InternalServerError : HttpError
    {
        public InternalServerError() : base() { }

        public InternalServerError(string mensaje) : base(mensaje)
        {
            ReasonPrase = mensaje;
        }

        public InternalServerError(string mensaje, Exception causa) : base(mensaje, causa)
        {
            ReasonPrase = mensaje;
        }
    }

    public class BadRequest : HttpError
    {
        public BadRequest() : base()
        {
            ReasonPrase = "Solicitud invalida";
            StatusCode = HttpStatusCode.BadRequest;
        }

        public BadRequest(string mensaje) : base(mensaje)
        {
            ReasonPrase = mensaje;
            StatusCode = HttpStatusCode.BadRequest;
        }

        public BadRequest(string mensaje, Exception causa) : base(mensaje, causa)
        {
            ReasonPrase = mensaje;
            StatusCode = HttpStatusCode.BadRequest;
        }
    }

}