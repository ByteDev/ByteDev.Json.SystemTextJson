# Release Notes

## 1.3.0 - ?

Breaking changes:
- (None)

New features:
- Added `StringToGuidJsonConverter` class.

Bug fixes / internal changes:
- `StringToBoolJsonConverter` contructor now throws exception on true and false params being equal.
- `NumberToBoolJsonConverter` contructor now throws exception on true and false params being equal.

## 1.2.0 - 07 December 2021

Breaking changes:
- (None)

New features:
- Added `StringExtensions.IsJson` method.
- Added `NumberToBoolJsonConverter` class.
- Added `StringToBoolJsonConverter` class.

Bug fixes / internal changes:
- (None)

## 1.1.0 - 04 August 2021

Breaking changes:
- (None)

New features:
- Added optional `EnumJsonConverter` constructor param that allows a default enum value to be defined.

Bug fixes / internal changes:
- (None)

## 1.0.0 - 04 August 2021

Initial version.
