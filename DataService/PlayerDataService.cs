using WalletService.Models;
using WalletService.Repositories;

namespace WalletService.DataService
{
    public class PlayerDataService : IPlayerDataService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerDataService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Player GetPlayer(Guid playerId)
        {
            return _playerRepository.GetPlayer(playerId);
        }

        public void AddPlayer(Player player)
        {
            _playerRepository.AddPlayer(player);
        }
    }
}
