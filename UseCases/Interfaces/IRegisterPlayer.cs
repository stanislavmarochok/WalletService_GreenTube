using WalletService.Messages;

namespace WalletService.UseCases.Interfaces
{
    public interface IRegisterPlayer
    {
        void Execute(RegisterPlayerRequest request);
    }
}
