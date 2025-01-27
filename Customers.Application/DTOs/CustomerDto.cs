using Customers.Application.Utilities;
using Customers.Domain.Enums;
using System.Text.Json.Serialization;

namespace Customers.Application.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }

        [JsonPropertyName("House Number")]
        public int HouseNumber { get; set; }

        [JsonPropertyName("Address Line 1")]
        public string AddressLine1 { get; set; }

        [JsonConverter(typeof(DescriptionEnumConverter<State>))]
        public State State { get; set; }

        [JsonConverter(typeof(DescriptionEnumConverter<Country>))]
        public Country Country { get; set; }

        [JsonConverter(typeof(DescriptionEnumConverter<Category>))]
        public Category CategoryId { get; set; }

        [JsonPropertyName("Date Of Birth")]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }
    }

}
