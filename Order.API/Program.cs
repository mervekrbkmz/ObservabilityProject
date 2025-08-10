var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

builder.Services.Configure<OpenTelemetryConstants>(builder.Configuration.GetSection("OpenTelemetry")); //appsettingden register ettik.
builder.Services.AddOpenTelemetry.WithMTracing(options =>
{
var opentlemetetryConstants = (builder.Configuration.Get<OpenTelemetryConstants>())!; //apsettings içeriisndeki datayı aldım.
    builder.AddSource(OpenTelemetryConstants.ActivitySourceName).ConfigureResource(resource =>
    {
        resource.AddService(OpenTelemetryConstants.ServiceName, serviceVersion: OpenTelemetryConstants.ServiceVersion);
    });
options.AddAspNetCoreInstrumentation();
    options.AddHttpClientInstrumentation();
    options.AddSqlClientInstrumentation();
    options.AddConsoleExporter();
    options.AddOtlpExporter(); //jaeger için
});
ActivitySourceProvider.source = new System.Diagnostics.ActivitySource(OpenTelemetryConstants.ActivitySourceName); //OpenTelemetryConstants sınıfından aldığımız ActivitySourceName ile yeni bir ActivitySource oluşturduk.
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
    var forecast =  Enumerable.Range(1, 5).Select(index =>
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
