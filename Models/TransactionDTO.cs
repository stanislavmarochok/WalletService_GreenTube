namespace WalletService.Models
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
