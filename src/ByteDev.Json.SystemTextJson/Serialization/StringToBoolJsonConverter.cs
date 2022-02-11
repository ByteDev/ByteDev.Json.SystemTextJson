using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Converts a JSON string to a .NET Boolean and back again. By default JSON boolean values are
    /// represented as either false or true. This converter allows a JSON representation of a boolean value
    /// to be one of two different strings.
    /// </summary>
    public class StringToBoolJsonConverter : JsonConverter<bool>
    {
        private readonly string _falseValue;
        private readonly string _trueValue;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.StringToBoolJsonConverter" /> class.
        /// </summary>
        /// <param name="falseValue">Value to use for false.</param>
        /// <param name="trueValue">Value to use for true.</param>
        public StringToBoolJsonConverter(string falseValue, string trueValue)
        {
            if (falseValue == trueValue)
                throw new ArgumentException("True value cannot be equal to false value.", nameof(trueValue));

            _falseValue = falseValue;
            _trueValue = trueValue;
        }

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonString = reader.GetString();

            if (jsonString == _trueValue)
                return true;

            if (jsonString == _falseValue)
                return false;

            throw new JsonException($"JSON string value '{jsonString}' could not be converted to bool. Expected '{_falseValue}' (false) or '{_trueValue}' (true).");
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value ? _trueValue : _falseValue);
        }
    }
}