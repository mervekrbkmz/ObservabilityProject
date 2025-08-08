using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.App
{
  internal class ServiceHelper
  {
    //methodun ne kadar sürdüğünü görmek için
    internal async Task Work()
    {
      using var activity = ActivitySourceProvider.source.StartActivity();
      var serviceOne = new ServiceOne();

      var result = await serviceOne.MakeRequestToGoogleAsync();

      activity.SetTag("work tag", "value");
      activity.SetTag("work duration", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
      activity.AddEvent(new ActivityEvent("workkkkkk event"));
      activity.AddEvent(new ActivityEvent("work duration"));

      Console.WriteLine($"google response length: {result}"); // Use the result
      Console.WriteLine("work tamamlandı.");
    }
  }
}
