using System.Collections.Concurrent;
using WalletService.Domain;
using WalletService.Repositories.Interfaces;

namespace WalletService.Repositories
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private readonly ConcurrentDictionary<Guid, List<Transaction>> _transactions = new ConcurrentDictionary<Guid, List<Transaction>>();

        public IAsyncEnumerable<Transaction> GetTransactionsAsync(Guid playerId)
        {
            _transactions.TryGetValue(playerId, out var transactions);
            if (transactions != null)
            {
                return transactions.ToAsyncEnumerable();
            }

            return AsyncEnumerable.Empty<Transaction>();
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
