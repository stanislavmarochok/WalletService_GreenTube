using WalletService.Domain;

namespace WalletService.Messages
{
    public class TransactionRequest
    {
        public Guid Id { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
    }
}
