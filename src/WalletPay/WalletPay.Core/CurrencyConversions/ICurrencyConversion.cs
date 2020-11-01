namespace WalletPay.Core.CurrencyConversions
{
    public interface ICurrencyConversion
    {
        /// <summary>
        /// Метод, получения конвертированной суммы из одной валюты в другую
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        decimal CurrencyConvert(string fromCurrency, string toCurrency, decimal amount);
    }
}
