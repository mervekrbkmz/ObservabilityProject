﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.OpenTelemetry
{
  public class OpenTelemetryConstants
  {
    public string ServiceName { get; set; } = null!; //null olamaz.
    public string ServiceVersion { get; set; } = null!;
    public string ActivitySourceName { get; set; } =null!;
  }
}