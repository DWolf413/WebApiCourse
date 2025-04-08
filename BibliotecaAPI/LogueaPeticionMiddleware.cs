namespace BibliotecaAPI;

public class LogueaPeticionMiddleware
{
    private readonly RequestDelegate _next;

    public LogueaPeticionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("$Peticion {contenxto.Request.Method} {contenxto.Request.Path}");
        await _next.Invoke(contexto);
        logger.LogInformation("$Resouesta {contenxto.Response.StatusCode}");
    }
}

public static class LogueaPeticionMiddlewareExtensions
{
    public static IApplicationBuilder UseLogueaPeticionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogueaPeticionMiddleware>();
    }
}
