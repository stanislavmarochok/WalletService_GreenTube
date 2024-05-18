using WalletService.DataService;

namespace WalletService.UseCases
{
    public class GetBalance
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
