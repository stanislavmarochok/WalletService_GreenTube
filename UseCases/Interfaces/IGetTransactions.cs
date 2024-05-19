using WalletService.Domain;

namespace WalletService.UseCases.Interfaces
{
    public interface IGetTransactions
    {
        IEnumerable<Transaction> Execute(Guid playerId);
    }
}
