using WalletService.Models;

namespace WalletService.Repositories
{
    public interface IPlayerRepository
    {
        Player GetPlayer(Guid playerId);
        void AddPlayer(Player player);
    }
}
