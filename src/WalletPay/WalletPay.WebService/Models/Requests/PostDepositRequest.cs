using System.ComponentModel.DataAnnotations;

namespace WalletPay.WebService.Models.Requests
{
    public class PostDepositRequest
    {
        public int UserId { get; set; }

        public int? AccountId { get; set; }

        [Required]
        public string CodeCurrency { get; set; }

        public decimal Amount { get; set; }
    }
}
