using WalletService.Models;

namespace WalletService.Messages
{
    public class TransactionRequest
    {
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
