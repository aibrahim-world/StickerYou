using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Customers.Application.Utilities
{
    public class DescriptionEnumConverter<T> : JsonConverter<T> where T : Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var description = reader.GetString();
            foreach (var field in typeToConvert.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new JsonException($"Unable to convert \"{description}\" to Enum \"{typeToConvert}\".");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var field = value.GetType().GetField(value.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                writer.WriteStringValue(attribute.Description);
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
