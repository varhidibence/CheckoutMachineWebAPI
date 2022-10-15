using CheckoutMachineWebAPI.Interfaces;

namespace CheckoutMachineWebAPI.Models
{
  public class TransactionData
  {
    /// <summary>
    /// Inserted money with the name and the amount of currency
    /// </summary>
    public Dictionary<string, int> Inserted { get; set; } = new Dictionary<string, int>();

    /// <summary>
    /// The price used in checkout and during stock currency
    /// </summary>
    public int Price { get; set; }
  }
}
