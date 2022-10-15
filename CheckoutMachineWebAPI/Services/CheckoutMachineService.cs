using CheckoutMachineWebAPI.Interfaces;
using CheckoutMachineWebAPI.Models;

namespace CheckoutMachineWebAPI.Services
{
  public class CheckoutMachineService : ICheckoutMachineService
  {
    private List<ICurrency> _currencies = new List<ICurrency>();

    public List<ICurrency> Checkout(KeyValuePair<string, int> inserted, int price)
    {
      return new List<ICurrency>()
      {
        new Currency { Value = "1000", Amount = 2},
        new Currency { Value = "500", Amount = 3},
      };
    }

    public Dictionary<string, int> GetStoredItems()
    {

      var dict = new Dictionary<string, int>();
      _currencies.ForEach(item => dict.Add(item.Value ?? "", item.Amount));

      return dict;
    }

    public void AddCurrency(ITransactionData transactionData)
    {
      foreach (var currency in transactionData.Inserted)
      {
        _currencies.Add(new Currency()
        {
          Amount = currency.Value,
          Value = currency.Key
        });
      }
    }

    public bool CheckTransactionData(ITransactionData transactionData)
    {
      return true;
    }
  }
}
