using WalletService.Domain;

namespace WalletService.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetTransactions(Guid playerId);
        void AddTransaction(Transaction transaction);
    }
}
