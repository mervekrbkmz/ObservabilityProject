using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController : ControllerBase
  {

    [HttpGet("get-order")]
    public IActionResult GetOrder()
    {
      var c = 10;
      var y = 0;
      var a = c / y;
      
      return Ok("Order Service is working...");
    }
  }
}
