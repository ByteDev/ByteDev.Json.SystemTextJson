using System.Text.Json.Serialization;
using ByteDev.Json.SystemTextJson.Serialization;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    public enum LightSwitch
    { 
        On = 1,
        
        [JsonEnumStringValue("switchOff")]
        Off = 2,

        Faulty = 3
    }

    public class TestEnumEntity
    {
        [JsonPropertyName("lights")]
        public LightSwitch HouseLights { get; set; }
    }
}