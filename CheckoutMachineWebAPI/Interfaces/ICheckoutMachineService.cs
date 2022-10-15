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

    /// <summary>
    /// Add new currency to the machine or increase the amount of existing currency.
    /// </summary>
    /// <param name="transactionData"></param>
    void AddCurrency(ITransactionData transactionData);

    /// <summary>
    /// Checks that the transaction data given is correctly formatted
    /// </summary>
    /// <param name="transactionData"></param>
    /// <returns></returns>
    bool CheckTransactionData(ITransactionData transactionData);
  }
}
