using System;
using System.Collections.Generic;
using System.Diagnostics;
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
      try
      {
       
        var result = await httpClient.GetAsync("https://www.google.com");
        var eventTags = new ActivityTagsCollection();
        eventTags.Add("userId", 30);
        if (!eventTags.ContainsKey("env"))
        {
          eventTags.Add("env", "prod");
        }
        //activity tagı
        activity.AddEvent(new("started request to google", tags: eventTags));
        activity.AddTag("request.schema", "https");
        activity.AddTag("request.metho", "get");



        var responseContent = await result.Content.ReadAsStringAsync();
        //eventtags key value seklinde gösterdim
        eventTags.Add("google body length", responseContent.Length);
        activity.AddEvent(new("completed request to google", tags: eventTags));

        var servicesTwo = new ServiceTwo();
        var fileLength = await servicesTwo.WriteToFile("hello world");

        Console.WriteLine($"file length: {fileLength}");


        return responseContent.Length;
      }
      catch (Exception ex)
      {
        activity?.SetStatus(ActivityStatusCode.Error,ex.Message);
        throw ex;
      }
    }
  }
}
