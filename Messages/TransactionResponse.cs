using WalletService.Domain;

namespace WalletService.Messages
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
        public string Info => "This is DTO object.";
    }
}
