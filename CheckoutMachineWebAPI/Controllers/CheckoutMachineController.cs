using CheckoutMachineWebAPI.Interfaces;
using CheckoutMachineWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheckoutMachineWebAPI.Controllers
{
  [Route("api/v1/")]
  [ApiController]
  public class CheckoutMachineController : ControllerBase
  {
    // GET: api/v1/Stock
    [HttpGet]
    [Route("Stock")]
    public ActionResult<Dictionary<string, int>>Get([FromServices] ICheckoutMachineService checkoutMachineService)
    {
      try
      {
        List<ICurrency>? storedItems = checkoutMachineService.GetStoredItems();

        if (storedItems == null)
        {
          return NotFound();
        }

        var dict = new Dictionary<string, int>();
        storedItems.ForEach(item => dict.Add(item.Value ?? "", item.Amount));

        return Ok(dict);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    // POST api/v1/Stock
    [HttpPost]
    [Route("Stock")]
    public void PostStock([FromBody] string value)
    {
    }

    // POST api/v1/Checkout
    [HttpPost]
    [Route("Checkout")]
    public void PostCheckout([FromBody] string value)
    {
    }

  }
}
