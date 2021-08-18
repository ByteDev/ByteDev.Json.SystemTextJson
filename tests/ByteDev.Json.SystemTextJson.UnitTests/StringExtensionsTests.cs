using NUnit.Framework;

namespace ByteDev.Json.SystemTextJson.UnitTests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestFixture]
        public class IsJson : StringExtensionsTests
        {
            [TestCase(JsonExamples.JsonEmpty)]
            [TestCase(JsonExamples.JsonNumber)]
            [TestCase(JsonExamples.JsonString)]
            [TestCase(JsonExamples.JsonObject)]
            [TestCase(JsonExamples.JsonArray)]
            [TestCase(JsonExamples.JsonBoolFalse)]
            [TestCase(JsonExamples.JsonBoolTrue)]
            [TestCase(JsonExamples.JsonNull)]
            [TestCase(JsonExamples.JsonTwoProperties)]
            public void WhenJsonIsWellFormed_ThenReturnTrue(string sut)
            {
                var result = sut.IsJson();

                Assert.That(result, Is.True);
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            [TestCase("<")]
            [TestCase("{")]
            [TestCase(" {")]
            [TestCase("}")]
            [TestCase("{age:30}")]
            [TestCase("{ \"MyNumber\": 1 \"MyString\": \"John\" }")]
            public void WhenJsonIsNotWellFormed_ThenReturnFalse(string sut)
            {
                var result = sut.IsJson();

                Assert.That(result, Is.False);
            }
        }
    }
}