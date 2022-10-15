using CheckoutMachineWebAPI.Interfaces;
using CheckoutMachineWebAPI.Models;
using System.Linq;

namespace CheckoutMachineWebAPI.Services
{
  public class CheckoutMachineService : ICheckoutMachineService
  {
    public List<ICurrency> _currencies = new List<ICurrency>();

    private List<string> typesOfCurrency = new List<string>(); // TODO: store ind DB

    public CheckoutMachineService()
    {
      typesOfCurrency = new List<string>()
      {
        "5", "10", "20", "50", "100", "200", "500", "1000", "2000", "5000", "10000", "20000",
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

      return correctTypeOfMoney && amountsArePositives;

    }

    public bool CheckPriceIsEqual(ITransactionData transactionData)
    {
      return transactionData.Price == SumTransaction(transactionData);
    }

    public bool CheckInsertedIsEnough(ITransactionData transactionData)
    {
      return transactionData.Price <= SumTransaction(transactionData);
    }

    private static int SumTransaction(ITransactionData transactionData)
    {
      return DotProduct(transactionData.Inserted);
    }

    public static int DotProduct(Dictionary<string, int> inserted)
    {
      return inserted.Select(currency => currency.Value * Int32.Parse(currency.Key)).Sum();
    }

    public Dictionary<string, int> Checkout(ITransactionData transactionData)
    {
      var price = transactionData.Price; // 3400 HUF
      var sumOfGivenMoney = SumTransaction(transactionData); // given money: 4000 HUF

      var difference = sumOfGivenMoney - price; // 600

      if (difference == 0)
      {
        return new Dictionary<string, int>();
      }

      // sorted stored money desc -> {1000 : 1, 500 : 2, 100}
      List<ICurrency> ordered = _currencies.OrderByDescending(curr => int.Parse(curr.Denomination)).ToList();

      var biggestChange = ordered.FirstOrDefault(storedCurr => int.Parse(storedCurr.Denomination) < difference);

      Dictionary<string, int> changeBack = new Dictionary<string, int>();

      foreach (var currencyInStore in ordered)
      {
        
        if (int.Parse(currencyInStore.Denomination) <= difference) // 500 < 900 HUF
        {
          if(currencyInStore.Amount > 0)
          {
            changeBack.Add(currencyInStore.Denomination, 1);
          }
          else
          {
            // 500: 0 pc -> next curr
          }
        }
        else
        {
          // 1000 > 900 -> next currency
        }
      }

      int sumOfChangeBack = DotProduct(changeBack);

      if (sumOfChangeBack != difference)
      {
        throw new Exception("Cannot give properly amount of cashback!");
      }

      return changeBack;

    }
  }
}
