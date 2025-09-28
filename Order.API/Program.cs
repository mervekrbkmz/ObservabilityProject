using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Order.API;
using Order.API.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

builder.Services.Configure<OpenTelemetryConstants>(builder.Configuration.GetSection("OpenTelemetry")); //appsettingden register ettim.
var openTelemetryConstants = builder.Configuration
    .GetSection("OpenTelemetry")
    .Get<OpenTelemetryConstants>()!;
//apsettings i�eriisndeki datay� ald�m.optionspoattern tip g�venli.BURADAN NULL DEGER GELEMEZ.

builder.Services.AddOpenTelemetry().WithTracing(options =>
{
  options.AddSource(openTelemetryConstants.ActivitySourceName).ConfigureResource(resource =>
  {
    resource.AddService(openTelemetryConstants.ServiceName, serviceVersion: openTelemetryConstants.ServiceVersion);
  });
  options.AddAspNetCoreInstrumentation(coreOptions => {

    coreOptions.Filter = (context) =>
    {
      if (!string.IsNullOrEmpty(context.Request.Path.Value))
      {
        return context.Request.Path.Value.Contains("api",StringComparison.InvariantCulture);
      }
      return false;
    };

  });//Traceleme INSTURMANTA�N �LE

  options.AddHttpClientInstrumentation();
  options.AddConsoleExporter();//export etme console
  options.AddOtlpExporter(); //expoert etme jaeger i�in
});
ActivitySourceProvider.Source = new System.Diagnostics.ActivitySource(openTelemetryConstants.ActivitySourceName); //OpenTelemetryConstants s�n�f�ndan ald���m�z ActivitySourceName ile yeni bir ActivitySource olu�turduk.ACT�V�TYSORUCE B�LG�LER�N� BU �EK�LDE ALDIM.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
  var forecast = Enumerable.Range(1, 5).Select(index =>
      new WeatherForecast
      (
          DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          Random.Shared.Next(-20, 55),
          summaries[Random.Shared.Next(summaries.Length)]
      ))
      .ToArray();
  return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
