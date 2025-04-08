namespace BibliotecaAPI;

public class BloqueaPeticionMiddleware
{
    private readonly RequestDelegate _next;

    public BloqueaPeticionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        if (contexto.Request.Path == "/bloquado") 
        {
         contexto.Response.StatusCode = 403;
         await contexto.Response.WriteAsync("Acceso Denegado");
        }
        else
        { 
            await _next.Invoke(contexto);
        } 
    }
}

public static class BloqueaPeticionMiddlewareExtensions
{
    public static IApplicationBuilder UseBloquePeticionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BloqueaPeticionMiddleware>();
    }
}