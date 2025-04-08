using System.Text.Json.Serialization;
using BibliotecaAPI;
using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Area de Servicios
builder.Services.AddControllers().AddJsonOptions(opciones => 
    opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //Ignora ciclos infinitos
builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseSqlServer("name=DefaultConnection"));

//builder.Services.AddTransient<RespositorioValores>();

builder.Services.AddTransient<IRespositorioValores, RespositorioValores>();
builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

//Area de middlewares
var app = builder.Build();

app.Use(async (contexto, next) =>
{
    var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("$Peticion {contenxto.Request.Method} {contenxto.Request.Path}");
    await next.Invoke();
    logger.LogInformation("$Resouesta {contenxto.Response.StatusCode}");
});

app.Use(async (contexto, next) =>
{
    if (contexto.Request.Path == "/bloquado")
    {
        contexto.Response.StatusCode = 403;
        await contexto.Response.WriteAsync("Acceso Denegado");
    }
    else
    {
        await next.Invoke();
    } 
});

app.MapControllers();

app.Run();