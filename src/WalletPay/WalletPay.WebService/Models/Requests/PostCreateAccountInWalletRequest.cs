using System.ComponentModel.DataAnnotations;
using WalletPay.WebService.Validations;

namespace WalletPay.WebService.Models.Requests
{
    public class PostCreateAccountInWalletRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int WalletId { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public string CodeCurrency { get; set; }

        [PositiveAmountValidation]
        [Required]
        public decimal Amount { get; set; }
    }
}
