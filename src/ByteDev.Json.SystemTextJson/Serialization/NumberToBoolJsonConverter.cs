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

        public NumberToBoolJsonConverter() : this(0, 1)
        {
        }

        public NumberToBoolJsonConverter(int falseValue, int trueValue)
        {
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