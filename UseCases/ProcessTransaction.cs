using WalletService.DataService;
using WalletService.Messages;
using WalletService.Domain;
using WalletService.UseCases.Interfaces;
using System.Transactions;
using Transaction = WalletService.Domain.Transaction;

namespace WalletService.UseCases
{
    public class ProcessTransaction : IProcessTransaction
    {
        private readonly IPlayerDataService _playerDataService;
        private readonly ITransactionDataService _transactionDataService;

        public ProcessTransaction(IPlayerDataService playerDataService, ITransactionDataService transactionDataService)
        {
            _playerDataService = playerDataService;
            _transactionDataService = transactionDataService;
        }

        public bool Execute(TransactionRequest transactionRequest)
        {
            var player = _playerDataService.GetPlayer(transactionRequest.Id);
            if (player == null) throw new Exception("Player not found.");

            var existingTransactions = _transactionDataService.GetTransactions(player.Id).ToList();
            var existingTransaction = existingTransactions.FirstOrDefault(t => t.Id == transactionRequest.Id);

            if (existingTransaction != null)
            {
                return existingTransaction.Status == TransactionStatusEnum.Accepted;
            }

            var newBalance = player.Balance + (transactionRequest.Type == TransactionTypeEnum.Stake ? -transactionRequest.Amount : transactionRequest.Amount);
            if (newBalance < 0)
            {
                var rejectedTransaction = new Transaction
                {
                    Id = transactionRequest.Id,
                    PlayerId = player.Id,
                    Type = transactionRequest.Type,
                    Amount = transactionRequest.Amount,
                    Status = TransactionStatusEnum.Rejected
                };
                _transactionDataService.AddTransaction(rejectedTransaction);
                return false;
            }

            var transaction = new Transaction
            {
                Id = transactionRequest.Id,
                PlayerId = player.Id,
                Type = transactionRequest.Type,
                Amount = transactionRequest.Amount,
                Status = TransactionStatusEnum.Accepted
            };

            player.Balance = newBalance;
            _transactionDataService.AddTransaction(transaction);
            return true;
        }
    }
}
