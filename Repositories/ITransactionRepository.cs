using WalletService.Domain;

namespace WalletService.Repositories
{
    public interface ITransactionRepository
    {
        IAsyncEnumerable<Transaction> GetTransactionsAsync(Guid playerId);
        void AddTransaction(Transaction transaction);
    }
}
