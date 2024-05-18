using WalletService.DataService;
using WalletService.Models;

namespace WalletService.UseCases
{
    public class GetTransactions
    {
        private readonly ITransactionDataService _transactionDataService;

        public GetTransactions(ITransactionDataService transactionDataService)
        {
            _transactionDataService = transactionDataService;
        }

        public IEnumerable<Transaction> Execute(Guid playerId)
        {
            return _transactionDataService.GetTransactions(playerId);
        }
    }
}
