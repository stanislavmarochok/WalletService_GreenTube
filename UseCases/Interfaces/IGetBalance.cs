namespace WalletService.UseCases.Interfaces
{
    public interface IGetBalance
    {
        decimal Execute(Guid playerId);
    }
}
