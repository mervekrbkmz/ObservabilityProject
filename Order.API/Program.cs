using Common.Shared;
using OpenTelemetry.Resources;
using OpenTelemetry.Shared;
using Order.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<Microsoft.IO.RecyclableMemoryStreamManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<OrderService>();//DI Container. İhtiyacım oldugunda kullanbilmem için constructor olarak kullanabilmek.
builder.Services.AddOpenTelemetryExt(builder.Configuration); //OpenTelemetry ayarlarını ekledim.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.API v1");
    });
    //--https://learn.microsoft.com/en-us/aspnet/core/fundamentals/target-aspnetcore?view=aspnetcore-10.0&tabs=visual-studio
    //library içi app reference eklenmesi.
}
app.UseHttpsRedirection();
app.UseMiddleware<RequestAndResponseActivityMiddleware>(); //Request ve Response içeriğini activity tag olarak eklemek için middleware'i kullanıyorum.  
app.UseAuthorization();
app.MapControllers();
app.Run();
