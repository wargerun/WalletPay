
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using WalletPay.Data.Entities;

namespace WalletPay.WebService.Models.Dto
{
    public class WalletDto
    {
        [JsonPropertyName("currencies-updated")]
        public DateTime CurrenciesUpdated { get; }

        [JsonPropertyName("status")]
        public string Status { get; }

        [JsonPropertyName("currencies")]
        public List<CurrencyDto> Currencies { get; }

        public WalletDto(Wallet wallet)
        {
            CurrenciesUpdated = wallet.CurrenciesUpdated;
            Status = wallet.Status.ToString();

            if (wallet.Currencies != null)
            {
                Currencies = wallet.Currencies.Select(c => new CurrencyDto(c)).ToList();
            }
        }
    }
}
