using WalletService.Domain;

namespace WalletService.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        IAsyncEnumerable<Transaction> GetTransactionsAsync(Guid playerId);
        void AddTransaction(Transaction transaction);
    }
}
