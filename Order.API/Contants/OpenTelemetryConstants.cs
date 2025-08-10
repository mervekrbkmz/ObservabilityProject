using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Contants
{
  public class OpenTelemetryConstants
  {
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceVersion { get; set; } = string.Empty;
    public string ActivitySourceName { get; set; } = string.Empty;
  }
}

