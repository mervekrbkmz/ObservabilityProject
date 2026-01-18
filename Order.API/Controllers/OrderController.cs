using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.OrderServices;

namespace Order.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly OrderService _orderService;
    public OrderController(OrderService orderService)
    {
      _orderService = orderService;
    }
    [HttpGet("get-order")]
    public IActionResult GetOrder()
    {

      //exception örneği
      // var c = 10;
      // var y = 0;
      // var a = c / y;

      return Ok("Order Service is working...");
    }
    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateRequestDto requestDto)
    {
      await _orderService.CreateAsync(requestDto);

      return Ok("Order Service is working...");
    }
  }
}
