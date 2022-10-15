using CheckoutMachineWebAPI.Interfaces;

namespace CheckoutMachineWebAPI.Models
{
  public class TransactionData : ITransactionData
  {
    public Dictionary<string, int> Inserted { get; set; } = new Dictionary<string, int>();

    public int Price { get; set; }
  }
}
