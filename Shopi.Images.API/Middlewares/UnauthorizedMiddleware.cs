using System.Net;
using Newtonsoft.Json;
using Shopi.Core.Exceptions;

namespace Shopi.Images.API.Middlewares;

public class UnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                Message = "Não autorizado",
                StatusCode = StatusCodes.Status401Unauthorized,
                Errors = new List<string> { "Faça login para acessar este recurso." }
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
        else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                Message = "Ação proíbida",
                StatusCode = StatusCodes.Status403Forbidden,
                Errors = new List<string> { "Acesso negado. Você não tem permissão para realizar esta ação." }
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}