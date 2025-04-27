using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.App
{
  internal class ServiceOne
  {
    static HttpClient httpClient = new HttpClient(); //ya static ya singleton olmalı. Her defasında oluşturulmamalı. singleton oldugunda program.cs her defasında httpclient socket açar. Ve bir süre socket hatası alırız.Bu yüzden static bestpractices olur.
    internal async Task<int> MakeRequestToGoogleAsync()
    {
      using var activity = ActivitySourceProvider.source.StartActivity();
      var result = await httpClient.GetAsync("https://www.google.com"); // Send request to Google.

      var responseContent = await result.Content.ReadAsStringAsync(); // Read response data.
      return responseContent.Length; // Return the length of the content.
    }
  }
}
