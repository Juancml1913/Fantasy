using System.Net;

namespace Fantasy.Fronted.Repositories;

public class HttpResponseWrapper<T>
{
    public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
    {
        Response = response;
        Error = error;
        HttpResponseMessage = httpResponseMessage;
    }

    public T? Response { get; set; }
    public bool Error { get; set; }
    public HttpResponseMessage HttpResponseMessage { get; set; }

    public async Task<string> GetErrorMessageAsync()
    {
        if (!Error)
        {
            return null;
        }
        var statusCode = HttpResponseMessage.StatusCode;
        if (statusCode == HttpStatusCode.NotFound)
        {
            return "Recurso no encontrado.";
        }
        else if (statusCode == HttpStatusCode.BadRequest)
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
        else if (statusCode == HttpStatusCode.Unauthorized)
        {
            return "Debes estar logueado para ejecutar esta operación.";
        }
        else if (statusCode == HttpStatusCode.Forbidden)
        {
            return "No tienes permiso para ejecutar esta operación.";
        }

        return "Ha ocurrido un error inesperado.";
    }
}