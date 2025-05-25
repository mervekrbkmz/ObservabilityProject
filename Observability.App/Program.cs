
using Observability.App;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

//DI CONTAINER yok console da
using var traceProvider = Sdk.CreateTracerProviderBuilder().AddSource(OpenTelemetryConstants.ActivitySourceName) //kuyruk sistemine gönderilen datanın da tracedatasını tutmak istiyorum.kuyruga gdiden mesajları dinler" //tracedatalarım jager gitmez using eklenmeli!!
.ConfigureResource(traceProvider =>
{
  //keywords verirken opentelemetry datası üretirken aşağıdaki bilgiler gelicek datanın nereden üreileceğini öğrrenmiş olacağız.
  //jager da servicenamedeki bu alanı alıp görüntüleriz.
  traceProvider.AddService(OpenTelemetryConstants.ServiceName, serviceVersion: OpenTelemetryConstants.ServiceVersion
     ).AddAttributes(new List<KeyValuePair<string, object>>()
        {
          new KeyValuePair<string, object>("host.machineName", Environment.MachineName),
          new KeyValuePair<string, object>("host.environment", "dev")}); //değiştirilebilir 
}).AddConsoleExporter().AddOtlpExporter().AddZipkinExporter(zipkinoptions =>
{
  zipkinoptions.Endpoint = new Uri("http://localhost:9411/api/v2/spans");

}).Build(); //otel formatında datayı göndermek için otlp exporter kullanıyoruz.otel formatında datayı konsola yazdırmak için console exporter kullanıyoruz.
//console da yayımlama için - //jaeger için eklendi.//zipkin uı               //consoleda yayımlamak için
var serviceHelper = new ServiceHelper();
await serviceHelper.Work(); //methodu çağırdık.
traceProvider.Shutdown(); // uygulama kapandığında trace providerı kapatır.