using WalletService.DataService.Interfaces;
using WalletService.Domain;
using WalletService.Messages;
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

        public void Execute(RegisterPlayerRequest request)
        {
            var player = new Player { Id = request.playerId, Balance = 0 };
            _playerDataService.AddPlayer(player);
        }
    }
}
