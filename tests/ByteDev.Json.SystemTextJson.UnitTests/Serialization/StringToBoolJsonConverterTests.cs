using System;
using System.Text.Json;
using ByteDev.Json.SystemTextJson.Serialization;
using ByteDev.Json.SystemTextJson.UnitTests.TestEntities;
using NUnit.Framework;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    [TestFixture]
    public class StringToBoolJsonConverterTests
    {
        private const string FalseValue = "N";
        private const string TrueValue = "Y";

        private readonly string _jsonStringFalse = JsonExamples.CreateJsonString("myBool", FalseValue);
        private readonly string _jsonStringTrue = JsonExamples.CreateJsonString("myBool", TrueValue);
        private readonly string _jsonStringInvalid = JsonExamples.CreateJsonString("myBool", "-");

        private StringToBoolJsonConverter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new StringToBoolJsonConverter(FalseValue, TrueValue);
        }

        [TestFixture]
        public class Constructor : StringToBoolJsonConverterTests
        {
            [Test]
            public void WhenFalseAndTrueValueAreEqual_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _ = new StringToBoolJsonConverter("0", "0"));
            }
        }

        [TestFixture]
        public class Read : StringToBoolJsonConverterTests
        {
            [Test]
            public void WhenJsonIsFalseValue_ThenSetFalse()
            {
                var result = _sut.Deserialize<TestBoolEntity>(_jsonStringFalse);

                Assert.That(result.MyBool, Is.False);
            }

            [Test]
            public void WhenJsonIsTrueValue_ThenSetTrue()
            {
                var result = _sut.Deserialize<TestBoolEntity>(_jsonStringTrue);

                Assert.That(result.MyBool, Is.True);
            }

            [Test]
            public void WhenJsonIsNotProvidedValue_ThenThrowException()
            {
                var ex = Assert.Throws<JsonException>(() => _sut.Deserialize<TestBoolEntity>(_jsonStringInvalid));

                Assert.That(ex.Message, Is.EqualTo($"JSON string value '-' could not be converted to bool. Expected '{FalseValue}' (false) or '{TrueValue}' (true)."));
            }
        }

        [TestFixture]
        public class Write : StringToBoolJsonConverterTests
        {
            [Test]
            public void WhenSetToFalse_ThenReturnJsonFalseValue()
            {
                var entity = new TestBoolEntity { MyBool = false };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(_jsonStringFalse));
            }

            [Test]
            public void WhenSetToTrue_ThenReturnJsonTrueValue()
            {
                var entity = new TestBoolEntity { MyBool = true };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(_jsonStringTrue));
            }
        }        
    }
}