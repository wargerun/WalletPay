namespace WalletPay.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Wallet Wallet { get; set; }
    }
}
