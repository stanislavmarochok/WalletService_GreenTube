using WalletService.Domain;

namespace WalletService.DataService
{
    public interface IPlayerDataService
    {
        Player GetPlayer(Guid playerId);
        void AddPlayer(Player player);
    }
}
