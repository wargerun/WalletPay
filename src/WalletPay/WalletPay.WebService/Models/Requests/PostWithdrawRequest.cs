using WalletPay.WebService.Validations;

namespace WalletPay.WebService.Models.Requests
{
    public class PostWithdrawRequest
    {
        public int UserId { get; set; }

        public int AccountId { get; set; }

        [PositiveAmountValidation]
        public decimal Amount { get; set; }
    }
}
