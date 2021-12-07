[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Json.SystemTextJson?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Json-SystemTextJson/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Json.SystemTextJson.svg)](https://www.nuget.org/packages/ByteDev.Json.SystemTextJson)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Json.SystemTextJson/blob/master/LICENSE)

# ByteDev.Json.SystemTextJson

.NET Standard library of functionality built on [System.Text.Json](https://www.nuget.org/packages/System.Text.Json/).

## Installation

ByteDev.Json.SystemTextJson is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Json.SystemTextJson`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Json.SystemTextJson/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Json.SystemTextJson/blob/master/docs/RELEASE-NOTES.md).

## Usage

All custom `JsonConverter` types are in namespace `ByteDev.Json.SystemTextJson.Serialization`.

---

### StringToDateTimeJsonConverter

Converter allows a JSON string representation of a date time to be automatically converted to a `DateTime` and back again using the supplied date time format string.

```csharp
public class TestEntity
{
    [JsonPropertyName("datetime")]
    public DateTime MyDateTime { get; set; }
}
```

```csharp
var converter = new StringToDateTimeJsonConverter("yyyy-MM-dd HH:mm:ss");

var options = new JsonSerializerOptions();
options.Converters.Add(converter);

string json = "{\"datetime\":\"2022-01-12 09:59:20\"}";

var obj = JsonSerializer.Deserialize<TestEntity>(json, options);

var newJson = JsonSerializer.Serialize(obj, options);

// newJson == json
```

---

### UnixEpochTimeToDateTimeJsonConverter

Converter allows a JSON number representation of a Unix epoch time to be automatically converted to a `DateTime` and back again.

Both Unix epoch times as seconds or milliseconds are supported.

The converter assumes that the `DateTime` is UTC.

```csharp
public class TestEntity
{
    [JsonPropertyName("datetime")]
    public DateTime MyDateTime { get; set; }
}
```

```csharp
var converter = new UnixEpochTimeToDateTimeJsonConverter
{
    Precision = UnixEpochTimePrecision.Seconds
};

var options = new JsonSerializerOptions();
options.Converters.Add(converter);

string json = "{\"datetime\":1641974376}";

var obj = JsonSerializer.Deserialize<TestEntity>(json, options);

var newJson = JsonSerializer.Serialize(obj, options);

// newJson == json
```

---

### EnumJsonConverter

Converter allows a JSON number or string representation of an enum to be automatically converted to a specified `Enum`.

As well as supporting both enum value (number) and name (string) alias string names can also be specified through use of `JsonEnumStringValueAttribute`.

```csharp
public enum LightSwitch
{
    On = 1,
        
    [JsonEnumStringValue("switchOff")]
    Off = 2,

    Faulty = 3
}

public class MyHouse
{
    [JsonPropertyName("lights")]
    public LightSwitch BedroomLights { get; set; }
}
```

```csharp
var converter = new EnumJsonConverter<LightSwitch>
{
    // false => Enum number value used
    // true => Enum string name used 
    // (or JsonEnumStringValueAttribute value if defined)
    WriteEnumName = true
};

var options = new JsonSerializerOptions();
options.Converters.Add(converter);

// Example JSON representations:
// - Using enum value => {"lights":1}
// - Using enum name => {"lights":"On"}
// - Using attribute name => {"lights":"switchOff"}

string json = "{\"lights\":\"switchOff\"}";

var obj = JsonSerializer.Deserialize<MyHouse>(json, options);

var newJson = JsonSerializer.Serialize(obj, options);

// newJson == json
```

---

### NumberToBoolJsonConverter

Converter allows a JSON number (integer) to be automatically converted to a .NET `Boolean`.

```csharp
public class BoolEntity
{
    [JsonPropertyName("myBool")]
    public bool MyBool { get; set; }
}
```

```csharp
// Optional true/false integer representations params
var converter = new NumberToBoolJsonConverter();

var options = new JsonSerializerOptions();
options.Converters.Add(converter);

string json = "{\"myBool\":1}";

var obj = JsonSerializer.Deserialize<BoolEntity>(json, options);

// obj.MyBool == true

var newJson = JsonSerializer.Serialize(obj, options);

// newJson == json
```
