using WalletService.Messages;

namespace WalletService.UseCases.Interfaces
{
    public interface IProcessTransaction
    {
        Task<bool> ExecuteAsync(TransactionRequest transactionRequest);
    }
}
