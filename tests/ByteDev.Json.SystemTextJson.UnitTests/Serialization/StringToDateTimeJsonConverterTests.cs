using System;
using System.Text.Json;
using ByteDev.Json.SystemTextJson.Serialization;
using NUnit.Framework;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    [TestFixture]
    public class StringToDateTimeJsonConverterTests
    {
        private const string Json = "{\"datetime\":\"2022-01-12 09:59:20\"}";
        private const string JsonNull = "{\"datetime\":null}";
        private const string JsonEmptyString = "{\"datetime\":\"\"}";
        private const string JsonInvalidString = "{\"datetime\":\"not a date\"}";
        private const string JsonNumber = "{\"datetime\":0}";
        private const string JsonFalse = "{\"datetime\":false}";
        private const string JsonTrue = "{\"datetime\":true}";

        [TestFixture]
        public class Constructor : StringToDateTimeJsonConverterTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenDateTimeFormatIsNullOrEmpty_ThenThrowException(string dateTimeFormat)
            {
                Assert.Throws<ArgumentException>(() => _ = new StringToDateTimeJsonConverter(dateTimeFormat));
            }
        }

        [TestFixture]
        public class Read : StringToDateTimeJsonConverterTests
        {
            [Test]
            public void WhenJsonDateTimeProvided_AndJsonValueIsSameFormat_ThenSetDateTime()
            {
                var sut = new StringToDateTimeJsonConverter("yyyy-MM-dd HH:mm:ss");

                var result = sut.Deserialize<TestDateTimeEntity>(Json);

                Assert.That(result.MyDateTime, Is.EqualTo(new DateTime(2022, 1, 12, 9, 59, 20)));
            }

            [Test]
            public void WhenJsonDateTimeIsNull_ThenThrowException()
            {
                var sut = new StringToDateTimeJsonConverter("yyyy-MM-dd HH:mm:ss");

                var ex = Assert.Throws<JsonException>(() => _ = sut.Deserialize<TestDateTimeEntity>(JsonNull));
                Assert.That(ex.Message, Is.EqualTo("The JSON null value could not be converted to System.DateTime."));
            }

            [TestCase(JsonEmptyString, "")]
            [TestCase(JsonInvalidString, "not a date")]
            public void WhenJsonDateTimeIsInvalidString_ThenThrowException(string json, string value)
            {
                var sut = new StringToDateTimeJsonConverter("yyyy-MM-dd HH:mm:ss");

                var ex = Assert.Throws<JsonException>(() => _ = sut.Deserialize<TestDateTimeEntity>(json));
                Assert.That(ex.Message, Is.EqualTo($"The JSON value: '{value}', could not be converted to System.DateTime."));
            }

            [TestCase(JsonNumber)]
            [TestCase(JsonFalse)]
            [TestCase(JsonTrue)]
            public void WhenJsonDateTimeIsNotString_ThenThrowException(string json)
            {
                var sut = new StringToDateTimeJsonConverter("yyyy-MM-dd HH:mm:ss");
                
                var ex = Assert.Throws<JsonException>(() => _ = sut.Deserialize<TestDateTimeEntity>(json));
                Assert.That(ex.Message, Does.StartWith("The JSON value could not be converted to System.DateTime. Path: $.datetime"));
            }
        }

        [TestFixture]
        public class Write : StringToDateTimeJsonConverterTests
        {
            [Test]
            public void WhenDateTimeSet_ThenOutputJsonValueInFormat()
            {
                var entity = new TestDateTimeEntity
                {
                    MyDateTime = new DateTime(2022, 1, 12, 9, 59, 20)
                };

                var sut = new StringToDateTimeJsonConverter("yyyy-MM-dd HH:mm:ss");

                var result = sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(Json));
            }
        }
    }
}