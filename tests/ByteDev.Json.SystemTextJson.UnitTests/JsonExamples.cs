namespace ByteDev.Json.SystemTextJson.UnitTests
{
    /// <summary>
    /// Examples of well formed JSON.
    /// </summary>
    internal static class JsonExamples
    {
        public const string JsonEmpty = "{}";

        public const string JsonNumber = "{\"age\":30}";
        public const string JsonString = "{\"name\":\"John\"}";
        public const string JsonObject = "{\"employee\":{\"name\":\"John\", \"age\":30, \"city\":\"New York\"}}";
        public const string JsonArray = "{\"employees\":[\"John\", \"Anna\", \"Peter\"]}";
        public const string JsonBoolFalse = "{\"sale\":false} ";
        public const string JsonBoolTrue = "{\"sale\":true} ";
        public const string JsonNull = "{\"middlename\":null} ";

        public const string JsonTwoProperties = "{ \"MyNumber\": 1, \"MyString\": \"John\" }";

        public const string JsonNumberZero = "{\"myBool\":0}";
        public const string JsonNumberOne = "{\"myBool\":1}";
        public const string JsonNumberTwo = "{\"myBool\":2}";
    }
}