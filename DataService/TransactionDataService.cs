using WalletService.DataService.Interfaces;
using WalletService.Domain;
using WalletService.Repositories.Interfaces;

namespace WalletService.DataService
{
    public class TransactionDataService : ITransactionDataService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionDataService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public IAsyncEnumerable<Transaction> GetTransactionsAsync(Guid playerId)
        {
            return _transactionRepository.GetTransactionsAsync(playerId);
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactionRepository.AddTransaction(transaction);
        }
    }
}
