using Newtonsoft.Json;

namespace TravelbilityApp.WebAPI.JsonConverters
{
    public class TimeOnlyNewtonsoftJsonConverter : JsonConverter<TimeOnly?>
    {
        private const string TimeFormat = "HH:mm";

        public override void WriteJson(JsonWriter writer, TimeOnly? value, JsonSerializer serializer)
            => writer.WriteValue(value?.ToString(TimeFormat));

        public override TimeOnly? ReadJson(JsonReader reader, Type objectType, TimeOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // Handle nulls
            if (reader.TokenType == JsonToken.Null)
            {
                return default;
            }

            // Expecting a string
            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException($"Unexpected token parsing TimeOnly. Expected String, got {reader.TokenType}.");
            }

            var timeString = (string)reader.Value!;

            // Try parsing with the exact format
            if (TimeOnly.TryParseExact(timeString, TimeFormat, null, System.Globalization.DateTimeStyles.None, out var parsed))
            {
                return parsed;
            }

            // Fallback to general parse
            if (TimeOnly.TryParse(timeString, out parsed))
            {
                return parsed;
            }

            throw new JsonSerializationException($"Invalid time format for TimeOnly: '{timeString}'.");
        }

        public override bool CanRead => true;

        public override bool CanWrite => true;
    }
}