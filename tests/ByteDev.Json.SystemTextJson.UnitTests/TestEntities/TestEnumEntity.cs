using System.Text.Json.Serialization;
using ByteDev.Json.SystemTextJson.Serialization;

namespace ByteDev.Json.SystemTextJson.UnitTests.TestEntities
{
    public enum LightSwitch
    { 
        On = 1,
        
        [JsonEnumStringValue("switchOff")]
        Off = 2,

        Faulty = 3
    }

    public enum Color
    {
        Unknown = 0,
        Red = 1,
        Green = 2,
        Blue = 3
    }

    public class TestEnumEntity
    {
        [JsonPropertyName("lights")]
        public LightSwitch HouseLights { get; set; }
    }

    public class TestTwoEnumEntity
    {
        [JsonPropertyName("lights")]
        public LightSwitch HouseLights { get; set; }

        [JsonPropertyName("room_color")]
        public Color RoomColor { get; set; }
    }
}