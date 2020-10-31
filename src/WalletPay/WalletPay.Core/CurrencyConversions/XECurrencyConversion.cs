using System.Net;
using System.Text.Json;

namespace WalletPay.Core.CurrencyConversions
{
    class XECurrencyConversion : ICurrencyConversion
    {
        /// <summary>
        /// https://www.xe.com/xecurrencydata/
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public decimal CurrencyConvert(string fromCurrency, string toCurrency, decimal amount)
        {    
            using var wc = new WebClient();
            string jsonStr = wc.DownloadString($"http://rate-exchange-1.appspot.com/currency?from={fromCurrency}&to={toCurrency}");
            XeJsonResponse response = JsonSerializer.Deserialize<XeJsonResponse>(jsonStr);
            return amount * response.Rate;
        }

        private class XeJsonResponse
        {
            public string To { get; set; }
            public decimal Rate { get; set; }
            public string From { get; set; }
        }
    }
}
