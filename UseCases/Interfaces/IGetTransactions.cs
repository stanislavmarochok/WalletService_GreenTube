using WalletService.Domain;

namespace WalletService.UseCases.Interfaces
{
    public interface IGetTransactions
    {
        IAsyncEnumerable<Transaction> ExecuteAsync(Guid playerId);
    }
}
