using System.Text.Json.Serialization;

using WalletPay.Data.Entities;

namespace WalletPay.WebService.Models.Dto
{
    public class CurrencyDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        public CurrencyDto(Currency currency)
        {
            Code = currency.Code;
            Amount = currency.Amount;
        }
    }
}
