namespace WalletPay.Core.CurrencyConversions
{
    public interface ICurrencyConversion
    {
        decimal CurrencyConvert(string fromCurrency, string toCurrency, decimal amount);
    }
}
