using System;
using System.Text.Json.Serialization;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    public class TestDateTimeEntity
    {
        [JsonPropertyName("datetime")]
        public DateTime MyDateTime { get; set; }
    }
}