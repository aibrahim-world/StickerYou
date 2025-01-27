using System.Text.Json.Serialization;
using System.Text.Json;

namespace Customers.Application.Utilities
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private static readonly string[] Formats = { "M/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "MM-dd-yyyy", "M-dd-yyyy", "M-d-yyyy" };

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.ParseExact(value, Formats);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Formats[0]));
        }
    }
}
