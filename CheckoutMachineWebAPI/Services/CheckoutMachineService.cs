using CheckoutMachineWebAPI.Interfaces;
using CheckoutMachineWebAPI.Models;
using System.Linq;

namespace CheckoutMachineWebAPI.Services
{
  public class CheckoutMachineService : ICheckoutMachineService
  {
    public List<Currency> storedCurrencies = new List<Currency>();

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
      storedCurrencies.ForEach(item => dict.Add(item.Denomination ?? "", item.Amount));

      return dict;
    }

    public void AddCurrency(TransactionData transactionData)
    {      
      foreach (var currency in transactionData.Inserted)
      {
        Currency? existingItem = storedCurrencies?.FirstOrDefault(storedCurrency => storedCurrency.Denomination == currency.Key);

        if (existingItem == null)
        {
          storedCurrencies?.Add(new Currency()
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

    public bool CheckTransactionData(TransactionData transactionData)
    {
      bool correctTypeOfMoney =  transactionData.Inserted.Keys.All(key => typesOfCurrency.Contains(key));
      bool amountsArePositives = transactionData.Inserted.Values.All(amount => amount > 0);

      return correctTypeOfMoney && amountsArePositives;

    }

    public bool CheckPriceIsEqual(TransactionData transactionData)
    {
      return transactionData.Price == SumTransaction(transactionData);
    }

    public bool CheckInsertedIsEnough(TransactionData transactionData)
    {
      return transactionData.Price <= SumTransaction(transactionData);
    }

    private static int SumTransaction(TransactionData transactionData)
    {
      return DotProduct(transactionData.Inserted);
    }

    public static int DotProduct(Dictionary<string, int> inserted)
    {
      return inserted.Select(currency => currency.Value * Int32.Parse(currency.Key)).Sum();
    }

    public Dictionary<string, int> Checkout(TransactionData transactionData)
    {
      var price = transactionData.Price; // 3400 HUF
      var sumOfGivenMoney = SumTransaction(transactionData); // given money: 4000 HUF

      var difference = sumOfGivenMoney - price; // 600

      if (difference == 0)
      {
        WithdrawChangeback(transactionData.Inserted);
        return new Dictionary<string, int>();
      }

      // sorted stored money desc -> {1000 : 1, 500 : 2, 100}
      List<Currency> ordered = storedCurrencies.OrderByDescending(curr => int.Parse(curr.Denomination)).ToList();

      var biggestChange = ordered.FirstOrDefault(storedCurr => int.Parse(storedCurr.Denomination) < difference);

      Dictionary<string, int> changeBack = new Dictionary<string, int>();
      int sumOfChangeBack = 0;

      foreach (var currencyInStore in ordered)
      {
        if (difference == sumOfChangeBack)
        {
          break;
        }
        if (int.Parse(currencyInStore.Denomination) <= difference) // 500 < 900 HUF
        {
          if(currencyInStore.Amount > 0)
          {
            changeBack.Add(currencyInStore.Denomination, 1);
            sumOfChangeBack += int.Parse(currencyInStore.Denomination);
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

      if (sumOfChangeBack != difference)
      {
        throw new Exception("Cannot give properly amount of cashback!");
      }

      WithdrawChangeback(changeBack);

      return changeBack;

    }

    private void WithdrawChangeback(Dictionary<string, int> changeBack)
    {
      foreach (KeyValuePair<string, int> currency in changeBack)
      {
        Currency? existingItem = storedCurrencies?.FirstOrDefault(storedCurrency => storedCurrency.Denomination == currency.Key);

        if (existingItem == null)
        {
          throw new Exception("Exception occured during withdraw cashback!");
        }

        existingItem.Amount -= 1;
      }
    }
  }
}
