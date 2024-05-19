namespace WalletService.Domain
{
    public class Player
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; } = 0;
    }
}
