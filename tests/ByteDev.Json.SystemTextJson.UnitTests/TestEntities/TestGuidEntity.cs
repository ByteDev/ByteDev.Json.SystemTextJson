using System;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.UnitTests.TestEntities
{
    public class TestGuidEntity
    {
        [JsonPropertyName("myGuid")]
        public Guid MyGuid { get; set; }
    }
}