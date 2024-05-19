using WalletService.Domain;

namespace WalletService.DataService
{
    public interface ITransactionDataService
    {
        IAsyncEnumerable<Transaction> GetTransactionsAsync(Guid playerId);
        void AddTransaction(Transaction transaction);
    }
}
