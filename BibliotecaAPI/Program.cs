var builder = WebApplication.CreateBuilder(args);

//Area de Servicios
builder.Services.AddControllers();

var app = builder.Build();

//Area de middlewares
app.MapControllers();

app.Run();