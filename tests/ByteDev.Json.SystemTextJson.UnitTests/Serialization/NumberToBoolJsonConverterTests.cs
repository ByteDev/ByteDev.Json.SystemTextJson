using System.Text.Json;
using ByteDev.Json.SystemTextJson.Serialization;
using ByteDev.Json.SystemTextJson.UnitTests.TestEntities;
using NUnit.Framework;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    [TestFixture]
    public class NumberToBoolJsonConverterTests
    {
        private NumberToBoolJsonConverter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new NumberToBoolJsonConverter();
        }

        [TestFixture]
        public class Read : NumberToBoolJsonConverterTests
        {
            [Test]
            public void WhenJsonIsZero_ThenSetFalse()
            {
                var result = _sut.Deserialize<TestBoolEntity>(JsonExamples.JsonNumberZero);

                Assert.That(result.MyBool, Is.False);
            }

            [Test]
            public void WhenJsonIsOne_ThenSetTrue()
            {
                var result = _sut.Deserialize<TestBoolEntity>(JsonExamples.JsonNumberOne);

                Assert.That(result.MyBool, Is.True);
            }

            [Test]
            public void WhenJsonIsNotZeroOrOne_ThenThrowException()
            {
                var ex = Assert.Throws<JsonException>(() => _sut.Deserialize<TestBoolEntity>(JsonExamples.JsonNumberTwo));

                Assert.That(ex.Message, Is.EqualTo("JSON number value 2 could not be converted to bool. Expected 0 (false) or 1 (true)."));
            }
        }

        [TestFixture]
        public class Write : NumberToBoolJsonConverterTests
        {
            [Test]
            public void WhenSetToFalse_ThenJsonValueZero()
            {
                var entity = new TestBoolEntity { MyBool = false };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(JsonExamples.JsonNumberZero));
            }

            [Test]
            public void WhenSetToTrue_ThenJsonValueOne()
            {
                var entity = new TestBoolEntity { MyBool = true };

                var result = _sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(JsonExamples.JsonNumberOne));
            }
        }
    }
}