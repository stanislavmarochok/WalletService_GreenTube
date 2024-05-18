using WalletService.Models;

namespace WalletService.DataService
{
    public interface ITransactionDataService
    {
        IEnumerable<Transaction> GetTransactions(Guid playerId);
        void AddTransaction(Transaction transaction);
    }
}
