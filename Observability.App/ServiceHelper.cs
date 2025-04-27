using System;
using System.Collections.Generic;
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

      var result =  serviceOne.MakeRequestToGoogleAsync().Result; // Await the async method

      Console.WriteLine($"google response length: {result}"); // Use the result
      Console.WriteLine("work tamamlandı.");
    }
  }
}
