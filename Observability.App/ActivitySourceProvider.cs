using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.App
{
  public class ActivitySourceProvider
  {
    public static  ActivitySource source= new ActivitySource(OpenTelemetryConstants.ActivitySourceName); //bu sınıftan üretilen dataaları opentelemtry formatına uygun üreticek.
  }
}
