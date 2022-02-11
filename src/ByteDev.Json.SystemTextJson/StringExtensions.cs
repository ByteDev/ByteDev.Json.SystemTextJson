using System;
using System.Text.Json;

namespace ByteDev.Json.SystemTextJson
{
    /// <summary>
    /// Extension methods for <see cref="T:System.String" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates if a string is well-formed JSON.
        /// </summary>
        /// <param name="source">The string to perform the operation on.</param>
        /// <returns>True if the string is well-formed JSON; otherwise false.</returns>
        public static bool IsJson(this string source)
        {
            if (source == null)
                return false;

            try
            {
                JsonDocument.Parse(source);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        internal static string RemoveStartsWith(this string source, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            
            if (string.IsNullOrEmpty(source))
                return source;

            if (value == string.Empty)
                return source;

            if (source.StartsWith(value))
                return source.Substring(value.Length);

            return source;
        }

        internal static string RemoveEndsWith(this string source, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value == string.Empty)
                return source;

            if (string.IsNullOrEmpty(source))
                return source;

            if (source.EndsWith(value))
                return source.Substring(0, source.Length - value.Length);

            return source;
        }
    }
}