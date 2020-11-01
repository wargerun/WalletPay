using System.ComponentModel.DataAnnotations;

namespace WalletPay.Data.Entities
{
    public class Account
    {
        private string currency;

        public int Id { get; set; }

        public int WalletId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Currency
        {
            get => currency;
            set
            {
                if (value != null)
                {
                    currency = value.ToUpperInvariant();
                }
            }
        }

        public decimal Amount { get; set; }

        public virtual Wallet Wallet { get; set; }
    }
}
