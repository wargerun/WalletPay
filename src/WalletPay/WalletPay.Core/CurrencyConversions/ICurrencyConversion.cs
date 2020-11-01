namespace WalletPay.Core.CurrencyConversions
{
    public interface ICurrencyConversion
    {
        /// <summary>
        /// Метод, получения конвертированной суммы из одной валюты в другую
        /// </summary>
        /// <param name="fromCurrency">Валюта отправляющего счета</param>
        /// <param name="toCurrency">Валюта получающего счета</param>
        /// <param name="amount">Сумма перевода</param>
        /// <returns>результат конвертации</returns>
        decimal CurrencyConvert(string fromCurrency, string toCurrency, decimal amount);
    }
}
