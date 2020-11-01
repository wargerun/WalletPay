using System;

using WalletPay.Core.CurrencyConversions;

namespace WalletPay.Core.Tests
{
    public class MockCurrencyConversion : ICurrencyConversion
    {
        public decimal CurrencyConvert(string fromCurrency, string toCurrency, decimal amount)
        {
            decimal rate = getRate(fromCurrency, toCurrency);
            return amount * rate;
        }

        private static decimal getRate(string fromCurrency, string toCurrency)
        {
            return fromCurrency switch
            {
                "RUB" when toCurrency == "EUR" => 92.7646m,
                "EUR" when toCurrency == "RUB" => 0.0108041m,

                _ => throw new NotImplementedException($"FromCurrency={fromCurrency}, ToCurrency={toCurrency}"),
            };
        }
    }
}
