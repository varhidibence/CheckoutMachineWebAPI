using CheckoutMachineWebAPI.Interfaces;
using CheckoutMachineWebAPI.Models;
using System.Linq;

namespace CheckoutMachineWebAPI.Services
{
  public class CheckoutMachineService : ICheckoutMachineService
  {
    private List<ICurrency> _currencies = new List<ICurrency>();

    private List<string> typesOfCurrency = new List<string>(); // TODO: store ind DB

    public CheckoutMachineService()
    {
      typesOfCurrency = new List<string>()
      {
        "5", "10", "20", "50", "100", "200", "500", "1000", "2000", "5000", "10000", "20000",
      };
    }

    public List<ICurrency> Checkout(KeyValuePair<string, int> inserted, int price)
    {
      return new List<ICurrency>()
      {
        new Currency { Denomination = "1000", Amount = 2},
        new Currency { Denomination = "500", Amount = 3},
      };
    }

    public Dictionary<string, int> GetStoredItems()
    {

      var dict = new Dictionary<string, int>();
      _currencies.ForEach(item => dict.Add(item.Denomination ?? "", item.Amount));

      return dict;
    }

    public void AddCurrency(ITransactionData transactionData)
    {      
      foreach (var currency in transactionData.Inserted)
      {
        ICurrency? existingItem = _currencies?.FirstOrDefault(storedCurrency => storedCurrency.Denomination == currency.Key);

        if (existingItem == null)
        {
          _currencies?.Add(new Currency()
          {
            Amount = currency.Value,
            Denomination = currency.Key
          });
        }
        else
        {
          existingItem.Amount += currency.Value;
        }

      }
    }

    public bool CheckTransactionData(ITransactionData transactionData)
    {
      bool correctTypeOfMoney =  transactionData.Inserted.Keys.All(key => typesOfCurrency.Contains(key));
      bool amountsArePositives = transactionData.Inserted.Values.All(amount => amount > 0);

      return correctTypeOfMoney && amountsArePositives && CheckPrice(transactionData);

    }

    private bool CheckPrice(ITransactionData transactionData)
    {
      return transactionData.Price == transactionData.Inserted.Select(currency => currency.Value * Int32.Parse(currency.Key)).Sum();
    }
  }
}
