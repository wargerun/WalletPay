namespace WalletPay.WebService.Models
{
    internal static class Errors
    {
        private const string ERROR_MESSAGE_FORMAT = "The '{0}' is incorrect.";

        internal static readonly string InvalidUserId = string.Format(ERROR_MESSAGE_FORMAT, "User Id");
        internal static readonly string InvalidAmount = string.Format(ERROR_MESSAGE_FORMAT, "Amount");
        internal static readonly string InvalidAccountId = string.Format(ERROR_MESSAGE_FORMAT, "AccountId");
        internal static readonly string InvalidCodeCurrency = string.Format(ERROR_MESSAGE_FORMAT, "CodeCurrency");
        internal static readonly string InvalidAccountName = string.Format(ERROR_MESSAGE_FORMAT, "AccountName");
    }
}
