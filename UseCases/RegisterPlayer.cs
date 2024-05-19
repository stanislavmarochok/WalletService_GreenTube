using WalletService.DataService;
using WalletService.Domain;
using WalletService.UseCases.Interfaces;

namespace WalletService.UseCases
{
    public class RegisterPlayer : IRegisterPlayer
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
