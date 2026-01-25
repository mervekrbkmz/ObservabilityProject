
// using Observability.App;
// using OpenTelemetry;
// using OpenTelemetry.Resources;
// using OpenTelemetry.Trace;

// using var traceProvider = Sdk.CreateTracerProviderBuilder().AddSource(OpenTelemetryConstants.ActivitySourceName)
// .ConfigureResource(traceProvider =>
// {

//   traceProvider.AddService(OpenTelemetryConstants.ServiceName, serviceVersion: OpenTelemetryConstants.ServiceVersion
//      ).AddAttributes(new List<KeyValuePair<string, object>>()
//         {
//           new KeyValuePair<string, object>("host.machineName", Environment.MachineName),
//           new KeyValuePair<string, object>("host.environment", "dev")});
// }).AddConsoleExporter().AddOtlpExporter().AddZipkinExporter(zipkinoptions =>
// {
//   zipkinoptions.Endpoint = new Uri("http://localhost:9411/api/v2/spans");

// }).Build(); //otel formatında datayı göndermek için otlp exporter kullanıyoruz.otel formatında datayı konsola yazdırmak için console exporter kullanıyoruz.
// //console da yayımlama için - //jaeger için eklendi.//zipkin uı               //consoleda yayımlamak için
// var serviceHelper = new ServiceHelper();
// await serviceHelper.Work(); //methodu çağırdık.
// traceProvider.Shutdown(); // uygulama kapandığında trace providerı kapatır.