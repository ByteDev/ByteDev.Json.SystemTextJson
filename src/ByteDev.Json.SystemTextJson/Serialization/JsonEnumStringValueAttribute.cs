using System;

namespace ByteDev.Json.SystemTextJson.Serialization
{
    /// <summary>
    /// Represents an alternative JSON string value for a enum value.
    /// </summary>
    public sealed class JsonEnumStringValueAttribute : Attribute
    {
        /// <summary>
        /// Value to use in the JSON instead of the enum name or number value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Json.SystemTextJson.Serialization.JsonEnumStringValueAttribute" /> class.
        /// </summary>
        /// <param name="value">Value to use in the JSON instead of the enum name or number value.</param>
        public JsonEnumStringValueAttribute(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Enum attribute value was null or empty.", nameof(value));

            Value = value;
        }
    }
}