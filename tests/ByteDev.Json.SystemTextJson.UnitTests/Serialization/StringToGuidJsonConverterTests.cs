using System;
using System.Text.Json;
using ByteDev.Json.SystemTextJson.Serialization;
using ByteDev.Json.SystemTextJson.UnitTests.TestEntities;
using NUnit.Framework;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    [TestFixture]
    public class StringToGuidJsonConverterTests
    {
        public const string JsonStringEmpty = "{\"myGuid\":\"\"}";
        
        public const string JsonStringHyphen = "{\"myGuid\":\"c62a82b3-5e0d-47bf-9a12-b027ff56d3e3\"}";
        public const string JsonStringNoHyphen = "{\"myGuid\":\"c62a82b35e0d47bf9a12b027ff56d3e3\"}";

        public const string JsonStringCurlyBrackets = "{\"myGuid\":\"{c62a82b3-5e0d-47bf-9a12-b027ff56d3e3}\"}";
        public const string JsonStringRoundBrackets = "{\"myGuid\":\"(c62a82b3-5e0d-47bf-9a12-b027ff56d3e3)\"}";

        public const string JsonStringNoHyphenCurlyBrackets = "{\"myGuid\":\"{c62a82b35e0d47bf9a12b027ff56d3e3}\"}";
        public const string JsonStringNoHyphenRoundBrackets = "{\"myGuid\":\"(c62a82b35e0d47bf9a12b027ff56d3e3)\"}";

        public const string JsonStringUppercase = "{\"myGuid\":\"C62A82B3-5E0D-47BF-9A12-B027FF56D3E3\"}";
        public const string JsonStringUppercaseCurlyBrackets = "{\"myGuid\":\"{C62A82B3-5E0D-47BF-9A12-B027FF56D3E3}\"}";
        public const string JsonStringUppercaseRoundBrackets = "{\"myGuid\":\"(C62A82B3-5E0D-47BF-9A12-B027FF56D3E3)\"}";

        public const string JsonStringUppercaseNoHypen = "{\"myGuid\":\"C62A82B35E0D47BF9A12B027FF56D3E3\"}";
        public const string JsonStringUppercaseNoHypenCurlyBrackets = "{\"myGuid\":\"{C62A82B35E0D47BF9A12B027FF56D3E3}\"}";
        public const string JsonStringUppercaseNoHypenRoundBrackets = "{\"myGuid\":\"(C62A82B35E0D47BF9A12B027FF56D3E3)\"}";

        private StringToGuidJsonConverter _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new StringToGuidJsonConverter();
        }

        [TestFixture]
        public class Constructor : StringToGuidJsonConverterTests
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase("Z")]
            public void WhenWriteFormatIsNotValid_ThenThrowException(string writeFormat)
            {
                Assert.Throws<ArgumentException>(() => _ = new StringToGuidJsonConverter(writeFormat));
            }
        }
        
        [TestFixture]
        public class Read : StringToGuidJsonConverterTests
        {
            [Test]
            public void WhenJsonValueIsEmpty_ThenThrowException()
            {
                var ex = Assert.Throws<JsonException>(() => _sut.Deserialize<TestGuidEntity>(JsonStringEmpty));

                Assert.That(ex.Message, Is.EqualTo("The JSON value: '', could not be converted to System.Guid."));
            }

            [TestCase(JsonStringHyphen)]
            [TestCase(JsonStringNoHyphen)]
            [TestCase(JsonStringCurlyBrackets)]
            [TestCase(JsonStringRoundBrackets)]
            [TestCase(JsonStringNoHyphenCurlyBrackets)]
            [TestCase(JsonStringNoHyphenRoundBrackets)]
            [TestCase(JsonStringUppercase)]
            [TestCase(JsonStringUppercaseCurlyBrackets)]
            [TestCase(JsonStringUppercaseRoundBrackets)]
            [TestCase(JsonStringUppercaseNoHypen)]
            [TestCase(JsonStringUppercaseNoHypenCurlyBrackets)]
            [TestCase(JsonStringUppercaseNoHypenRoundBrackets)]
            public void WhenJsonGuidInDifferentFormats_ThenSetGuid(string json)
            {
                var result = _sut.Deserialize<TestGuidEntity>(json);

                Assert.That(result.MyGuid, Is.EqualTo(Guid.Parse("c62a82b3-5e0d-47bf-9a12-b027ff56d3e3")));
            }
        }

        [TestFixture]
        public class Write : StringToGuidJsonConverterTests
        {
            private readonly Guid _myGuid = Guid.Parse("c62a82b3-5e0d-47bf-9a12-b027ff56d3e3");

            private TestGuidEntity _entity;

            [SetUp]
            public new void SetUp()
            {
                _entity = new TestGuidEntity
                {
                    MyGuid = _myGuid
                };
            }

            [Test]
            public void WhenGuidSet_AndWriteFormatD_ThenOutputJsonValueInFormat()
            {
                var sut = new StringToGuidJsonConverter("D");

                var result = sut.Serialize(_entity);

                Assert.That(result, Is.EqualTo(JsonStringHyphen));
            }

            [Test]
            public void WhenGuidSet_AndWriteFormatN_ThenOutputJsonValueInFormat()
            {
                var sut = new StringToGuidJsonConverter("N");

                var result = sut.Serialize(_entity);

                Assert.That(result, Is.EqualTo(JsonStringNoHyphen));
            }

            [Test]
            public void WhenGuidSet_AndWriteFormatB_ThenOutputJsonValueInFormat()
            {
                var sut = new StringToGuidJsonConverter("B");

                var result = sut.Serialize(_entity);

                Assert.That(result, Is.EqualTo(JsonStringCurlyBrackets));
            }

            [Test]
            public void WhenGuidSet_AndWriteFormatP_ThenOutputJsonValueInFormat()
            {
                var sut = new StringToGuidJsonConverter("P");

                var result = sut.Serialize(_entity);

                Assert.That(result, Is.EqualTo(JsonStringRoundBrackets));
            }
        }
    }
}