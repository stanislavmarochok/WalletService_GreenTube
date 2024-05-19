using WalletService.DataService.Interfaces;
using WalletService.UseCases.Interfaces;

namespace WalletService.UseCases
{
    public class GetBalance : IGetBalance
    {
        private readonly IPlayerDataService _playerDataService;

        public GetBalance(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }

        public decimal Execute(Guid playerId)
        {
            var player = _playerDataService.GetPlayer(playerId);
            if (player == null) throw new Exception("Player not found.");
            return player.Balance;
        }
    }
}
