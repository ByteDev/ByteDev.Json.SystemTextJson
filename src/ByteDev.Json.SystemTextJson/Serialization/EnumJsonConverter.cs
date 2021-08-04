using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Converts a JSON number or string to a Enum and back again.
    /// </summary>
    public class EnumJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        private readonly TEnum? _defaultEnumValue;

        /// <summary>
        /// Indicates on write (serialize) how the enum value should be represented in the JSON.
        /// Default is by number value.
        /// </summary>
        public EnumValueType WriteEnumValueType { get; set; } = EnumValueType.Number;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.EnumJsonConverter{TEnum}" /> class.
        /// </summary>
        public EnumJsonConverter() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.EnumJsonConverter{TEnum}" /> class.
        /// </summary>
        /// <param name="defaultEnumValue">This value will be returned when either a JSON value is not a number or string or a JSON string cannot be successfully mapped to a enum value.</param>
        public EnumJsonConverter(TEnum? defaultEnumValue)
        {
            _defaultEnumValue = defaultEnumValue;
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                    return ReadNumber(reader);

                case JsonTokenType.String:
                    return ReadString(reader);

                default:
                    if (_defaultEnumValue.HasValue)
                        return _defaultEnumValue.Value;

                    throw new JsonException($"The JSON value could not be converted to Enum: '{typeof(TEnum)}'. Value was not a number or string.");
            }
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            if (WriteEnumValueType == EnumValueType.Name)
            {
                var attributes = GetAttributesForEnumValue<JsonEnumStringValueAttribute>(value);

                if (attributes.Length > 0)
                    writer.WriteStringValue(attributes.First().Value);
                else
                    writer.WriteStringValue(value.ToString());
            }
            else
            {
                writer.WriteNumberValue(value.GetEnumNumber());
            }
        }

        private static TEnum ReadNumber(Utf8JsonReader reader)
        {
            var jsonNumber = reader.GetInt64();

            return (TEnum)Enum.Parse(typeof(TEnum), jsonNumber.ToString(), false);
        }

        private TEnum ReadString(Utf8JsonReader reader)
        {
            var jsonString = reader.GetString();

            if (Enum.TryParse(jsonString, false, out TEnum result))
            {
                return result;
            }

            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                var attributes = GetAttributesForEnumValue<JsonEnumStringValueAttribute>(enumValue);

                if (attributes.Length > 0)
                {
                    if (attributes.First().Value == jsonString)
                    {
                        return enumValue;
                    }
                }
            }

            if (_defaultEnumValue.HasValue)
                return _defaultEnumValue.Value;

            throw new JsonException($"The JSON string value does match any value in Enum: '{typeof(TEnum)}'.");
        }

        private static TAttribute[] GetAttributesForEnumValue<TAttribute>(TEnum enumValue) where TAttribute : Attribute
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo == null)
                return new TAttribute[0];

            return (TAttribute[])fieldInfo.GetCustomAttributes(typeof(TAttribute), false);
        }
    }
}