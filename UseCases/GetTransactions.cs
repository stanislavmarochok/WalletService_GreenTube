using WalletService.DataService;
using WalletService.Domain;
using WalletService.UseCases.Interfaces;

namespace WalletService.UseCases
{
    public class GetTransactions : IGetTransactions
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
