using System.Collections.Concurrent;
using WalletService.Domain;

namespace WalletService.Repositories
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private readonly ConcurrentDictionary<Guid, List<Transaction>> _transactions = new ConcurrentDictionary<Guid, List<Transaction>>();

        public IEnumerable<Transaction> GetTransactions(Guid playerId)
        {
            _transactions.TryGetValue(playerId, out var transactions);
            return transactions ?? new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.AddOrUpdate(
                transaction.PlayerId,
                new List<Transaction> { transaction },
                (key, existingList) => { existingList.Add(transaction); return existingList; }
            );
        }
    }
}
