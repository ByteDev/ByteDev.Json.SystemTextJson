using System.Text.Json;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.UnitTests
{
    public static class JsonConverterTestExtensions
    {
        public static string Serialize(this JsonConverter sut, object obj)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(sut);

            return JsonSerializer.Serialize(obj, options);
        }

        public static T Deserialize<T>(this JsonConverter sut, string json)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(sut);

            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}