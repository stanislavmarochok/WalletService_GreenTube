using WalletService.Domain;

namespace WalletService.DataService
{
    public interface ITransactionDataService
    {
        IEnumerable<Transaction> GetTransactions(Guid playerId);
        void AddTransaction(Transaction transaction);
    }
}
