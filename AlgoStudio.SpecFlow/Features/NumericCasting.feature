Feature: NumericCasting

Casting from type to type, including BigInteger, Decimal, Integers

Scenario: General Cast Checks
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning bytes is called
	Then the byte length is <ExpectedLength>
Examples:
	| TC           | Method          | ExpectedLength |
	| GeneralCasts | IntToLong       | 4              |
	| GeneralCasts | IntToULong      | 4              |
	| GeneralCasts | IntToUInt       | 4              |
	| GeneralCasts | IntToUShort     | 2              |
	| GeneralCasts | IntToShort      | 2              |
	| GeneralCasts | IntToByte       | 1              |
	| GeneralCasts | IntToSByte      | 1              |
	| GeneralCasts | UIntToLong      | 4              |
	| GeneralCasts | UIntToULong     | 4              |
	| GeneralCasts | UIntToInt       | 4              |
	| GeneralCasts | UIntToUShort    | 2              |
	| GeneralCasts | UIntToShort     | 2              |
	| GeneralCasts | UIntToByte      | 1              |
	| GeneralCasts | UIntToSByte     | 1              |
	| GeneralCasts | LongToULong     | 8              |
	| GeneralCasts | LongToInt       | 4              |
	| GeneralCasts | LongToUInt      | 4              |
	| GeneralCasts | LongToUShort    | 2              |
	| GeneralCasts | LongToShort     | 2              |
	| GeneralCasts | LongToByte      | 1              |
	| GeneralCasts | LongToSByte     | 1              |
	| GeneralCasts | ULongToInt      | 4              |
	| GeneralCasts | ULongToLong     | 8              |
	| GeneralCasts | ULongToUInt     | 4              |
	| GeneralCasts | ULongToUShort   | 2              |
	| GeneralCasts | ULongToShort    | 2              |
	| GeneralCasts | ULongToByte     | 1              |
	| GeneralCasts | ULongToSByte    | 1              |
	| GeneralCasts | ShortToInt      | 2              |
	| GeneralCasts | ShortToLong     | 2              |
	| GeneralCasts | ShortToULong    | 2              |
	| GeneralCasts | ShortToUInt     | 2              |
	| GeneralCasts | ShortToUShort   | 2              |
	| GeneralCasts | ShortToByte     | 1              |
	| GeneralCasts | ShortToSByte    | 1              |
	| GeneralCasts | UShortToInt     | 2              |
	| GeneralCasts | UShortToLong    | 2              |
	| GeneralCasts | UShortToULong   | 2              |
	| GeneralCasts | UShortToUInt    | 2              |
	| GeneralCasts | UShortToShort   | 2              |
	| GeneralCasts | UShortToByte    | 1              |
	| GeneralCasts | UShortToSByte   | 1              |
	| GeneralCasts | ByteToInt       | 1              |
	| GeneralCasts | ByteToLong      | 1              |
	| GeneralCasts | ByteToULong     | 1              |
	| GeneralCasts | ByteToUInt      | 1              |
	| GeneralCasts | ByteToUShort    | 1              |
	| GeneralCasts | ByteToShort     | 1              |
	| GeneralCasts | ByteToSByte     | 1              |
	| GeneralCasts | SByteToInt      | 4              |
	| GeneralCasts | SByteToLong     | 1              |
	| GeneralCasts | SByteToULong    | 1              |
	| GeneralCasts | SByteToUInt     | 1              |
	| GeneralCasts | SByteToUShort   | 1              |
	| GeneralCasts | SByteToShort    | 1              |
	| GeneralCasts | SByteToByte     | 1              |
	| GeneralCasts | DecimalToLong   | 7              |
	| GeneralCasts | DecimalToULong  | 7              |
	| GeneralCasts | DecimalToUInt   | 3              |
	| GeneralCasts | DecimalToInt    | 3              |
	| GeneralCasts | DecimalToUShort | 2              |
	| GeneralCasts | DecimalToShort  | 2              |
	| GeneralCasts | DecimalToByte   | 1              |
	| GeneralCasts | DecimalToSbyte  | 1              |


Scenario: Integer Casts
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning an integer is called
	Then The integer result is the same as C# would calculate
Examples:
	| TC           | Method        |
	| IntegerCasts | IntToLong     |
	| IntegerCasts | IntToULong    |
	| IntegerCasts | IntToUInt     |
	| IntegerCasts | IntToUShort   |
	| IntegerCasts | IntToShort    |
	| IntegerCasts | IntToByte     |
	| IntegerCasts | IntToSByte    |
	| IntegerCasts | UIntToLong    |
	| IntegerCasts | UIntToULong   |
	| IntegerCasts | UIntToInt     |
	| IntegerCasts | UIntToUShort  |
	| IntegerCasts | UIntToShort   |
	| IntegerCasts | UIntToByte    |
	| IntegerCasts | UIntToSByte   |
	| IntegerCasts | LongToULong   |
	| IntegerCasts | LongToInt     |
	| IntegerCasts | LongToUInt    |
	| IntegerCasts | LongToUShort  |
	| IntegerCasts | LongToShort   |
	| IntegerCasts | LongToByte    |
	| IntegerCasts | LongToSByte   |
	| IntegerCasts | ULongToInt    |
	| IntegerCasts | ULongToLong   |
	| IntegerCasts | ULongToUInt   |
	| IntegerCasts | ULongToUShort |
	| IntegerCasts | ULongToShort  |
	| IntegerCasts | ULongToByte   |
	| IntegerCasts | ULongToSByte  |
	| IntegerCasts | ShortToInt    |
	| IntegerCasts | ShortToLong   |
	| IntegerCasts | ShortToULong  |
	| IntegerCasts | ShortToUInt   |
	| IntegerCasts | ShortToUShort |
	| IntegerCasts | ShortToByte   |
	| IntegerCasts | ShortToSByte  |
	| IntegerCasts | UShortToInt   |
	| IntegerCasts | UShortToLong  |
	| IntegerCasts | UShortToULong |
	| IntegerCasts | UShortToUInt  |
	| IntegerCasts | UShortToShort |
	| IntegerCasts | UShortToByte  |
	| IntegerCasts | UShortToSByte |
	| IntegerCasts | ByteToInt     |
	| IntegerCasts | ByteToLong    |
	| IntegerCasts | ByteToULong   |
	| IntegerCasts | ByteToUInt    |
	| IntegerCasts | ByteToUShort  |
	| IntegerCasts | ByteToShort   |
	| IntegerCasts | ByteToSByte   |
	| IntegerCasts | SByteToInt    |
	| IntegerCasts | SByteToLong   |
	| IntegerCasts | SByteToULong  |
	| IntegerCasts | SByteToUInt   |
	| IntegerCasts | SByteToUShort |
	| IntegerCasts | SByteToShort  |
	| IntegerCasts | SByteToByte   |

Scenario: Decimal To Integer Casts
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning an integer is called
	Then The integer result is the same as C# would calculate
Examples:
	| TC                  | Method          |
	| DecimalIntegerCasts | DecimalToLong   |
	| DecimalIntegerCasts | DecimalToULong  |
	| DecimalIntegerCasts | DecimalToUInt   |
	| DecimalIntegerCasts | DecimalToInt    |
	| DecimalIntegerCasts | DecimalToUShort |
	| DecimalIntegerCasts | DecimalToShort  |
	| DecimalIntegerCasts | DecimalToByte   |
	| DecimalIntegerCasts | DecimalToSbyte  |


Scenario: Integer to Decimal Casts
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning a decimal is called
	Then The result is the same as C# would calculate
Examples:
	| TC                  | Method          |
	| DecimalIntegerCasts | LongToDecimal   |
	| DecimalIntegerCasts | ULongToDecimal  |
	| DecimalIntegerCasts | UIntToDecimal   |
	| DecimalIntegerCasts | IntToDecimal    |
	| DecimalIntegerCasts | UShortToDecimal |
	| DecimalIntegerCasts | ShortToDecimal  |
	| DecimalIntegerCasts | ByteToDecimal   |
	| DecimalIntegerCasts | SbyteToDecimal  |