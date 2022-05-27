using System.Text.Json;
using ByteDev.Json.SystemTextJson.Serialization;
using ByteDev.Json.SystemTextJson.UnitTests.TestEntities;
using NUnit.Framework;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    [TestFixture]
    public class StringToNullableBoolJsonConverterTests
    {
        private const string FalseValue = "No";
        private const string TrueValue = "Yes";
        private const string NullValue = "Unknown";

        private readonly string _jsonStringFalse = JsonExamples.CreateJsonString("myBool", FalseValue);
        private readonly string _jsonStringTrue = JsonExamples.CreateJsonString("myBool", TrueValue);
        private readonly string _jsonStringNull = JsonExamples.CreateJsonString("myBool", NullValue);
        private readonly string _jsonStringInvalid = JsonExamples.CreateJsonString("myBool", "-");

        private StringToNullableBoolJsonConverter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new StringToNullableBoolJsonConverter(FalseValue, TrueValue, NullValue);
        }

        [TestFixture]
        public class Read : StringToNullableBoolJsonConverterTests
        {
            [Test]
            public void WhenJsonIsFalseValue_ThenSetFalse()
            {
                var result = _sut.Deserialize<TestNullableBoolEntity>(_jsonStringFalse);

                Assert.That(result.MyBool, Is.False);
            }

            [Test]
            public void WhenJsonIsTrueValue_ThenSetTrue()
            {
                var result = _sut.Deserialize<TestNullableBoolEntity>(_jsonStringTrue);

                Assert.That(result.MyBool, Is.True);
            }

            [Test]
            public void WhenJsonIsNullValue_ThenSetNull()
            {
                var result = _sut.Deserialize<TestNullableBoolEntity>(_jsonStringNull);

                Assert.That(result.MyBool, Is.Null);
            }

            [Test]
            public void WhenJsonIsNotProvidedValue_ThenThrowException()
            {
                var ex = Assert.Throws<JsonException>(() => _sut.Deserialize<TestNullableBoolEntity>(_jsonStringInvalid));

                Assert.That(ex.Message, Is.EqualTo($"JSON string value '-' could not be converted to nullable bool. Expected '{FalseValue}' (false) or '{TrueValue}' (true) or '{NullValue}' (null)."));
            }
        }

        [TestFixture]
        public class Write : StringToNullableBoolJsonConverterTests
        {
            [Test]
            public void WhenSetToFalse_ThenReturnJsonFalseValue()
            {
                var entity = new TestNullableBoolEntity { MyBool = false };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(_jsonStringFalse));
            }

            [Test]
            public void WhenSetToTrue_ThenReturnJsonTrueValue()
            {
                var entity = new TestNullableBoolEntity { MyBool = true };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(_jsonStringTrue));
            }

            [Test]
            public void WhenSetToNull_ThenReturnJsonNullValue()
            {
                var entity = new TestNullableBoolEntity { MyBool = null };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(_jsonStringNull));
            }
        }
    }
}