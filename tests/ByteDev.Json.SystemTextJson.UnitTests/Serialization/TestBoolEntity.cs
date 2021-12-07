using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    public class TestBoolEntity
    {
        [JsonPropertyName("myBool")]
        public bool MyBool { get; set; }
    }
}