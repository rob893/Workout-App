using System;
using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WorkoutApp.API.Helpers
{
    /// <summary>
    /// Used with System.Text.Json to convert string to date.
    /// </summary>
    public class StringToDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                if (Utf8Parser.TryParse(span, out DateTimeOffset date, out int bytesConsumed) && span.Length == bytesConsumed)
                {
                    return date;
                }

                if (DateTimeOffset.TryParse(reader.GetString(), out date))
                {
                    return date;
                }
            }

            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}