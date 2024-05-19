using WalletService.Domain;

namespace WalletService.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Player? GetPlayer(Guid playerId);
        void AddPlayer(Player player);
    }
}
