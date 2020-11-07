using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;

using WalletPay.Data.Entities;

namespace WalletPay.WebService.Models.Dto
{
    public class AccountDto
    {
        [JsonPropertyName("account-id")]
        public int AccountId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("amount-in-format")]
        public string AmountInFormat => GetFormat();

        public AccountDto(Account account)
        {
            AccountId = account.Id;
            Name = account.Name;
            Currency = account.Currency;
            Amount = account.Amount;
        }

        private string GetFormat()
        {
            if (CurrencyTools.TryGetCurrencySymbol(Currency.ToUpperInvariant(), out string cultureName))
            {
                var culture = CultureInfo.CreateSpecificCulture(cultureName);
                return Amount.ToString("C4", culture);
            }

            return Amount.ToString(CultureInfo.InvariantCulture);
        }

        public static class CurrencyTools
        {
            private static readonly IDictionary<string, string> map;

            static CurrencyTools()
            {
                // TODO Данное решение не оптимальное
                map = CultureInfo
                    .GetCultures(CultureTypes.AllCultures)
                    .Where(c => !c.IsNeutralCulture)
                    .Select(culture => {
                        try
                        {
                            return new RegionInfo(culture.Name);
                        }
                        catch
                        {
                            return null;
                        }
                    })
                    .Where(ri => ri != null)
                    .GroupBy(ri => ri.ISOCurrencySymbol)
                    .ToDictionary(x => x.Key, x => x.First().Name);
            }

            public static bool TryGetCurrencySymbol(string isoCurrencySymbol, out string symbol) => map.TryGetValue(isoCurrencySymbol, out symbol);
        }
    }
}
