namespace CheckoutMachineWebAPI.Interfaces
{
  public interface ICurrency
  {
    string Denomination { get; set; }

    int Amount { get; set; }
  }
}
