using CheckoutMachineWebAPI.Interfaces;

namespace CheckoutMachineWebAPI.Models
{
  public class Currency : ICurrency
  {
    public string? Value {get; set;}

    public int Amount {get; set;} 
  }
}
