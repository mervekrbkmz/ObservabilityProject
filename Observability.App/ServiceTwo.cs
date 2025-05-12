using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.App
{
  internal class ServiceTwo
  {
    internal async Task<int> WriteToFile(string text)
    {
      using var activity = ActivitySourceProvider.source.StartActivity(); //using önemli
      await File.WriteAllTextAsync("myfile.txt", text);


      using (var span =activity)
      {
        activity.SetTag("user.id", "56585");
        activity.SetTag("env", "dev");
        Console.WriteLine("Span çalışıyor.");
        await Task.Delay(300);
      }

      return (await File.ReadAllTextAsync("myfile.txt")).Length;//.Result.Length var olan async bozup sync çevirir ve işlemi yapıcak olan threadi bloklar.Best bractices değil.Suanki await ile kullanım
    }
  }
}
