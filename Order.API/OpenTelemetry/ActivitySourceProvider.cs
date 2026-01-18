using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.API.OpenTelemetry;

namespace Order.API.OpenTelemetry
{
  public class ActivitySourceProvider
  {
    public static ActivitySource Source =null!; //bu sınıftan üretilen dataaları opentelemtry formatına uygun üreticek.
  }
}