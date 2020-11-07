using System.ComponentModel.DataAnnotations;
using WalletPay.WebService.Validations;

namespace WalletPay.WebService.Models.Requests
{
    public class PutDepositRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AccountId { get; set; }

        [PositiveAmountValidation]
        public decimal Amount { get; set; }
    }
}
