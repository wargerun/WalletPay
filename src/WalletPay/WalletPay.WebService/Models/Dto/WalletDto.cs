
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

        [JsonPropertyName("accounts")]
        public List<AccountDto> Accounts { get; }

        public WalletDto(Wallet wallet)
        {
            CurrenciesUpdated = wallet.AccountUpdated;
            Status = wallet.Status.ToString();

            if (wallet.Accounts != null)
            {
                Accounts = wallet.Accounts.Select(c => new AccountDto(c)).ToList();
            }
        }
    }
}
