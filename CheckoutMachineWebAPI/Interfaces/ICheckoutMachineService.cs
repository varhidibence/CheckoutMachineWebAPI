namespace CheckoutMachineWebAPI.Interfaces
{
  /// <summary>
  /// Interface for checkout and check the stored items.
  /// </summary>
  public interface ICheckoutMachineService
  {

    /// <summary>
    /// Gets the currently stored items in the machine/>
    /// </summary>
    /// <returns>currently stored items/></returns>
    Dictionary<string, int> GetStoredItems();

    /// <summary>
    /// Pay the price with the given money
    /// </summary>
    /// <param name="inserted">amount of money to pay the price with</param>
    /// <param name="price">amount of money to be paid</param>
    /// <returns>change given back</returns>
    List<ICurrency> Checkout(KeyValuePair<string, int> inserted, int price);

    void AddCurrency(ITransactionData transactionData);

    bool CheckTransactionData(ITransactionData transactionData);
  }
}
