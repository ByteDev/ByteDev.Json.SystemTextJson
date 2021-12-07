using System;
using ByteDev.Json.SystemTextJson.Serialization;
using ByteDev.Json.SystemTextJson.UnitTests.TestEntities;
using NUnit.Framework;

namespace ByteDev.Json.SystemTextJson.UnitTests.Serialization
{
    [TestFixture]
    public class UnixEpochTimeToDateTimeJsonConverterTests
    {
        private const string JsonSeconds = "{\"datetime\":1641974376}";
        private const string JsonMilliseconds = "{\"datetime\":1641974376123}";

        [TestFixture]
        public class Read : UnixEpochTimeToDateTimeJsonConverterTests
        {
            [Test]
            public void WhenJsonTimeIsSeconds_ThenSetDateTime()
            {
                var sut = new UnixEpochTimeToDateTimeJsonConverter
                {
                    Precision = UnixEpochTimePrecision.Seconds
                };
            
                var result = sut.Deserialize<TestDateTimeEntity>(JsonSeconds);

                Assert.That(result.MyDateTime, Is.EqualTo(new DateTime(2022, 1, 12, 7, 59, 36)));
                Assert.That(result.MyDateTime.Kind, Is.EqualTo(DateTimeKind.Utc));
            }

            [Test]
            public void WhenJsonTimeIsMilliseconds_ThenSetDateTime()
            {
                var sut = new UnixEpochTimeToDateTimeJsonConverter
                {
                    Precision = UnixEpochTimePrecision.Milliseconds
                };

                var result = sut.Deserialize<TestDateTimeEntity>(JsonMilliseconds);

                Assert.That(result.MyDateTime, Is.EqualTo(new DateTime(2022, 1, 12, 7, 59, 36, 123)));
                Assert.That(result.MyDateTime.Kind, Is.EqualTo(DateTimeKind.Utc));
            }

            [Test]
            public void WhenPrecisionIsUndefined_ThenSetDateTimeForPrecisionSeconds()
            {
                var sut = new UnixEpochTimeToDateTimeJsonConverter
                {
                    Precision = (UnixEpochTimePrecision)99
                };
            
                var result = sut.Deserialize<TestDateTimeEntity>(JsonSeconds);

                Assert.That(result.MyDateTime, Is.EqualTo(new DateTime(2022, 1, 12, 7, 59, 36)));
                Assert.That(result.MyDateTime.Kind, Is.EqualTo(DateTimeKind.Utc));
            }

            [Test]
            public void WhenReadAndWrite_ThenValuesAreEqual()
            {
                var sut = new UnixEpochTimeToDateTimeJsonConverter
                {
                    Precision = UnixEpochTimePrecision.Seconds
                };
                
                var obj = sut.Deserialize<TestDateTimeEntity>(JsonSeconds);

                var json = sut.Serialize(obj);

                Assert.That(json, Is.EqualTo(JsonSeconds));
            }
        }

        [TestFixture]
        public class Write : UnixEpochTimeToDateTimeJsonConverterTests
        {
            [Test]
            public void WhenPrecisionIsSeconds_ThenWriteUnixEpoch()
            {
                var sut = new UnixEpochTimeToDateTimeJsonConverter
                {
                    Precision = UnixEpochTimePrecision.Seconds
                };

                var entity = new TestDateTimeEntity
                {
                    MyDateTime = new DateTime(2022, 1, 12, 7, 59, 36, DateTimeKind.Utc)
                };

                var result = sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(JsonSeconds));
            }

            [Test]
            public void WhenPrecisionIsMilliseconds_ThenWriteUnixEpoch()
            {
                var sut = new UnixEpochTimeToDateTimeJsonConverter
                {
                    Precision = UnixEpochTimePrecision.Milliseconds
                };

                var entity = new TestDateTimeEntity
                {
                    MyDateTime = new DateTime(2022, 1, 12, 7, 59, 36, 123, DateTimeKind.Utc)
                };

                var result = sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(JsonMilliseconds));
            }

            [Test]
            public void WhenPrecisionIsUndefined_ThenWriteUnixEpochAsSeconds()
            {
                var sut = new UnixEpochTimeToDateTimeJsonConverter
                {
                    Precision = (UnixEpochTimePrecision)99
                };

                var entity = new TestDateTimeEntity
                {
                    MyDateTime = new DateTime(2022, 1, 12, 7, 59, 36, DateTimeKind.Utc)
                };

                var result = sut.Serialize(entity);

                Assert.That(result, Is.EqualTo(JsonSeconds));
            }

            [Test]
            public void WhenWriteAndRead_ThenValuesAreEqual()
            {
                var sut = new UnixEpochTimeToDateTimeJsonConverter
                {
                    Precision = UnixEpochTimePrecision.Seconds
                };
                
                var entity = new TestDateTimeEntity
                {
                    MyDateTime = new DateTime(2022, 1, 12, 7, 59, 36, DateTimeKind.Utc)
                };

                var json = sut.Serialize(entity);

                var obj = sut.Deserialize<TestDateTimeEntity>(json);

                Assert.That(obj.MyDateTime, Is.EqualTo(entity.MyDateTime));
            }
        }
    }
}