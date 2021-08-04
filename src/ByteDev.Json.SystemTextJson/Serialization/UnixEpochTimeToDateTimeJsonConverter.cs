using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Converts a Unix epoch time JSON number to a DateTime and back again.
    /// </summary>
    public class UnixEpochTimeToDateTimeJsonConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// Indicates the precision of the Unix epoch time (seconds or milliseconds).
        /// Default is seconds.
        /// </summary>
        public UnixEpochTimePrecision Precision { get; set; } = UnixEpochTimePrecision.Seconds;

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonNumber = reader.GetInt64();

            if (Precision == UnixEpochTimePrecision.Milliseconds)
            {
                return DateTimeOffset
                    .FromUnixTimeMilliseconds(jsonNumber)
                    .UtcDateTime;
            }

            return DateTimeOffset
                .FromUnixTimeSeconds(jsonNumber)
                .UtcDateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var unixTime = Precision == UnixEpochTimePrecision.Milliseconds ? 
                ((DateTimeOffset)value).ToUnixTimeMilliseconds() : 
                ((DateTimeOffset)value).ToUnixTimeSeconds();

            writer.WriteNumberValue(unixTime);
        }
    }
}