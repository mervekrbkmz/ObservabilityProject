
using Observability.App;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

//DI CONTAINER yok console da
var traceProvider = Sdk.CreateTracerProviderBuilder().AddSource(OpenTelemetryConstants.ActivitySourceName) //kuyruk sistemine gönderilen datanın da tracedatasını tutmak istiyorum.kuyruga gdiden mesajları dinler"
.ConfigureResource(traceProvider =>
{
  //keywords verirken opentelemetry datası üretirken aşağıdaki bilgiler gelicek datanın nereden üreileceğini öğrrenmiş olacağız.
  traceProvider.AddService(OpenTelemetryConstants.ServiceName, serviceVersion:OpenTelemetryConstants.ServiceVersion
     ).AddAttributes(new List<KeyValuePair<string, object>>()
        {
          new KeyValuePair<string, object>("host.machineName", Environment.MachineName),
          new KeyValuePair<string, object>("host.environment", "dev")}); //değiştirilebilir 
}).AddConsoleExporter().Build(); //consoleda yayımlamak için.

var serviceHelper = new ServiceHelper();
await serviceHelper.Work(); //methodu çağırdık.
traceProvider.Shutdown(); // uygulama kapandığında trace providerı kapatır.