using System;

namespace ByteDev.Json.SystemTextJson
{
    internal static class GenericExtensions
    {
        public static long GetEnumNumber<T>(this T source)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Generic param should be of type enum.", nameof(source));

            Enum enumValue = Enum.Parse(typeof(T), source.ToString()) as Enum;

            return Convert.ToInt64(enumValue);
        }
    }
}