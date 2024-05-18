using WalletService.Models;
using WalletService.Repositories;

namespace WalletService.DataService
{
    public class TransactionDataService : ITransactionDataService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionDataService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public IEnumerable<Transaction> GetTransactions(Guid playerId)
        {
            return _transactionRepository.GetTransactions(playerId);
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactionRepository.AddTransaction(transaction);
        }
    }
}
