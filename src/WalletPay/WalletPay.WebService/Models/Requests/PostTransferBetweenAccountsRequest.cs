using System;
using System.ComponentModel.DataAnnotations;

using WalletPay.WebService.Validations;

namespace WalletPay.WebService.Models.Requests
{
    public class PostTransferBetweenAccountsRequest
    {
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Range(1, int.MaxValue)]
        public int TransferFromAccountId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int TransferToAccountId { get; set; }

        [PositiveAmountValidation]
        public decimal Amount { get; set; }
    }
}
