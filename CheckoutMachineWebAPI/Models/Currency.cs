using CheckoutMachineWebAPI.Interfaces;

namespace CheckoutMachineWebAPI.Models
{
  public class Currency : ICurrency
  {
    public string Denomination { get; set; } = String.Empty;

    public int Amount {get; set;} 
  }
}
