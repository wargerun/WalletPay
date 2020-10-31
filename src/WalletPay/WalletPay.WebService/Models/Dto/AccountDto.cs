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

        public AccountDto(Account account)
        {
            AccountId = account.Id;
            Name = account.Name;
            Currency = account.Currency;
            Amount = account.Amount;
        }
    }
}
