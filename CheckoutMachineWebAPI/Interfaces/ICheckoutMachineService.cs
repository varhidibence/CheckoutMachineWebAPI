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
    /// <param name="transactionData">transaction with given money and amount of purchase </param>
    /// <returns>change given back</returns>
    Dictionary<string, int> Checkout(ITransactionData transactionData);

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

    bool CheckPriceIsEqual(ITransactionData transactionData);

    bool CheckInsertedIsEnough(ITransactionData transactionData);
  }
}
