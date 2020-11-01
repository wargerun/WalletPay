using System.ComponentModel.DataAnnotations;

namespace WalletPay.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public int WalletId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Currency { get; set; }

        public decimal Amount { get; set; }  

        public virtual Wallet Wallet { get; set; }
    }
}
