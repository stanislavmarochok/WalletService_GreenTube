namespace WalletService.Models
{
    public enum TransactionType
    {
        Deposit,
        Stake,
        Win
    }

    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
