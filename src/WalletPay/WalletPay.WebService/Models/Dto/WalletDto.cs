
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

        [JsonPropertyName("wallet-id")]
        public int WalletId { get; }

        [JsonPropertyName("status")]
        public string Status { get; }

        [JsonPropertyName("accounts")]
        public List<AccountDto> Accounts { get; }

        public WalletDto(Wallet wallet, List<Account> accounts)
        {
            WalletId = wallet.Id;
            CurrenciesUpdated = wallet.AccountUpdated;
            Status = wallet.Status.ToString();

            if (accounts != null)
            {
                Accounts = accounts.Select(c => new AccountDto(c)).ToList();
            }
        }
    }
}
