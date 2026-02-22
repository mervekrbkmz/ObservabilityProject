using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;

namespace Common.Shared;

public class RequestAndResponseActivityMiddleware
{
    private readonly RequestDelegate _next; //next:gelen requesti bir sonraki middleware gönder demek.
    private readonly RecyclableMemoryStreamManager _streamManager;


    public RequestAndResponseActivityMiddleware(RequestDelegate next, RecyclableMemoryStreamManager streamManager)
    {
        _next = next;
        _streamManager = streamManager;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        await AddRequestBodyContentToActivityTag(httpContext); //Request içeriğini activity tag olarak ekliyorum.
        await AddResponseBodyContentToActivityTag(httpContext); //Response içeriğini activity tag olarak ekliyorum.
    }
    //activity tag ekledim.
    private async Task AddRequestBodyContentToActivityTag(HttpContext httpContext)
    {
        httpContext.Request.EnableBuffering(); //Requesti tekrar okuyabilmek için bufferlama işlemi yapıyorum.
        var requestBody = new StreamReader(httpContext.Request.Body);
        var requestContent = await requestBody.ReadToEndAsync(); //Request içeriğini okuyorum.
        Activity.Current?.SetTag("http.request.body", requestContent); //Activity'e request içeriğini tag olarak ekliyorum.
        httpContext.Request.Body.Position = 0; //Request body'nin pozisyonunu okuyup bitiyor sonrasın da sıfırlıyorum ki sonraki middleware'ler de request içeriğini okuyabilsin.
    }
    //reponse activity taga eklemek.
    private async Task AddResponseBodyContentToActivityTag(HttpContext httpContext)
    {
        var originalResponse = httpContext.Response.Body; //Orjinal response body'yi saklıyorum.
        await using var responseBodyStream = _streamManager.GetStream(); //Yeni bir stream oluşturuyorum.
        httpContext.Response.Body = responseBodyStream; //Response body'yi yeni stream'e yönlendiriyorum.
        await _next(httpContext); //middleware de nextten önce request işlenir(ara katman)

        responseBodyStream.Position = 0; //Response body'nin pozisyonunu sıfırlıyorum.
        var responseBodyStreamReader = new StreamReader(responseBodyStream);
        var responseContent = await responseBodyStreamReader.ReadToEndAsync(); //Response içeriğini okuyorum.
        Activity.Current?.SetTag("http.response.body", responseContent); //Activity'e response içeriğini tag olarak ekliyorum.

        responseBodyStream.Position = 0;
        await responseBodyStream.CopyToAsync(originalResponse); //Yeni stream'deki içeriği orjinal response body'ye kopyalıyorum.body direkt okursa responsebody patlar. dolaylı yoldan body memorystrema koyuyorum

    }

}
