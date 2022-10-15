using CheckoutMachineWebAPI.Interfaces;

namespace CheckoutMachineWebAPI.Services
{
  public class CheckoutMachineService : ICheckoutMachineService
  {
    public KeyValuePair<string, int> Checkout(KeyValuePair<string, int> inserted, int price)
    {
      throw new NotImplementedException();
    }

    public KeyValuePair<string, int> GetStoredItems()
    {
      throw new NotImplementedException();
    }
  }
}
