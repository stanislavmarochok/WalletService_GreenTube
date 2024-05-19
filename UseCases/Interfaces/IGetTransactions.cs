using WalletService.Messages;

namespace WalletService.UseCases.Interfaces
{
    public interface IGetTransactions
    {
        IAsyncEnumerable<TransactionResponse> ExecuteAsync(Guid playerId);
    }
}
