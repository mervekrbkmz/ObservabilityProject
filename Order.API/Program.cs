using OpenTelemetry.Resources;
using OpenTelemetry.Shared;
using Order.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<OrderService>();//DI Container. İhtiyacım oldugunda kullanbilmem için constructor olarak kullanabilmek.
builder.Services.AddOpenTelemetryExt(builder.Configuration); //OpenTelemetry ayarlarını ekledim.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseHttpsRedirection();
app.MapControllers();
app.Run();
