namespace WalletPay.Data.Entities
{
    public class Wallet
    {
        public int UserId { get; set; }

        public WalletStatus Status { get; set; }

        public virtual User User { get; set; }
    }
}
                        