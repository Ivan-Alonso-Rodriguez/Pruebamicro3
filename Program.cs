using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using imagenes_ms.Models;
using imagenes_ms.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // <-- Swagger FIX

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<GridFsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // <-- Swagger FIX
    app.UseSwaggerUI(); // <-- Swagger FIX
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
