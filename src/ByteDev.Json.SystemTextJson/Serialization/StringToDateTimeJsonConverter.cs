using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Converts a string to a DateTime and back again using a provided custom
    /// date time format.
    /// </summary>
    public class StringToDateTimeJsonConverter : JsonConverter<DateTime>
    {
        public readonly string _dateTimeFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.StringToDateTimeJsonConverter" /> class.
        /// </summary>
        /// <param name="dateTimeFormat">Date time format.</param>
        public StringToDateTimeJsonConverter(string dateTimeFormat)
        {
            if (string.IsNullOrEmpty(dateTimeFormat))
                throw new ArgumentException("DateTime format was null or empty");

            _dateTimeFormat = dateTimeFormat;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonString = reader.GetString();

            if (jsonString == null)
                throw new JsonException("The JSON null value could not be converted to System.DateTime.");

            try
            {
                return DateTime.ParseExact(jsonString, _dateTimeFormat, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new JsonException($"The JSON value: '{jsonString}', could not be converted to System.DateTime.", ex);
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateTimeFormat, CultureInfo.InvariantCulture));
        }
    }
}