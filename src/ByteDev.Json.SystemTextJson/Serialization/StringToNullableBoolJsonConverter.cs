using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Converts a JSON string to a .NET Nullable Boolean and back again. This converter allows a JSON representation
    /// of a nullable boolean value to be one of three different strings.
    /// </summary>
    public class StringToNullableBoolJsonConverter : JsonConverter<bool?>
    {
        private readonly string _falseValue;
        private readonly string _trueValue;
        private readonly string _nullValue;

        public override bool HandleNull => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.StringToBoolJsonConverter" /> class.
        /// </summary>
        /// <param name="falseValue">Value to use for false.</param>
        /// <param name="trueValue">Value to use for true.</param>
        /// <param name="nullValue">Value to use for null.</param>
        public StringToNullableBoolJsonConverter(string falseValue, string trueValue, string nullValue)
        {
            _falseValue = falseValue;
            _trueValue = trueValue;
            _nullValue = nullValue;
        }

        public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonString = reader.GetString();

            if (jsonString == _nullValue)
                return null;

            if (jsonString == _trueValue)
                return true;

            if (jsonString == _falseValue)
                return false;

            throw new JsonException($"JSON string value '{jsonString}' could not be converted to nullable bool. Expected '{_falseValue}' (false) or '{_trueValue}' (true) or '{_nullValue}' (null).");
        }

        public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteStringValue(_nullValue);
            }
            else
            {
                writer.WriteStringValue(value.Value ? _trueValue : _falseValue);
            }
        }
    }
}