namespace WalletPay.Data.Entities
{
    public class Currency
    {
        public int WalletId { get; set; }

        public string Code { get; set; }

        public decimal Amount { get; set; }  

        public virtual Wallet Wallet { get; set; }
    }
}
