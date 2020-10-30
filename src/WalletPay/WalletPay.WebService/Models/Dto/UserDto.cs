using System.Text.Json.Serialization;

using WalletPay.Data.Entities;

namespace WalletPay.WebService.Models.Dto
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; }

        [JsonPropertyName("name")]
        public string Name { get; }

        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name;
        }
    }
}
