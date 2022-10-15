namespace CheckoutMachineWebAPI.Interfaces
{
  public interface ICurrency
  {
    string? Value { get; set; }

    int Amount { get; set; }
  }
}
