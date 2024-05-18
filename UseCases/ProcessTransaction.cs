using WalletService.DataService;
using WalletService.Models;

namespace WalletService.UseCases
{
    public class ProcessTransaction
    {
        private readonly IPlayerDataService _playerDataService;
        private readonly ITransactionDataService _transactionDataService;

        public ProcessTransaction(IPlayerDataService playerDataService, ITransactionDataService transactionDataService)
        {
            _playerDataService = playerDataService;
            _transactionDataService = transactionDataService;
        }

        public bool Execute(TransactionDTO transactionDto)
        {
            var player = _playerDataService.GetPlayer(transactionDto.Id);
            if (player == null) throw new Exception("Player not found.");

            var existingTransactions = _transactionDataService.GetTransactions(player.Id);
            if (existingTransactions.Any(t => t.Id == transactionDto.Id))
            {
                return existingTransactions.First(t => t.Id == transactionDto.Id).Type == transactionDto.Type;
            }

            var newBalance = player.Balance + (transactionDto.Type == TransactionType.Stake ? -transactionDto.Amount : transactionDto.Amount);
            if (newBalance < 0) return false;

            var transaction = new Transaction
            {
                Id = transactionDto.Id,
                PlayerId = player.Id,
                Type = transactionDto.Type,
                Amount = transactionDto.Amount
            };

            player.Balance = newBalance;
            _transactionDataService.AddTransaction(transaction);
            return true;
        }
    }
}
