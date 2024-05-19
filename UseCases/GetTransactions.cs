using WalletService.DataService;
using WalletService.Messages;
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

        public async IAsyncEnumerable<TransactionResponse> ExecuteAsync(Guid playerId)
        {
            await foreach (var transaction in _transactionDataService.GetTransactionsAsync(playerId))
            {
                yield return new TransactionResponse
                {
                    Id = transaction.Id,
                    PlayerId = playerId,
                    Amount = transaction.Amount,
                    Type = transaction.Type
                };
            }
        }
    }
}
