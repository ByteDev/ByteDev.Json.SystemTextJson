using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Converts a JSON number (integer) to a .NET Boolean and back again. By default JSON boolean values are
    /// represented as either false or true. This converter allows a JSON representation of a boolean value
    /// to be one of two different integers.
    /// </summary>
    public class NumberToBoolJsonConverter : JsonConverter<bool>
    {
        private readonly int _falseValue;
        private readonly int _trueValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.NumberToBoolJsonConverter" /> class
        /// using 0 as the false value and 1 as the true value.
        /// </summary>
        public NumberToBoolJsonConverter() : this(0, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.NumberToBoolJsonConverter" /> class
        /// </summary>
        /// <param name="falseValue">Value to use for false.</param>
        /// <param name="trueValue">Value to use for true.</param>
        public NumberToBoolJsonConverter(int falseValue, int trueValue)
        {
            if (falseValue == trueValue)
                throw new ArgumentException("True value cannot be equal to false value.", nameof(trueValue));

            _falseValue = falseValue;
            _trueValue = trueValue;
        }

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonInt = reader.GetInt32();

            if (jsonInt == _trueValue)
                return true;

            if (jsonInt == _falseValue)
                return false;

            throw new JsonException($"JSON number value {jsonInt} could not be converted to bool. Expected {_falseValue} (false) or {_trueValue} (true).");
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value ? _trueValue : _falseValue);
        }
    }
}