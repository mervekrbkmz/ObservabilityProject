using System.Diagnostics;
using Order.API.OrderServices;

namespace Order.API.Models
{
    public class OrderService

    {

        // public Task CreateAsync(OrderCreateRequestDto orderCreateRequest)
        // {
        // //     //ana aktivite 
        // //     Activity.Current?.SetTag("Main Activity Order User Id", "");
        // //  //child activity
        // //     using var activity = ActivitySourceProvider.Source.StartActivity();
        // //     var eventTags = new ActivityTagsCollection
        // //     {
        // //         { "UserId", orderCreateRequest.UserId }
        // //     };
        // //     var activityEvent = new ActivityEvent("Sipariş süreci başladı!", default, eventTags);
        // //     activity?.AddEvent(activityEvent);

        // //     activity?.SetTag("Order User Id",orderCreateRequest.UserId);
        // //     //veritabanına kaydettim
        // //     activity?.AddEvent(new ("Sipariş süreci tamamlandı!"));
        // //     return Task.CompletedTask;
        // }
    }
}