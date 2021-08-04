using System.Text.Json;
using ByteDev.Json.SystemTextJson.Serialization;
using NUnit.Framework;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    [TestFixture]
    public class EnumJsonConverterTests
    {
        private const string JsonNotNumberOrStringValue = "{\"lights\":true}";
        private const string JsonNumber = "{\"lights\":2}";
        private const string JsonNumberNoMatch = "{\"lights\":99}";
        private const string JsonString = "{\"lights\":\"Off\"}";
        private const string JsonStringNoAttribute = "{\"lights\":\"Faulty\"}";
        private const string JsonStringAttribute = "{\"lights\":\"switchOff\"}";
        private const string JsonStringNoMatch = "{\"lights\":\"InvalidValue\"}";

        private EnumJsonConverter<LightSwitch> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EnumJsonConverter<LightSwitch>();
        }

        [TestFixture]
        public class Read : EnumJsonConverterTests
        {
            [Test]
            public void WhenJsonEnumIsNumber_ThenSetEnum()
            {
                var result = _sut.Deserialize<TestEnumEntity>(JsonNumber);

                Assert.That(result.HouseLights, Is.EqualTo(LightSwitch.Off));
            }

            [Test]
            public void WhenJsonEnumIsString_AndMatches_ThenSetEnum()
            {
                var result = _sut.Deserialize<TestEnumEntity>(JsonString);

                Assert.That(result.HouseLights, Is.EqualTo(LightSwitch.Off));
            }

            [Test]
            public void WhenJsonEnumIsString_AndMatchesAttribute_ThenSetEnum()
            {
                var result = _sut.Deserialize<TestEnumEntity>(JsonStringAttribute);

                Assert.That(result.HouseLights, Is.EqualTo(LightSwitch.Off));
            }

            [Test]
            public void WhenJsonEnumIsNumber_AndDoesNotMatch_ThenSetEnum()
            {
                var result = _sut.Deserialize<TestEnumEntity>(JsonNumberNoMatch);

                Assert.That((int)result.HouseLights, Is.EqualTo(99));
            }

            [Test]
            public void WhenJsonEnumIsString_AndDoesNotMatch_ThenThrowException()
            {
                var ex = Assert.Throws<JsonException>(() => _ = _sut.Deserialize<TestEnumEntity>(JsonStringNoMatch));
                Assert.That(ex.Message, Is.EqualTo("The JSON string value does match any value in Enum: 'ByteDev.Json.SystemTextJson.UnitTests.Serialization.LightSwitch'."));
            }

            [Test]
            public void WhenJsonEnumIsNotNumberOrString_ThenThrowException()
            {
                var ex = Assert.Throws<JsonException>(() => _ = _sut.Deserialize<TestEnumEntity>(JsonNotNumberOrStringValue));
                Assert.That(ex.Message, Is.EqualTo("The JSON value could not be converted to Enum: 'ByteDev.Json.SystemTextJson.UnitTests.Serialization.LightSwitch'. Value was not a number or string."));
            }
        }

        [TestFixture]
        public class Write : EnumJsonConverterTests
        {
            [Test]
            public void WhenWriteEnumNameIsFalse_ThenWriteEnumNumber()
            {
                var entity = new TestEnumEntity
                {
                    HouseLights = LightSwitch.Off
                };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(JsonNumber));
            }

            [Test]
            public void WhenWriteEnumNameIsFalse_AndEnumValueIsNotDefined_ThenWriteEnumNumber()
            {
                var entity = new TestEnumEntity
                {
                    HouseLights = (LightSwitch)99
                };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo("{\"lights\":99}"));
            }

            [Test]
            public void WhenWriteName_ThenWriteEnumString()
            {
                var entity = new TestEnumEntity
                {
                    HouseLights = LightSwitch.Faulty
                };

                _sut.WriteEnumValueType = EnumValueType.Name;

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(JsonStringNoAttribute));
            }

            [Test]
            public void WhenWriteName_AndHasAttribute_ThenWriteAttributeString()
            {
                var entity = new TestEnumEntity
                {
                    HouseLights = LightSwitch.Off
                };

                _sut.WriteEnumValueType = EnumValueType.Name;

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(JsonStringAttribute));
            }

            [Test]
            public void WhenWriteName_AndEnumValueIsNotDefined_ThenWriteEnumNumberAsString()
            {
                var entity = new TestEnumEntity
                {
                    HouseLights = (LightSwitch)99
                };

                _sut.WriteEnumValueType = EnumValueType.Name;

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo("{\"lights\":\"99\"}"));
            }
        }
    }
}