using WalletService.Domain;
using WalletService.Messages;

namespace WalletService.UseCases.Interfaces
{
    public interface IProcessTransaction
    {
        bool Execute(TransactionRequest transactionDto);
    }
}
