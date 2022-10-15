namespace CheckoutMachineWebAPI.Interfaces
{
  public interface ITransactionData
  {
    /// <summary>
    /// TODO
    /// </summary>
    Dictionary<string, int> Inserted { get; set; }

    int Price { get; set; }
  }
}
