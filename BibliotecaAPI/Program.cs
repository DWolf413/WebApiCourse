using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Area de Servicios
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
    opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

//Area de middlewares
app.MapControllers();

app.Run();