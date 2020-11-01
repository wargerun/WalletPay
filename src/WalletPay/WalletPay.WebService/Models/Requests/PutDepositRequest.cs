using WalletPay.WebService.Validations;

namespace WalletPay.WebService.Models.Requests
{
    public class PutDepositRequest
    {
        public int UserId { get; set; }

        public int? AccountId { get; set; }
        public string AccountName { get; set; }

        public string CodeCurrency { get; set; }

        [PositiveAmountValidation]
        public decimal Amount { get; set; }
    }
}
