﻿using CheckoutMachineWebAPI.Interfaces;
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
    public ActionResult<List<ICurrency>> Get([FromServices] ICheckoutMachineService checkoutMachineService)
    {
      return checkoutMachineService.GetStoredItems();
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
