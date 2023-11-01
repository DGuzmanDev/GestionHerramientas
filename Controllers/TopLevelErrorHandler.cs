
namespace GestionHerramientas.Controllers
{
    public class TopLevelErrorHandler
    {
        public static void ManejarError(Exception error, string controlador, string endpoint, ILogger logger)
        {
            logger.LogError($"Manejando error de alto nivel del endpoint {endpoint} del controlador {controlador}. Razon: {error.Message}");

            if (error is HttpError httpError)
            {
                throw httpError;
            }
            else if (error is ArgumentNullException || error is ArgumentException)
            {
                throw new HttpError.BadRequest($"La solicitud no puede ser procesada: {error.Message}");
            }
            else
            {
                throw new HttpError.InternalServerError($"Error inesperado del servidor: {error.Message}");
            }
        }
    }
}