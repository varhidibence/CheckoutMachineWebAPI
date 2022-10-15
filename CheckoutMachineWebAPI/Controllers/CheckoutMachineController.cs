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
    public ActionResult<Dictionary<string, int>> Get([FromServices] ICheckoutMachineService checkoutMachineService)
    {
      try
      {
        Dictionary<string, int> storedItems = checkoutMachineService.GetStoredItems();

        if (storedItems == null)
        {
          return NotFound();
        }

        return Ok(storedItems);
      }
      catch (Exception ex)
      {
        return BadRequest("Exception occured during getting stock process!\n" + ex.Message);
      }
    }

    // POST api/v1/Stock
    [HttpPost]
    [Route("Stock")]
    public ActionResult<Dictionary<string, int>> PostStock([FromServices] ICheckoutMachineService checkoutMachineService, 
      [FromBody] TransactionData transactionData)
    {
      try
      {
        bool correctTransactionData = checkoutMachineService.CheckTransactionData(transactionData);
        bool correctPrice = checkoutMachineService.CheckPriceIsEqual(transactionData);

        if (!correctTransactionData || !correctPrice)
        {
          return BadRequest("Transaction data given is incorrectly formatted!");
        }

        checkoutMachineService.AddCurrency(transactionData);
        return Ok(checkoutMachineService.GetStoredItems());
      }
      catch (Exception ex)
      {
        return BadRequest("Exception occured during stock money process!\n" + ex.Message);
      }
    }

    // POST api/v1/Checkout
    [HttpPost]
    [Route("Checkout")]
    public ActionResult<Dictionary<string, int>> PostCheckout([FromServices] ICheckoutMachineService checkoutMachineService,
      [FromBody] TransactionData transactionData)
    {
      try
      {
        bool correctTransactionData = checkoutMachineService.CheckTransactionData(transactionData);
        bool insertedIsEnough = checkoutMachineService.CheckInsertedIsEnough(transactionData);  

        if (!correctTransactionData)
        {
          return BadRequest("Transaction data given is incorrectly formatted!");
        }
        
        if (!insertedIsEnough)
        {
          return BadRequest($"Inserted money is not enough to pay the price!\n The price is: {transactionData.Price}.");
        }

        return Ok(checkoutMachineService.Checkout(transactionData));
      }
      catch (Exception ex)
      {
        return BadRequest("Exception occured during checkout process!\n" + ex.Message);
      }
    }

  }
}
