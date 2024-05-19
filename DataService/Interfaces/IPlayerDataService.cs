using WalletService.Domain;

namespace WalletService.DataService.Interfaces
{
    public interface IPlayerDataService
    {
        Player? GetPlayer(Guid playerId);
        void AddPlayer(Player player);
    }
}
