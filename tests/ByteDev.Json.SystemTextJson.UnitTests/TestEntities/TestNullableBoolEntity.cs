using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.UnitTests.TestEntities
{
    public class TestNullableBoolEntity
    {
        [JsonPropertyName("myBool")]
        public bool? MyBool { get; set; }
    }
}