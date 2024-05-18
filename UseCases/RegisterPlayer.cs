using WalletService.DataService;
using WalletService.Models;

namespace WalletService.UseCases
{
    public class RegisterPlayer
    {
        private readonly IPlayerDataService _playerDataService;

        public RegisterPlayer(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }

        public void Execute(Guid playerId)
        {
            var player = new Player { Id = playerId, Balance = 0 };
            _playerDataService.AddPlayer(player);
        }
    }
}
