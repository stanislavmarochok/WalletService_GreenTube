using System.Collections.Concurrent;
using WalletService.Domain;
using WalletService.Repositories.Interfaces;

namespace WalletService.Repositories
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        private readonly ConcurrentDictionary<Guid, Player> _players = new ConcurrentDictionary<Guid, Player>();

        public Player? GetPlayer(Guid playerId)
        {
            _players.TryGetValue(playerId, out var player);
            return player;
        }

        public void AddPlayer(Player player)
        {
            if (!_players.TryAdd(player.Id, player))
            {
                throw new Exception("Player already exists.");
            }
        }
    }
}
