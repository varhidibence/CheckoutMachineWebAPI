using CheckoutMachineWebAPI.Interfaces;
using CheckoutMachineWebAPI.Models;
using CheckoutMachineWebAPI.Services;

namespace CheckoutMachineWebAPI.Tests
{
  public class CheckoutMachineServiceTests
  {
    [Fact]
    public void TestCheckout_AmountEqualPrice()
    {
      // Arrange
      CheckoutMachineService service = new CheckoutMachineService();
      service.storedCurrencies = new List<Currency>() { new Currency { Amount = 1, Denomination = "1000" } };

      var insertedMoney = new Dictionary<string, int>
      {
        { "1000", 1 }
      };

      TransactionData transactionData = new TransactionData()
      {
        Inserted = insertedMoney,
        Price = 1000
      };


      // Act
      var cashBack = service.Checkout(transactionData);
      var amount = service.storedCurrencies.FirstOrDefault(curr => curr.Denomination == "1000")?.Amount;

      // Assert
      Assert.Empty(cashBack);
      Assert.Equal(0, amount);
    }

    [Fact]
    public void TestCheckout_ProperCashback()
    {
      // Arrange
      CheckoutMachineService service = new CheckoutMachineService();
      service.storedCurrencies.Add( new Currency { Amount = 2, Denomination = "200" });
      service.storedCurrencies.Add( new Currency { Amount = 2, Denomination = "100" });

      var insertedMoney = new Dictionary<string, int>
      {
        { "1000", 1 }
      };

      TransactionData transactionData = new TransactionData()
      {
        Inserted = insertedMoney,
        Price = 700
      };


      // Act
      var cashBack = service.Checkout(transactionData);
      var actual = CheckoutMachineService.DotProduct(cashBack);
      var amountof200 = service.storedCurrencies.FirstOrDefault(curr => curr.Denomination == "200")?.Amount;
      var amountof100 = service.storedCurrencies.FirstOrDefault(curr => curr.Denomination == "100")?.Amount;

      // Assert
      Assert.Equal(300, actual);
      Assert.Equal(1, amountof100);
      Assert.Equal(1, amountof200);
    }

    [Fact]
    public void TestCheckout_CannotGiveCashback()
    {
      // Arrange
      CheckoutMachineService service = new CheckoutMachineService();
      service.storedCurrencies.Add(new Currency { Amount = 2, Denomination = "200" });

      var insertedMoney = new Dictionary<string, int>
      {
        { "1000", 1 }
      };

      TransactionData transactionData = new TransactionData()
      {
        Inserted = insertedMoney,
        Price = 700
      };

      // Act
      var ex = Assert.Throws<Exception>(() => { service.Checkout(transactionData); });

      // Assert
      Assert.Equal(typeof(Exception), ex.GetType());
    }

    [Fact]
    public void TestCheckout_ProperCashbackOneItem()
    {
      // Arrange
      CheckoutMachineService service = new CheckoutMachineService();
      service.storedCurrencies.Add(new Currency { Amount = 1, Denomination = "500" });

      var insertedMoney = new Dictionary<string, int>
      {
        { "1000", 1 }
      };

      TransactionData transactionData = new TransactionData()
      {
        Inserted = insertedMoney,
        Price = 500
      };


      // Act
      var cashBack = service.Checkout(transactionData);
      var actual = CheckoutMachineService.DotProduct(cashBack);

      // Assert
      Assert.Equal(500, actual);
    }
  }
}
