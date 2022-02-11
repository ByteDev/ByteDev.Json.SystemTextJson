using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Converts a string to a System.Guid and back again.
    /// Write to JSON string format can be configured using the provided constructor parameter.
    /// Read from JSON is very tolerant of different Guid formats.
    /// </summary>
    public class StringToGuidJsonConverter : JsonConverter<Guid>
    {
        private const string DefaultWriteFormat = "D"; // .NET default format on Guid.ToString()

        private readonly string _writeFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.StringToGuidJsonConverter" /> class
        /// using the default .NET write format "D" ("c62a82b3-5e0d-47bf-9a12-b027ff56d3e3").
        /// </summary>
        public StringToGuidJsonConverter() : this(DefaultWriteFormat)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.StringToGuidJsonConverter" /> class
        /// using the provided .NET write format.
        /// </summary>
        /// <param name="writeFormat">Write to JSON string format. Must be one of values:<br/>
        /// "D": c62a82b3-5e0d-47bf-9a12-b027ff56d3e3<br/>
        /// "N": c62a82b35e0d47bf9a12b027ff56d3e3<br/>
        /// "B": {c62a82b3-5e0d-47bf-9a12-b027ff56d3e3}<br/>
        /// "P": (c62a82b3-5e0d-47bf-9a12-b027ff56d3e3)
        /// </param>
        public StringToGuidJsonConverter(string writeFormat)
        {
            if (writeFormat != "D" &&
                writeFormat != "N" &&
                writeFormat != "B" &&
                writeFormat != "P")
                throw new ArgumentException("Invalid write format. Only the .NET defined formats: \"D\", \"N\", \"B\", \"P\" are valid.", nameof(writeFormat));

            _writeFormat = writeFormat;
        }

        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonString = reader.GetString();

            if (jsonString == null)
                throw new JsonException("The JSON null value could not be converted to System.Guid.");

            try
            {
                jsonString = jsonString
                    .RemoveStartsWith("{")
                    .RemoveEndsWith("}")
                    .RemoveStartsWith("(")
                    .RemoveEndsWith(")");

                return Guid.Parse(jsonString);
            }
            catch (Exception ex)
            {
                throw new JsonException($"The JSON value: '{jsonString}', could not be converted to System.Guid.", ex);
            }
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_writeFormat));
        }
    }
}