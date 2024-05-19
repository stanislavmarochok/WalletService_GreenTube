using System.Transactions;

namespace WalletService.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatusEnum Status { get; set; }
    }
}
