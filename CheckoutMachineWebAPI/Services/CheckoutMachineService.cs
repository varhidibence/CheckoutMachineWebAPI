using CheckoutMachineWebAPI.Interfaces;
using CheckoutMachineWebAPI.Models;

namespace CheckoutMachineWebAPI.Services
{
  public class CheckoutMachineService : ICheckoutMachineService
  {
    public List<ICurrency> Checkout(KeyValuePair<string, int> inserted, int price)
    {
      return new List<ICurrency>()
      {
        new Currency { Value = "1000", Amount = 2},
        new Currency { Value = "500", Amount = 3},
      };
    }

    public List<ICurrency> GetStoredItems()
    {
      return new List<ICurrency>() 
      { 
        new Currency { Value = "1000", Amount = 2},
        new Currency { Value = "500", Amount = 3},
      };
    }
  }
}
