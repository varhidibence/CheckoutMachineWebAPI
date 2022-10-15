using CheckoutMachineWebAPI.Interfaces;

namespace CheckoutMachineWebAPI.Models
{
  public class Currency
  {
    /// <summary>
    /// The value of currency
    /// </summary>
    public string Denomination { get; set; } = String.Empty;

    /// <summary>
    /// The amount of currency
    /// </summary>
    public int Amount {get; set;} 
  }
}
