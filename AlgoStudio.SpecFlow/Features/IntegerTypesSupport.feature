Feature: IntegerTypesSupport



Scenario: Addition
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are added
	Then The integer result is the same as C# would calculate

Examples:
	| TC                | a                     | b                     | c          |
	| Int32Arithmetic   | 1                     | 2                     | int        |
	| Int32Arithmetic   | 2                     | 3                     | int        |
	| Int32Arithmetic   | 1                     | 2147483646            | int        |
	| Int32Arithmetic   | 2147483647            | 0                     | int        |
	| Int32Arithmetic   | -2147483648           | 0                     | int        |
	| Int32Arithmetic   | 123456789             | -987654321            | int        |
	| Int32Arithmetic   | 0                     | 0                     | int        |
	| Int32Arithmetic   | 2147483647            | -2147483648           | int        |
	| Int16Arithmetic   | 1                     | 2                     | short      |
	| Int16Arithmetic   | 2                     | 3                     | short      |
	| Int16Arithmetic   | 1                     | 32767                 | short      |
	| Int16Arithmetic   | 32767                 | 0                     | short      |
	| Int16Arithmetic   | -32768                | 0                     | short      |
	| Int16Arithmetic   | 12345                 | -9876                 | short      |
	| Int16Arithmetic   | 0                     | 0                     | short      |
	| Int16Arithmetic   | 32767                 | -32768                | short      |
	| Int8Arithmetic    | 1                     | 2                     | sbyte      |
	| Int8Arithmetic    | 2                     | 3                     | sbyte      |
	| Int8Arithmetic    | 1                     | 127                   | sbyte      |
	| Int8Arithmetic    | 127                   | 0                     | sbyte      |
	| Int8Arithmetic    | -128                  | 0                     | sbyte      |
	| Int8Arithmetic    | 123                   | -98                   | sbyte      |
	| Int8Arithmetic    | 0                     | 0                     | sbyte      |
	| Int8Arithmetic    | 127                   | -128                  | sbyte      |
	| Int64Arithmetic   | 1                     | 2                     | long       |
	| Int64Arithmetic   | 2                     | 3                     | long       |
	| Int64Arithmetic   | 1                     | 9223372036854775806   | long       |
	| Int64Arithmetic   | 9223372036854775807   | 0                     | long       |
	| Int64Arithmetic   | -9223372036854775808  | 0                     | long       |
	| Int64Arithmetic   | 123456789             | -987654321            | long       |
	| Int64Arithmetic   | 0                     | 0                     | long       |
	| Int64Arithmetic   | 9223372036854775807   | -9223372036854775808  | long       |
	| UInt32Arithmetic  | 1                     | 2                     | uint       |
	| UInt32Arithmetic  | 2                     | 3                     | uint       |
	| UInt32Arithmetic  | 1                     | 2147483646            | uint       |
	| UInt32Arithmetic  | 2147483647            | 0                     | uint       |
	| UInt32Arithmetic  | 4294967295            | 0                     | uint       |
	| UInt32Arithmetic  | 123456789             | 3421772750            | uint       |
	| UInt32Arithmetic  | 0                     | 0                     | uint       |
	| UInt32Arithmetic  | 2147483647            | 2147483648            | uint       |
	| UInt16Arithmetic  | 1                     | 2                     | ushort     |
	| UInt16Arithmetic  | 2                     | 3                     | ushort     |
	| UInt16Arithmetic  | 1                     | 32767                 | ushort     |
	| UInt16Arithmetic  | 32767                 | 0                     | ushort     |
	| UInt16Arithmetic  | 65535                 | 0                     | ushort     |
	| UInt16Arithmetic  | 12345                 | 55560                 | ushort     |
	| UInt16Arithmetic  | 0                     | 0                     | ushort     |
	| UInt16Arithmetic  | 32767                 | 32768                 | ushort     |
	| UInt8Arithmetic   | 1                     | 2                     | byte       |
	| UInt8Arithmetic   | 2                     | 3                     | byte       |
	| UInt8Arithmetic   | 1                     | 127                   | byte       |
	| UInt8Arithmetic   | 127                   | 0                     | byte       |
	| UInt8Arithmetic   | 255                   | 0                     | byte       |
	| UInt8Arithmetic   | 123                   | 158                   | byte       |
	| UInt8Arithmetic   | 0                     | 0                     | byte       |
	| UInt8Arithmetic   | 127                   | 128                   | byte       |
	| UInt64Arithmetic  | 1                     | 2                     | ulong      |
	| UInt64Arithmetic  | 2                     | 3                     | ulong      |
	| UInt64Arithmetic  | 1                     | 9223372036854775806   | ulong      |
	| UInt64Arithmetic  | 9223372036854775807   | 0                     | ulong      |
	| UInt64Arithmetic  | 18446744073709551615  | 0                     | ulong      |
	| UInt64Arithmetic  | 123456789             | 18446744073586099737  | ulong      |
	| UInt64Arithmetic  | 0                     | 0                     | ulong      |
	| UInt64Arithmetic  | 9223372036854775807   | 9223372036854775808   | ulong      |
	| BigIntArithmetic  | 1                     | 2                     | BigInteger |
	| BigIntArithmetic  | 2                     | 3                     | BigInteger |
	| BigIntArithmetic  | 1                     | 92233720368547758060  | BigInteger |
	| BigIntArithmetic  | 92233720368547758070  | 0                     | BigInteger |
	| BigIntArithmetic  | -92233720368547758080 | 0                     | BigInteger |
	| BigIntArithmetic  | 1234567890            | -9876543210           | BigInteger |
	| BigIntArithmetic  | 0                     | 0                     | BigInteger |
	| BigIntArithmetic  | 92233720368547758070  | -92233720368547758080 | BigInteger |
	

Scenario: Multiplication
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are multiplied
	Then The integer result is the same as C# would calculate
Examples:
	| TC                | a                    | b                    | c          |
	| Int32Arithmetic   | 0                    | 0                    | int        |
	| Int32Arithmetic   | 1                    | 1                    | int        |
	| Int32Arithmetic   | -1                   | -1                   | int        |
	| Int32Arithmetic   | 1                    | -1                   | int        |
	| Int32Arithmetic   | -1                   | 1                    | int        |
	| Int32Arithmetic   | 0                    | 1                    | int        |
	| Int32Arithmetic   | 1                    | 0                    | int        |
	| Int32Arithmetic   | 0                    | -1                   | int        |
	| Int32Arithmetic   | -1                   | 0                    | int        |
	| Int32Arithmetic   | 2147483647           | 1                    | int        |
	| Int32Arithmetic   | -2147483648          | 1                    | int        |
	| Int32Arithmetic   | 2147483647           | -1                   | int        |
	| Int32Arithmetic   | -2147483648          | -1                   | int        |
	| Int32Arithmetic   | 32767                | 65536                | int        |
	| Int32Arithmetic   | -32768               | 65536                | int        |
	| Int32Arithmetic   | 32767                | -65536               | int        |
	| Int32Arithmetic   | -32768               | -65536               | int        |
	| Int16Arithmetic   | 0                    | 0                    | short      |
	| Int16Arithmetic   | 1                    | 1                    | short      |
	| Int16Arithmetic   | -1                   | -1                   | short      |
	| Int16Arithmetic   | 1                    | -1                   | short      |
	| Int16Arithmetic   | -1                   | 1                    | short      |
	| Int16Arithmetic   | 0                    | 1                    | short      |
	| Int16Arithmetic   | 1                    | 0                    | short      |
	| Int16Arithmetic   | 0                    | -1                   | short      |
	| Int16Arithmetic   | -1                   | 0                    | short      |
	| Int16Arithmetic   | 32767                | 1                    | short      |
	| Int16Arithmetic   | -32768               | 1                    | short      |
	| Int16Arithmetic   | 32767                | -1                   | short      |
	| Int16Arithmetic   | -32768               | -1                   | short      |
	| Int16Arithmetic   | 127                  | 256                  | short      |
	| Int16Arithmetic   | -128                 | 256                  | short      |
	| Int16Arithmetic   | 127                  | -256                 | short      |
	| Int16Arithmetic   | -128                 | -256                 | short      |
	| Int8Arithmetic    | 0                    | 0                    | sbyte      |
	| Int8Arithmetic    | 1                    | 1                    | sbyte      |
	| Int8Arithmetic    | -1                   | -1                   | sbyte      |
	| Int8Arithmetic    | 1                    | -1                   | sbyte      |
	| Int8Arithmetic    | -1                   | 1                    | sbyte      |
	| Int8Arithmetic    | 0                    | 1                    | sbyte      |
	| Int8Arithmetic    | 1                    | 0                    | sbyte      |
	| Int8Arithmetic    | 0                    | -1                   | sbyte      |
	| Int8Arithmetic    | -1                   | 0                    | sbyte      |
	| Int8Arithmetic    | 127                  | 1                    | sbyte      |
	| Int8Arithmetic    | -128                 | 1                    | sbyte      |
	| Int8Arithmetic    | 127                  | -1                   | sbyte      |
	| Int8Arithmetic    | -128                 | -1                   | sbyte      |
	| Int8Arithmetic    | 7                    | 16                   | sbyte      |
	| Int8Arithmetic    | -8                   | 16                   | sbyte      |
	| Int8Arithmetic    | 7                    | -16                  | sbyte      |
	| Int8Arithmetic    | -8                   | -16                  | sbyte      |
	| Int64Arithmetic   | 0                    | 0                    | long       |
	| Int64Arithmetic   | 1                    | 1                    | long       |
	| Int64Arithmetic   | -1                   | -1                   | long       |
	| Int64Arithmetic   | 1                    | -1                   | long       |
	| Int64Arithmetic   | -1                   | 1                    | long       |
	| Int64Arithmetic   | 0                    | 1                    | long       |
	| Int64Arithmetic   | 1                    | 0                    | long       |
	| Int64Arithmetic   | 0                    | -1                   | long       |
	| Int64Arithmetic   | -1                   | 0                    | long       |
	| Int64Arithmetic   | 9223372036854775807  | 1                    | long       |
	| Int64Arithmetic   | -9223372036854775808 | 1                    | long       |
	| Int64Arithmetic   | 9223372036854775807  | -1                   | long       |
	| Int64Arithmetic   | -9223372036854775808 | -1                   | long       |
	| Int64Arithmetic   | 2147483647           | 4294967296           | long       |
	| Int64Arithmetic   | -2147483648          | 4294967296           | long       |
	| Int64Arithmetic   | 2147483647           | -4294967296          | long       |
	| Int64Arithmetic   | -2147483648          | -4294967296          | long       |
	| UInt32Arithmetic  | 0                    | 0                    | uint       |
	| UInt32Arithmetic  | 1                    | 1                    | uint       |
	| UInt32Arithmetic  | 1                    | 0                    | uint       |
	| UInt32Arithmetic  | 0                    | 1                    | uint       |
	| UInt32Arithmetic  | 2147483647           | 1                    | uint       |
	| UInt32Arithmetic  | 4294967295           | 1                    | uint       |
	| UInt32Arithmetic  | 32767                | 65536                | uint       |
	| UInt16Arithmetic  | 0                    | 0                    | ushort     |
	| UInt16Arithmetic  | 1                    | 1                    | ushort     |
	| UInt16Arithmetic  | 1                    | 0                    | ushort     |
	| UInt16Arithmetic  | 0                    | 1                    | ushort     |
	| UInt16Arithmetic  | 32767                | 1                    | ushort     |
	| UInt16Arithmetic  | 65535                | 1                    | ushort     |
	| UInt16Arithmetic  | 127                  | 256                  | ushort     |
	| UInt8Arithmetic   | 0                    | 0                    | byte       |
	| UInt8Arithmetic   | 1                    | 1                    | byte       |
	| UInt8Arithmetic   | 1                    | 0                    | byte       |
	| UInt8Arithmetic   | 0                    | 1                    | byte       |
	| UInt8Arithmetic   | 127                  | 1                    | byte       |
	| UInt8Arithmetic   | 255                  | 1                    | byte       |
	| UInt8Arithmetic   | 7                    | 16                   | byte       |
	| UInt64Arithmetic  | 0                    | 0                    | ulong      |
	| UInt64Arithmetic  | 1                    | 1                    | ulong      |
	| UInt64Arithmetic  | 1                    | 0                    | ulong      |
	| UInt64Arithmetic  | 0                    | 1                    | ulong      |
	| UInt64Arithmetic  | 9223372036854775807  | 1                    | ulong      |
	| UInt64Arithmetic  | 18446744073709551615 | 1                    | ulong      |
	| UInt64Arithmetic  | 9223372036854775807  | 1                    | ulong      |
	| UInt64Arithmetic  | 18446744073709551615 | 1                    | ulong      |
	| UInt64Arithmetic  | 2147483647           | 4294967296           | ulong      |
	| UInt64Arithmetic  | 2147483648           | 4294967296           | ulong      |
	| UInt64Arithmetic  | 2147483647           | 18446744069414584320 | ulong      |
	| UInt64Arithmetic  | 2147483648           | 18446744069414584320 | ulong      |
	| BigIntArithmetic  | 0                    | 0                    | BigInteger       |
	| BigIntArithmetic  | 1                    | 1                    | BigInteger       |
	| BigIntArithmetic  | -1                   | -1                   | BigInteger       |
	| BigIntArithmetic  | 1                    | -1                   | BigInteger       |
	| BigIntArithmetic  | -1                   | 1                    | BigInteger       |
	| BigIntArithmetic  | 0                    | 1                    | BigInteger       |
	| BigIntArithmetic  | 1                    | 0                    | BigInteger       |
	| BigIntArithmetic  | 0                    | -1                   | BigInteger       |
	| BigIntArithmetic  | -1                   | 0                    | BigInteger       |
	| BigIntArithmetic  | 92233720368547758070  | 1                   | BigInteger       |
	| BigIntArithmetic  | -92233720368547758080 | 1                   | BigInteger       |
	| BigIntArithmetic  | 92233720368547758070  | -1                  | BigInteger       |
	| BigIntArithmetic  | -92233720368547758080 | -1                  | BigInteger       |
	| BigIntArithmetic  | 2147483647           | 4294967296           | BigInteger       |
	| BigIntArithmetic  | -2147483648          | 4294967296           | BigInteger       |
	| BigIntArithmetic  | 2147483647           | -4294967296          | BigInteger       |
	| BigIntArithmetic  | -2147483648          | -4294967296          | BigInteger       |

Scenario: Division
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are divided
	Then The integer result is the same as C# would calculate
Examples:
	| TC                | a                    | b           | c          |
	| Int32Arithmetic   | 1                    | 1           | int        |
	| Int32Arithmetic   | -1                   | -1          | int        |
	| Int32Arithmetic   | 1                    | -1          | int        |
	| Int32Arithmetic   | -1                   | 1           | int        |
	| Int32Arithmetic   | 0                    | 1           | int        |
	| Int32Arithmetic   | 0                    | -1          | int        |
	| Int32Arithmetic   | 2147483647           | 1           | int        |
	| Int32Arithmetic   | -2147483647          | -1          | int        |
	| Int32Arithmetic   | 2147483647           | -1          | int        |
	| Int32Arithmetic   | -2147483648          | 1           | int        |
	| Int32Arithmetic   | 32767                | 65536       | int        |
	| Int32Arithmetic   | -32768               | 65536       | int        |
	| Int32Arithmetic   | 32767                | -65536      | int        |
	| Int32Arithmetic   | -32768               | -65536      | int        |
	| Int16Arithmetic   | 1                    | 1           | short      |
	| Int16Arithmetic   | -1                   | -1          | short      |
	| Int16Arithmetic   | 1                    | -1          | short      |
	| Int16Arithmetic   | -1                   | 1           | short      |
	| Int16Arithmetic   | 0                    | 1           | short      |
	| Int16Arithmetic   | 0                    | -1          | short      |
	| Int16Arithmetic   | 32767                | 1           | short      |
	| Int16Arithmetic   | -32768               | -1          | short      |
	| Int16Arithmetic   | 32767                | -1          | short      |
	| Int16Arithmetic   | -32768               | 1           | short      |
	| Int16Arithmetic   | 127                  | 256         | short      |
	| Int16Arithmetic   | -128                 | 256         | short      |
	| Int16Arithmetic   | 127                  | -256        | short      |
	| Int16Arithmetic   | -128                 | -256        | short      |
	| Int8Arithmetic    | 1                    | 1           | sbyte      |
	| Int8Arithmetic    | -1                   | -1          | sbyte      |
	| Int8Arithmetic    | 1                    | -1          | sbyte      |
	| Int8Arithmetic    | -1                   | 1           | sbyte      |
	| Int8Arithmetic    | 0                    | 1           | sbyte      |
	| Int8Arithmetic    | 1                    | 0           | sbyte      |
	| Int8Arithmetic    | 0                    | -1          | sbyte      |
	| Int8Arithmetic    | -1                   | 0           | sbyte      |
	| Int8Arithmetic    | 127                  | 1           | sbyte      |
	| Int8Arithmetic    | -128                 | 1           | sbyte      |
	| Int8Arithmetic    | 127                  | -1          | sbyte      |
	| Int8Arithmetic    | -128                 | -1          | sbyte      |
	| Int8Arithmetic    | 7                    | 16          | sbyte      |
	| Int8Arithmetic    | -8                   | 16          | sbyte      |
	| Int8Arithmetic    | 7                    | -16         | sbyte      |
	| Int8Arithmetic    | -8                   | -16         | sbyte      |
	| Int64Arithmetic   | 0                    | 0           | long       |
	| Int64Arithmetic   | 1                    | 1           | long       |
	| Int64Arithmetic   | -1                   | -1          | long       |
	| Int64Arithmetic   | 1                    | -1          | long       |
	| Int64Arithmetic   | -1                   | 1           | long       |
	| Int64Arithmetic   | 0                    | 1           | long       |
	| Int64Arithmetic   | 1                    | 0           | long       |
	| Int64Arithmetic   | 0                    | -1          | long       |
	| Int64Arithmetic   | -1                   | 0           | long       |
	| Int64Arithmetic   | 9223372036854775807  | 1           | long       |
	| Int64Arithmetic   | -9223372036854775808 | 1           | long       |
	| Int64Arithmetic   | 9223372036854775807  | -1          | long       |
	| Int64Arithmetic   | -9223372036854775807 | -1          | long       |
	| Int64Arithmetic   | 2147483647           | 4294967296  | long       |
	| Int64Arithmetic   | -2147483648          | 4294967296  | long       |
	| Int64Arithmetic   | 2147483647           | -4294967296 | long       |
	| Int64Arithmetic   | -2147483648          | -4294967296 | long       |
	| UInt32Arithmetic  | 1                    | 1           | uint       |
	| UInt32Arithmetic  | 1                    | 0           | uint       |
	| UInt32Arithmetic  | 0                    | 1           | uint       |
	| UInt32Arithmetic  | 2147483647           | 1           | uint       |
	| UInt32Arithmetic  | 4294967295           | 1           | uint       |
	| UInt32Arithmetic  | 32767                | 65536       | uint       |
	| UInt16Arithmetic  | 1                    | 1           | ushort     |
	| UInt16Arithmetic  | 1                    | 0           | ushort     |
	| UInt16Arithmetic  | 0                    | 1           | ushort     |
	| UInt16Arithmetic  | 32767                | 1           | ushort     |
	| UInt16Arithmetic  | 65535                | 1           | ushort     |
	| UInt16Arithmetic  | 127                  | 256         | ushort     |
	| UInt8Arithmetic   | 1                    | 1           | byte       |
	| UInt8Arithmetic   | 1                    | 0           | byte       |
	| UInt8Arithmetic   | 0                    | 1           | byte       |
	| UInt8Arithmetic   | 127                  | 1           | byte       |
	| UInt8Arithmetic   | 255                  | 1           | byte       |
	| UInt8Arithmetic   | 7                    | 16          | byte       |
	| UInt64Arithmetic  | 0                    | 0           | ulong      |
	| UInt64Arithmetic  | 1                    | 1           | ulong      |
	| UInt64Arithmetic  | 1                    | 0           | ulong      |
	| UInt64Arithmetic  | 0                    | 1           | ulong      |
	| UInt64Arithmetic  | 9223372036854775807  | 1           | ulong      |
	| UInt64Arithmetic  | 18446744073709551615 | 1           | ulong      |
	| UInt64Arithmetic  | 2147483647           | 4294967296  | ulong      |
	| UInt32Arithmetic  | 4294967295           | 2           | uint       |
	| UInt32Arithmetic  | 2147483647           | 2           | uint       |
	| UInt32Arithmetic  | 65536                | 32767       | uint       |
	| UInt16Arithmetic  | 65535                | 2           | ushort     |
	| UInt16Arithmetic  | 32767                | 2           | ushort     |
	| UInt16Arithmetic  | 256                  | 127         | ushort     |
	| UInt8Arithmetic   | 255                  | 2           | byte       |
	| UInt8Arithmetic   | 127                  | 2           | byte       |
	| UInt8Arithmetic   | 16                   | 7           | byte       |
	| UInt64Arithmetic  | 18446744073709551615 | 2           | ulong      |
	| UInt64Arithmetic  | 9223372036854775807  | 2           | ulong      |
	| UInt64Arithmetic  | 4294967296           | 2147483647  | ulong      |

	| BigIntArithmetic  | 0                    | 0           | BigInteger |
	| BigIntArithmetic  | 1                    | 1           | BigInteger |
	| BigIntArithmetic  | -1                   | -1          | BigInteger |
	| BigIntArithmetic  | 1                    | -1          | BigInteger |
	| BigIntArithmetic  | -1                   | 1           | BigInteger |
	| BigIntArithmetic  | 0                    | 1           | BigInteger |
	| BigIntArithmetic  | 1                    | 0           | BigInteger |
	| BigIntArithmetic  | 0                    | -1          | BigInteger |
	| BigIntArithmetic  | 0                    | -5          | BigInteger |
	| BigIntArithmetic  | -1                   | 0           | BigInteger |
	| BigIntArithmetic  | 92233720368547758070  | 1           | BigInteger |
	| BigIntArithmetic  | -92233720368547758080 | 1           | BigInteger |
	| BigIntArithmetic  | 92233720368547758070  | -1          | BigInteger |
	| BigIntArithmetic  | -92233720368547758080 | -1          | BigInteger |
	| BigIntArithmetic  | 2147483647           | 4294967296  | BigInteger |
	| BigIntArithmetic  | -2147483648          | 4294967296  | BigInteger |
	| BigIntArithmetic  | 2147483647           | -4294967296 | BigInteger |
	| BigIntArithmetic  | -2147483648          | -4294967296 | BigInteger |
	

Scenario: Subtraction
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are subtracted
	Then The integer result is the same as C# would calculate
Examples:
	| TC                | a                    | b           | c          |
	| Int32Arithmetic   | 10                   | 5           | int        |
	| Int32Arithmetic   | -10                  | -5          | int        |
	| Int32Arithmetic   | 10                   | -5          | int        |
	| Int32Arithmetic   | -10                  | 5           | int        |
	| Int32Arithmetic   | 0                    | 5           | int        |
	| Int32Arithmetic   | 0                    | -5          | int        |
	| Int32Arithmetic   | 2147483647           | 1           | int        |
	| Int32Arithmetic   | -2147483648          | -1          | int        |
	| Int32Arithmetic   | 2147483647           | -1          | int        |
	| Int32Arithmetic   | -2147483648          | 1           | int        |
	| Int32Arithmetic   | 32767                | 65536       | int        |
	| Int32Arithmetic   | -32768               | 65536       | int        |
	| Int32Arithmetic   | 32767                | -65536      | int        |
	| Int32Arithmetic   | -32768               | -65536      | int        |
	| Int16Arithmetic   | 10                   | 5           | short      |
	| Int16Arithmetic   | -10                  | -5          | short      |
	| Int16Arithmetic   | 10                   | -5          | short      |
	| Int16Arithmetic   | -10                  | 5           | short      |
	| Int16Arithmetic   | 0                    | 5           | short      |
	| Int16Arithmetic   | 0                    | -5          | short      |
	| Int16Arithmetic   | 32767                | 1           | short      |
	| Int16Arithmetic   | -32768               | -1          | short      |
	| Int16Arithmetic   | 32767                | -1          | short      |
	| Int16Arithmetic   | -32768               | 1           | short      |
	| Int16Arithmetic   | 127                  | 256         | short      |
	| Int16Arithmetic   | -128                 | 256         | short      |
	| Int16Arithmetic   | 127                  | -256        | short      |
	| Int16Arithmetic   | -128                 | -256        | short      |
	| Int8Arithmetic    | 10                   | 5           | sbyte      |
	| Int8Arithmetic    | -10                  | -5          | sbyte      |
	| Int8Arithmetic    | 10                   | -5          | sbyte      |
	| Int8Arithmetic    | -10                  | 5           | sbyte      |
	| Int8Arithmetic    | 0                    | 5           | sbyte      |
	| Int8Arithmetic    | 0                    | -5          | sbyte      |
	| Int8Arithmetic    | 127                  | 1           | sbyte      |
	| Int8Arithmetic    | -128                 | -1          | sbyte      |
	| Int8Arithmetic    | 127                  | -1          | sbyte      |
	| Int8Arithmetic    | -128                 | 1           | sbyte      |
	| Int8Arithmetic    | 7                    | 16          | sbyte      |
	| Int8Arithmetic    | -8                   | 16          | sbyte      |
	| Int8Arithmetic    | 7                    | -16         | sbyte      |
	| Int8Arithmetic    | -8                   | -16         | sbyte      |
	| Int64Arithmetic   | 10                   | 5           | long       |
	| Int64Arithmetic   | -10                  | -5          | long       |
	| Int64Arithmetic   | 10                   | -5          | long       |
	| Int64Arithmetic   | -10                  | 5           | long       |
	| Int64Arithmetic   | 0                    | 5           | long       |
	| Int64Arithmetic   | 0                    | -5          | long       |
	| Int64Arithmetic   | 9223372036854775807  | 1           | long       |
	| Int64Arithmetic   | -9223372036854775808 | 1           | long       |
	| Int64Arithmetic   | 9223372036854775807  | -1          | long       |
	| Int64Arithmetic   | -9223372036854775808 | -1          | long       |
	| Int64Arithmetic   | 2147483647           | 4294967296  | long       |
	| Int64Arithmetic   | -2147483648          | 4294967296  | long       |
	| Int64Arithmetic   | 2147483647           | -4294967296 | long       |
	| Int64Arithmetic   | -2147483648          | -4294967296 | long       |
	| UInt32Arithmetic  | 10                   | 5           | uint       |
	| UInt32Arithmetic  | 10                   | 0           | uint       |
	| UInt32Arithmetic  | 0                    | 5           | uint       |
	| UInt32Arithmetic  | 2147483647           | 1           | uint       |
	| UInt32Arithmetic  | 4294967295           | 1           | uint       |
	| UInt32Arithmetic  | 32767                | 65536       | uint       |
	| UInt16Arithmetic  | 10                   | 5           | ushort     |
	| UInt16Arithmetic  | 10                   | 0           | ushort     |
	| UInt16Arithmetic  | 0                    | 5           | ushort     |
	| UInt16Arithmetic  | 32767                | 1           | ushort     |
	| UInt16Arithmetic  | 65535                | 1           | ushort     |
	| UInt16Arithmetic  | 127                  | 256         | ushort     |
	| UInt8Arithmetic   | 10                   | 5           | byte       |
	| UInt8Arithmetic   | 10                   | 0           | byte       |
	| UInt8Arithmetic   | 0                    | 5           | byte       |
	| UInt8Arithmetic   | 127                  | 1           | byte       |
	| UInt8Arithmetic   | 255                  | 1           | byte       |
	| UInt8Arithmetic   | 7                    | 16          | byte       |
	| UInt64Arithmetic  | 10                   | 5           | ulong      |
	| UInt64Arithmetic  | 10                   | 0           | ulong      |
	| UInt64Arithmetic  | 0                    | 5           | ulong      |
	| UInt64Arithmetic  | 9223372036854775807  | 1           | ulong      |
	| UInt64Arithmetic  | 18446744073709551615 | 1           | ulong      |
	| UInt64Arithmetic  | 2147483647           | 4294967296  | ulong      |

	| BigIntArithmetic  | 10                   | 5           | BigInteger |
	| BigIntArithmetic  | -10                  | -5          | BigInteger |
	| BigIntArithmetic  | 10                   | -5          | BigInteger |
	| BigIntArithmetic  | -10                  | 5           | BigInteger |
	| BigIntArithmetic  | 0                    | 5           | BigInteger |
	| BigIntArithmetic  | 0                    | -5          | BigInteger |
	| BigIntArithmetic  | 92233720368547758070  | 1           | BigInteger |
	| BigIntArithmetic  | -92233720368547758080 | 1           | BigInteger |
	| BigIntArithmetic  | 92233720368547758070  | -1          | BigInteger |
	| BigIntArithmetic  | -92233720368547758080 | -1          | BigInteger |
	| BigIntArithmetic  | 2147483647           | 4294967296  | BigInteger |
	| BigIntArithmetic  | -2147483648          | 4294967296  | BigInteger |
	| BigIntArithmetic  | 2147483647           | -4294967296 | BigInteger |
	| BigIntArithmetic  | -2147483648          | -4294967296 | BigInteger |
	

Scenario: Remainder
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>'  are remaindered
	Then The integer result is the same as C# would calculate
Examples:
	| TC                | a                    | b           | c          |
	| Int32Arithmetic   | 10                   | 5           | int        |
	| Int32Arithmetic   | -10                  | -5          | int        |
	| Int32Arithmetic   | 10                   | -5          | int        |
	| Int32Arithmetic   | -10                  | 5           | int        |
	| Int32Arithmetic   | 0                    | 5           | int        |
	| Int32Arithmetic   | 0                    | -5          | int        |
	| Int32Arithmetic   | 2147483647           | 1           | int        |
	| Int32Arithmetic   | -2147483647          | -1          | int        |
	| Int32Arithmetic   | 2147483647           | -1          | int        |
	| Int32Arithmetic   | -2147483648          | 1           | int        |
	| Int32Arithmetic   | 32767                | 65536       | int        |
	| Int32Arithmetic   | -32768               | 65536       | int        |
	| Int32Arithmetic   | 32767                | -65536      | int        |
	| Int32Arithmetic   | -32768               | -65536      | int        |
	| Int16Arithmetic   | 10                   | 5           | short      |
	| Int16Arithmetic   | -10                  | -5          | short      |
	| Int16Arithmetic   | 10                   | -5          | short      |
	| Int16Arithmetic   | -10                  | 5           | short      |
	| Int16Arithmetic   | 0                    | 5           | short      |
	| Int16Arithmetic   | 0                    | -5          | short      |
	| Int16Arithmetic   | 32767                | 1           | short      |
	| Int16Arithmetic   | -32768               | -1          | short      |
	| Int16Arithmetic   | 32767                | -1          | short      |
	| Int16Arithmetic   | -32768               | 1           | short      |
	| Int16Arithmetic   | 127                  | 256         | short      |
	| Int16Arithmetic   | -128                 | 256         | short      |
	| Int16Arithmetic   | 127                  | -256        | short      |
	| Int16Arithmetic   | -128                 | -256        | short      |
	| Int8Arithmetic    | 10                   | 5           | sbyte      |
	| Int8Arithmetic    | -10                  | -5          | sbyte      |
	| Int8Arithmetic    | 10                   | -5          | sbyte      |
	| Int8Arithmetic    | -10                  | 5           | sbyte      |
	| Int8Arithmetic    | 0                    | 5           | sbyte      |
	| Int8Arithmetic    | 0                    | -5          | sbyte      |
	| Int8Arithmetic    | 127                  | 1           | sbyte      |
	| Int8Arithmetic    | -128                 | -1          | sbyte      |
	| Int8Arithmetic    | 127                  | -1          | sbyte      |
	| Int8Arithmetic    | -128                 | 1           | sbyte      |
	| Int8Arithmetic    | 7                    | 16          | sbyte      |
	| Int8Arithmetic    | -8                   | 16          | sbyte      |
	| Int8Arithmetic    | 7                    | -16         | sbyte      |
	| Int8Arithmetic    | -8                   | -16         | sbyte      |
	| Int64Arithmetic   | 10                   | 5           | long       |
	| Int64Arithmetic   | -10                  | -5          | long       |
	| Int64Arithmetic   | 10                   | -5          | long       |
	| Int64Arithmetic   | -10                  | 5           | long       |
	| Int64Arithmetic   | 0                    | 5           | long       |
	| Int64Arithmetic   | 0                    | -5          | long       |
	| Int64Arithmetic   | 9223372036854775807  | 1           | long       |
	| Int64Arithmetic   | -9223372036854775807 | -1          | long       |
	| Int64Arithmetic   | 9223372036854775807  | -1          | long       |
	| Int64Arithmetic   | -9223372036854775808 | 1           | long       |
	| Int64Arithmetic   | 2147483647           | 4294967296  | long       |
	| Int64Arithmetic   | -2147483648          | 4294967296  | long       |
	| Int64Arithmetic   | 2147483647           | -4294967296 | long       |
	| Int64Arithmetic   | -2147483648          | -4294967296 | long       |
	| UInt32Arithmetic  | 10                   | 5           | uint       |
	| UInt32Arithmetic  | 10                   | 0           | uint       |
	| UInt32Arithmetic  | 0                    | 5           | uint       |
	| UInt32Arithmetic  | 2147483647           | 1           | uint       |
	| UInt32Arithmetic  | 4294967295           | 1           | uint       |
	| UInt32Arithmetic  | 32767                | 65536       | uint       |
	| UInt16Arithmetic  | 10                   | 5           | ushort     |
	| UInt16Arithmetic  | 10                   | 0           | ushort     |
	| UInt16Arithmetic  | 0                    | 5           | ushort     |
	| UInt16Arithmetic  | 32767                | 1           | ushort     |
	| UInt16Arithmetic  | 65535                | 1           | ushort     |
	| UInt16Arithmetic  | 127                  | 256         | ushort     |
	| UInt8Arithmetic   | 10                   | 5           | byte       |
	| UInt8Arithmetic   | 10                   | 0           | byte       |
	| UInt8Arithmetic   | 0                    | 5           | byte       |
	| UInt8Arithmetic   | 127                  | 1           | byte       |
	| UInt8Arithmetic   | 255                  | 1           | byte       |
	| UInt8Arithmetic   | 7                    | 16          | byte       |
	| UInt64Arithmetic  | 10                   | 5           | ulong      |
	| UInt64Arithmetic  | 10                   | 0           | ulong      |
	| UInt64Arithmetic  | 0                    | 5           | ulong      |
	| UInt64Arithmetic  | 9223372036854775807  | 1           | ulong      |
	| UInt64Arithmetic  | 18446744073709551615 | 1           | ulong      |
	| UInt64Arithmetic  | 2147483647           | 4294967296  | ulong      |
	| BigIntArithmetic  | 10                   | 5           | BigInteger |
	| BigIntArithmetic  | -10                  | -5          | BigInteger |
	| BigIntArithmetic  | 10                   | -5          | BigInteger |
	| BigIntArithmetic  | -10                  | 5           | BigInteger |
	| BigIntArithmetic  | 0                    | 5           | BigInteger |
	| BigIntArithmetic  | 0                    | -5          | BigInteger |
	| BigIntArithmetic  | 92233720368547758070  | 1           | BigInteger |
	| BigIntArithmetic  | -92233720368547758080 | -1          | BigInteger |
	| BigIntArithmetic  | 92233720368547758070  | -1          | BigInteger |
	| BigIntArithmetic  | -92233720368547758080 | 1           | BigInteger |
	| BigIntArithmetic  | 2147483647           | 4294967296  | BigInteger |
	| BigIntArithmetic  | -2147483648          | 4294967296  | BigInteger |
	| BigIntArithmetic  | 2147483647           | -4294967296 | BigInteger |
	| BigIntArithmetic  | -2147483648          | -4294967296 | BigInteger |



Scenario: And
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are bitwise anded
	Then The integer result is the same as C# would calculate
Examples:
	| TC             | a                    | b                    | c          |
	| Int32Bitwise   | 2147483647           | 2147483647           | int        |
	| Int32Bitwise   | -2147483647          | -2147483647          | int        |
	| Int32Bitwise   | -2147483647          | 0                    | int        |
	| Int32Bitwise   | 2147483647           | 0                    | int        |
	| Int32Bitwise   | 2147483647           | 65536                | int        |
	| Int32Bitwise   | 65536                | 2147483647           | int        |
	| Int16Bitwise   | 32767                | 32767                | short      |
	| Int16Bitwise   | -32767               | -32767               | short      |
	| Int16Bitwise   | -32767               | 0                    | short      |
	| Int16Bitwise   | 32767                | 0                    | short      |
	| Int16Bitwise   | 32767                | 256                  | short      |
	| Int16Bitwise   | 256                  | 32767                | short      |
	| Int8Bitwise    | 127                  | 127                  | sbyte      |
	| Int8Bitwise    | -127                 | -127                 | sbyte      |
	| Int8Bitwise    | -127                 | 0                    | sbyte      |
	| Int8Bitwise    | 127                  | 0                    | sbyte      |
	| Int8Bitwise    | 127                  | 16                   | sbyte      |
	| Int8Bitwise    | 16                   | 127                  | sbyte      |
	| Int64Bitwise   | 9223372036854775807  | 9223372036854775807  | long       |
	| Int64Bitwise   | -9223372036854775807 | -9223372036854775807 | long       |
	| Int64Bitwise   | -9223372036854775807 | 0                    | long       |
	| Int64Bitwise   | 9223372036854775807  | 0                    | long       |
	| Int64Bitwise   | 9223372036854775807  | 4294967296           | long       |
	| Int64Bitwise   | 4294967296           | 9223372036854775807  | long       |
	| UInt32Bitwise  | 2147483647           | 2147483647           | uint       |
	| UInt32Bitwise  | 2147483647           | 0                    | uint       |
	| UInt32Bitwise  | 2147483647           | 65536                | uint       |
	| UInt32Bitwise  | 65536                | 2147483647           | uint       |
	| UInt16Bitwise  | 32767                | 32767                | ushort     |
	| UInt16Bitwise  | 32767                | 0                    | ushort     |
	| UInt16Bitwise  | 32767                | 256                  | ushort     |
	| UInt16Bitwise  | 256                  | 32767                | ushort     |
	| UInt8Bitwise   | 127                  | 127                  | byte       |
	| UInt8Bitwise   | 127                  | 0                    | byte       |
	| UInt8Bitwise   | 127                  | 16                   | byte       |
	| UInt8Bitwise   | 16                   | 127                  | byte       |
	| UInt64Bitwise  | 9223372036854775807  | 9223372036854775807  | ulong      |
	| UInt64Bitwise  | 9223372036854775807  | 0                    | ulong      |
	| UInt64Bitwise  | 9223372036854775807  | 4294967296           | ulong      |
	| UInt64Bitwise  | 4294967296           | 9223372036854775807  | ulong      |
	| BoolBitwise    | true                 | false                | bool       |
	| BoolBitwise    | false                | false                | bool       |
	| BoolBitwise    | false                | true                 | bool       |
	| BoolBitwise    | true                 | true                 | bool       |
	
	| BigIntBitwise  | 92233720368547758070  | 92233720368547758070  | BigInteger |
	| BigIntBitwise  | -92233720368547758070 | -92233720368547758070 | BigInteger |
	| BigIntBitwise  | -92233720368547758070 | 0                    | BigInteger |
	| BigIntBitwise  | 92233720368547758070  | 0                    | BigInteger |
	| BigIntBitwise  | 92233720368547758070  | 4294967296           | BigInteger |
	| BigIntBitwise  | 4294967296           | 92233720368547758070  | BigInteger |



Scenario: Or
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are bitwise ored
	Then The integer result is the same as C# would calculate
Examples:
	| TC             | a                    | b                    | c          |
	| Int32Bitwise   | 2147483647           | 2147483647           | int        |
	| Int32Bitwise   | -2147483647          | -2147483647          | int        |
	| Int32Bitwise   | -2147483647          | 0                    | int        |
	| Int32Bitwise   | 2147483647           | 0                    | int        |
	| Int32Bitwise   | 2147483647           | 65536                | int        |
	| Int32Bitwise   | 65536                | 2147483647           | int        |
	| Int16Bitwise   | 32767                | 32767                | short      |
	| Int16Bitwise   | -32767               | -32767               | short      |
	| Int16Bitwise   | -32767               | 0                    | short      |
	| Int16Bitwise   | 32767                | 0                    | short      |
	| Int16Bitwise   | 32767                | 256                  | short      |
	| Int16Bitwise   | 256                  | 32767                | short      |
	| Int8Bitwise    | 127                  | 127                  | sbyte      |
	| Int8Bitwise    | -127                 | -127                 | sbyte      |
	| Int8Bitwise    | -127                 | 0                    | sbyte      |
	| Int8Bitwise    | 127                  | 0                    | sbyte      |
	| Int8Bitwise    | 127                  | 16                   | sbyte      |
	| Int8Bitwise    | 16                   | 127                  | sbyte      |
	| Int64Bitwise   | 9223372036854775807  | 9223372036854775807  | long       |
	| Int64Bitwise   | -9223372036854775807 | -9223372036854775807 | long       |
	| Int64Bitwise   | -9223372036854775807 | 0                    | long       |
	| Int64Bitwise   | 9223372036854775807  | 0                    | long       |
	| Int64Bitwise   | 9223372036854775807  | 4294967296           | long       |
	| Int64Bitwise   | 4294967296           | 9223372036854775807  | long       |
	| UInt32Bitwise  | 2147483647           | 2147483647           | uint       |
	| UInt32Bitwise  | 2147483647           | 0                    | uint       |
	| UInt32Bitwise  | 2147483647           | 65536                | uint       |
	| UInt32Bitwise  | 65536                | 2147483647           | uint       |
	| UInt16Bitwise  | 32767                | 32767                | ushort     |
	| UInt16Bitwise  | 32767                | 0                    | ushort     |
	| UInt16Bitwise  | 32767                | 256                  | ushort     |
	| UInt16Bitwise  | 256                  | 32767                | ushort     |
	| UInt8Bitwise   | 127                  | 127                  | byte       |
	| UInt8Bitwise   | 127                  | 0                    | byte       |
	| UInt8Bitwise   | 127                  | 16                   | byte       |
	| UInt8Bitwise   | 16                   | 127                  | byte       |
	| UInt64Bitwise  | 9223372036854775807  | 9223372036854775807  | ulong      |
	| UInt64Bitwise  | 9223372036854775807  | 0                    | ulong      |
	| UInt64Bitwise  | 9223372036854775807  | 4294967296           | ulong      |
	| UInt64Bitwise  | 4294967296           | 9223372036854775807  | ulong      |
	| BoolBitwise    | true                 | false                | bool       |
	| BoolBitwise    | false                | false                | bool       |
	| BoolBitwise    | false                | true                 | bool       |
	| BoolBitwise    | true                 | true                 | bool       |

	| BigIntBitwise  | 92233720368547758070  | 92233720368547758070  | BigInteger |
	| BigIntBitwise  | -92233720368547758070 | -92233720368547758070 | BigInteger |
	| BigIntBitwise  | -92233720368547758070 | 0                    | BigInteger |
	| BigIntBitwise  | 92233720368547758070  | 0                    | BigInteger |
	| BigIntBitwise  | 92233720368547758070  | 4294967296           | BigInteger |
	| BigIntBitwise  | 4294967296           | 92233720368547758070  | BigInteger |
	




Scenario: Xor
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are bitwise xord
	Then The integer result is the same as C# would calculate
Examples:
	| TC             | a                    | b                    | c          |
	| Int32Bitwise   | 2147483647           | 2147483647           | int        |
	| Int32Bitwise   | -2147483647          | -2147483647          | int        |
	| Int32Bitwise   | -2147483647          | 0                    | int        |
	| Int32Bitwise   | 2147483647           | 0                    | int        |
	| Int32Bitwise   | 2147483647           | 65536                | int        |
	| Int32Bitwise   | 65536                | 2147483647           | int        |
	| Int16Bitwise   | 32767                | 32767                | short      |
	| Int16Bitwise   | -32767               | -32767               | short      |
	| Int16Bitwise   | -32767               | 0                    | short      |
	| Int16Bitwise   | 32767                | 0                    | short      |
	| Int16Bitwise   | 32767                | 256                  | short      |
	| Int16Bitwise   | 256                  | 32767                | short      |
	| Int8Bitwise    | 127                  | 127                  | sbyte      |
	| Int8Bitwise    | -127                 | -127                 | sbyte      |
	| Int8Bitwise    | -127                 | 0                    | sbyte      |
	| Int8Bitwise    | 127                  | 0                    | sbyte      |
	| Int8Bitwise    | 127                  | 16                   | sbyte      |
	| Int8Bitwise    | 16                   | 127                  | sbyte      |
	| Int64Bitwise   | 9223372036854775807  | 9223372036854775807  | long       |
	| Int64Bitwise   | -9223372036854775807 | -9223372036854775807 | long       |
	| Int64Bitwise   | -9223372036854775807 | 0                    | long       |
	| Int64Bitwise   | 9223372036854775807  | 0                    | long       |
	| Int64Bitwise   | 9223372036854775807  | 4294967296           | long       |
	| Int64Bitwise   | 4294967296           | 9223372036854775807  | long       |
	| UInt32Bitwise  | 2147483647           | 2147483647           | uint       |
	| UInt32Bitwise  | 2147483647           | 0                    | uint       |
	| UInt32Bitwise  | 2147483647           | 65536                | uint       |
	| UInt32Bitwise  | 65536                | 2147483647           | uint       |
	| UInt16Bitwise  | 32767                | 32767                | ushort     |
	| UInt16Bitwise  | 32767                | 0                    | ushort     |
	| UInt16Bitwise  | 32767                | 256                  | ushort     |
	| UInt16Bitwise  | 256                  | 32767                | ushort     |
	| UInt8Bitwise   | 127                  | 127                  | byte       |
	| UInt8Bitwise   | 127                  | 0                    | byte       |
	| UInt8Bitwise   | 127                  | 16                   | byte       |
	| UInt8Bitwise   | 16                   | 127                  | byte       |
	| UInt64Bitwise  | 9223372036854775807  | 9223372036854775807  | ulong      |
	| UInt64Bitwise  | 9223372036854775807  | 0                    | ulong      |
	| UInt64Bitwise  | 9223372036854775807  | 4294967296           | ulong      |
	| UInt64Bitwise  | 4294967296           | 9223372036854775807  | ulong      |
	| BoolBitwise    | true                 | false                | bool       |
	| BoolBitwise    | false                | false                | bool       |
	| BoolBitwise    | false                | true                 | bool       |
	| BoolBitwise    | true                 | true                 | bool       |
	| BigIntBitwise  | 92233720368547758070  | 92233720368547758070  | BigInteger |
	| BigIntBitwise  | -92233720368547758070 | -92233720368547758070 | BigInteger |
	| BigIntBitwise  | -92233720368547758070 | 0                    | BigInteger |
	| BigIntBitwise  | 92233720368547758070  | 0                    | BigInteger |
	| BigIntBitwise  | 92233720368547758070  | 4294967296           | BigInteger |
	| BigIntBitwise  | 4294967296           | 92233720368547758070  | BigInteger |





Scenario: Shl
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are left bitshifted
	Then The integer result is the same as C# would calculate
Examples:
	| TC            | a                    | b  | c      |
	| Int32Bitwise  | 0                    | 0  | int    |
	| Int32Bitwise  | 1                    | 1  | int    |
	| Int32Bitwise  | -1                   | 1  | int    |
	| Int32Bitwise  | 1                    | 31 | int    |
	| Int32Bitwise  | -1                   | 31 | int    |
	| Int32Bitwise  | 0                    | 1  | int    |
	| Int32Bitwise  | 2147483647           | 1  | int    |
	| Int32Bitwise  | -2147483648          | 1  | int    |
	| Int32Bitwise  | 2147483647           | 31 | int    |
	| Int32Bitwise  | -2147483648          | 31 | int    |
	| Int32Bitwise  | 32767                | 16 | int    |
	| Int32Bitwise  | -32768               | 16 | int    |
	| Int16Bitwise  | 0                    | 0  | short  |
	| Int16Bitwise  | 1                    | 1  | short  |
	| Int16Bitwise  | -1                   | 1  | short  |
	| Int16Bitwise  | 1                    | 15 | short  |
	| Int16Bitwise  | -1                   | 15 | short  |
	| Int16Bitwise  | 0                    | 1  | short  |
	| Int16Bitwise  | 32767                | 1  | short  |
	| Int16Bitwise  | -32768               | 1  | short  |
	| Int16Bitwise  | 32767                | 15 | short  |
	| Int16Bitwise  | -32768               | 15 | short  |
	| Int16Bitwise  | 127                  | 8  | short  |
	| Int16Bitwise  | -128                 | 8  | short  |
	| Int8Bitwise   | 0                    | 0  | sbyte  |
	| Int8Bitwise   | 1                    | 1  | sbyte  |
	| Int8Bitwise   | -1                   | 1  | sbyte  |
	| Int8Bitwise   | 1                    | 7  | sbyte  |
	| Int8Bitwise   | -1                   | 7  | sbyte  |
	| Int8Bitwise   | 0                    | 1  | sbyte  |
	| Int8Bitwise   | 127                  | 1  | sbyte  |
	| Int8Bitwise   | -128                 | 1  | sbyte  |
	| Int8Bitwise   | 127                  | 7  | sbyte  |
	| Int8Bitwise   | -128                 | 7  | sbyte  |
	| Int8Bitwise   | 7                    | 4  | sbyte  |
	| Int8Bitwise   | -8                   | 4  | sbyte  |
	| Int64Bitwise  | 0                    | 0  | long   |
	| Int64Bitwise  | 1                    | 1  | long   |
	| Int64Bitwise  | -1                   | 1  | long   |
	| Int64Bitwise  | 1                    | 63 | long   |
	| Int64Bitwise  | -1                   | 63 | long   |
	| Int64Bitwise  | 0                    | 1  | long   |
	| Int64Bitwise  | 9223372036854775807  | 1  | long   |
	| Int64Bitwise  | -9223372036854775808 | 1  | long   |
	| Int64Bitwise  | 9223372036854775807  | 63 | long   |
	| Int64Bitwise  | -9223372036854775808 | 63 | long   |
	| Int64Bitwise  | 2147483647           | 32 | long   |
	| Int64Bitwise  | -2147483648          | 32 | long   |
	| UInt32Bitwise | 0                    | 0  | uint   |
	| UInt32Bitwise | 1                    | 1  | uint   |
	| UInt32Bitwise | 1                    | 31 | uint   |
	| UInt32Bitwise | 0                    | 1  | uint   |
	| UInt32Bitwise | 2147483647           | 1  | uint   |
	| UInt32Bitwise | 4294967295           | 1  | uint   |
	| UInt32Bitwise | 32767                | 16 | uint   |
	| UInt16Bitwise | 0                    | 0  | ushort |
	| UInt16Bitwise | 1                    | 1  | ushort |
	| UInt16Bitwise | 1                    | 15 | ushort |
	| UInt16Bitwise | 0                    | 1  | ushort |
	| UInt16Bitwise | 32767                | 1  | ushort |
	| UInt16Bitwise | 65535                | 1  | ushort |
	| UInt16Bitwise | 127                  | 8  | ushort |
	| UInt8Bitwise  | 0                    | 0  | byte   |
	| UInt8Bitwise  | 1                    | 1  | byte   |
	| UInt8Bitwise  | 1                    | 7  | byte   |
	| UInt8Bitwise  | 0                    | 1  | byte   |
	| UInt8Bitwise  | 127                  | 1  | byte   |
	| UInt8Bitwise  | 255                  | 1  | byte   |
	| UInt8Bitwise  | 7                    | 4  | byte   |
	| UInt64Bitwise | 0                    | 0  | ulong  |
	| UInt64Bitwise | 1                    | 1  | ulong  |
	| UInt64Bitwise | 1                    | 63 | ulong  |
	| UInt64Bitwise | 0                    | 1  | ulong  |
	| UInt64Bitwise | 9223372036854775807  | 1  | ulong  |
	| UInt64Bitwise | 18446744073709551615 | 1  | ulong  |
	| UInt64Bitwise | 2147483647           | 32 | ulong  |






Scenario: Shr
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are right bitshifted
	Then The integer result is the same as C# would calculate
Examples:
	| TC            | a                    | b  | c      |
	| Int32Bitwise  | 0                    | 0  | int    |
	| Int32Bitwise  | 1                    | 1  | int    |
	| Int32Bitwise  | -1                   | 1  | int    |
	| Int32Bitwise  | 1                    | 31 | int    |
	| Int32Bitwise  | -1                   | 31 | int    |
	| Int32Bitwise  | 0                    | 1  | int    |
	| Int32Bitwise  | 2147483647           | 1  | int    |
	| Int32Bitwise  | -2147483648          | 1  | int    |
	| Int32Bitwise  | 2147483647           | 31 | int    |
	| Int32Bitwise  | -2147483648          | 31 | int    |
	| Int32Bitwise  | 32767                | 16 | int    |
	| Int32Bitwise  | -32768               | 16 | int    |
	| Int16Bitwise  | 0                    | 0  | short  |
	| Int16Bitwise  | 1                    | 1  | short  |
	| Int16Bitwise  | -1                   | 1  | short  |
	| Int16Bitwise  | 1                    | 15 | short  |
	| Int16Bitwise  | -1                   | 15 | short  |
	| Int16Bitwise  | 0                    | 1  | short  |
	| Int16Bitwise  | 32767                | 1  | short  |
	| Int16Bitwise  | -32768               | 1  | short  |
	| Int16Bitwise  | 32767                | 15 | short  |
	| Int16Bitwise  | -32768               | 15 | short  |
	| Int16Bitwise  | 127                  | 8  | short  |
	| Int16Bitwise  | -128                 | 8  | short  |
	| Int8Bitwise   | 0                    | 0  | sbyte  |
	| Int8Bitwise   | 1                    | 1  | sbyte  |
	| Int8Bitwise   | -1                   | 1  | sbyte  |
	| Int8Bitwise   | 1                    | 7  | sbyte  |
	| Int8Bitwise   | -1                   | 7  | sbyte  |
	| Int8Bitwise   | 0                    | 1  | sbyte  |
	| Int8Bitwise   | 127                  | 1  | sbyte  |
	| Int8Bitwise   | -128                 | 1  | sbyte  |
	| Int8Bitwise   | 127                  | 7  | sbyte  |
	| Int8Bitwise   | -128                 | 7  | sbyte  |
	| Int8Bitwise   | 7                    | 4  | sbyte  |
	| Int8Bitwise   | -8                   | 4  | sbyte  |
	| Int64Bitwise  | 0                    | 0  | long   |
	| Int64Bitwise  | 1                    | 1  | long   |
	| Int64Bitwise  | -1                   | 1  | long   |
	| Int64Bitwise  | 1                    | 63 | long   |
	| Int64Bitwise  | -1                   | 63 | long   |
	| Int64Bitwise  | 0                    | 1  | long   |
	| Int64Bitwise  | 9223372036854775807  | 1  | long   |
	| Int64Bitwise  | -9223372036854775808 | 1  | long   |
	| Int64Bitwise  | 9223372036854775807  | 63 | long   |
	| Int64Bitwise  | -9223372036854775808 | 63 | long   |
	| Int64Bitwise  | 2147483647           | 32 | long   |
	| Int64Bitwise  | -2147483648          | 32 | long   |
	| UInt32Bitwise | 0                    | 0  | uint   |
	| UInt32Bitwise | 1                    | 1  | uint   |
	| UInt32Bitwise | 1                    | 31 | uint   |
	| UInt32Bitwise | 0                    | 1  | uint   |
	| UInt32Bitwise | 2147483647           | 1  | uint   |
	| UInt32Bitwise | 4294967295           | 1  | uint   |
	| UInt32Bitwise | 32767                | 16 | uint   |
	| UInt16Bitwise | 0                    | 0  | ushort |
	| UInt16Bitwise | 1                    | 1  | ushort |
	| UInt16Bitwise | 1                    | 15 | ushort |
	| UInt16Bitwise | 0                    | 1  | ushort |
	| UInt16Bitwise | 32767                | 1  | ushort |
	| UInt16Bitwise | 65535                | 1  | ushort |
	| UInt16Bitwise | 127                  | 8  | ushort |
	| UInt8Bitwise  | 0                    | 0  | byte   |
	| UInt8Bitwise  | 1                    | 1  | byte   |
	| UInt8Bitwise  | 1                    | 7  | byte   |
	| UInt8Bitwise  | 0                    | 1  | byte   |
	| UInt8Bitwise  | 127                  | 1  | byte   |
	| UInt8Bitwise  | 255                  | 1  | byte   |
	| UInt8Bitwise  | 7                    | 4  | byte   |
	| UInt64Bitwise | 0                    | 0  | ulong  |
	| UInt64Bitwise | 1                    | 1  | ulong  |
	| UInt64Bitwise | 1                    | 63 | ulong  |
	| UInt64Bitwise | 0                    | 1  | ulong  |
	| UInt64Bitwise | 9223372036854775807  | 1  | ulong  |
	| UInt64Bitwise | 18446744073709551615 | 1  | ulong  |
	| UInt64Bitwise | 2147483647           | 32 | ulong  |

Scenario: And comparison
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two booleans '<a>' and '<b>' of integer type '<c>' are conditionally anded
	Then The integer result is the same as C# would calculate
Examples:
	| TC             | a     | b     | c    |
	| BoolConditions | true  | false | bool |
	| BoolConditions | false | false | bool |
	| BoolConditions | false | true  | bool |
	| BoolConditions | true  | true  | bool |

Scenario: Or comparison
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two booleans '<a>' and '<b>' of integer type '<c>' are conditionally ored
	Then The integer result is the same as C# would calculate
Examples:
	| TC             | a     | b     | c    |
	| BoolConditions | true  | false | bool |
	| BoolConditions | false | false | bool |
	| BoolConditions | false | true  | bool |
	| BoolConditions | true  | true  | bool |

	

Scenario: Equals comparison
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are compared for equality
	Then The integer result is the same as C# would calculate
Examples:
	| TC                | a                    | b                    | c      |
	| Int32Conditions   | 0                    | 0                    | int    |
	| Int32Conditions   | 1                    | 1                    | int    |
	| Int32Conditions   | -1                   | -1                   | int    |
	| Int32Conditions   | 1                    | -1                   | int    |
	| Int32Conditions   | -1                   | 1                    | int    |
	| Int32Conditions   | 0                    | 1                    | int    |
	| Int32Conditions   | 1                    | 0                    | int    |
	| Int32Conditions   | 0                    | -1                   | int    |
	| Int32Conditions   | -1                   | 0                    | int    |
	| Int32Conditions   | 2147483647           | 1                    | int    |
	| Int32Conditions   | -2147483648          | 1                    | int    |
	| Int32Conditions   | 2147483647           | -1                   | int    |
	| Int32Conditions   | -2147483648          | -1                   | int    |
	| Int32Conditions   | 32767                | 65536                | int    |
	| Int32Conditions   | -32768               | 65536                | int    |
	| Int32Conditions   | 32767                | -65536               | int    |
	| Int32Conditions   | -32768               | -65536               | int    |
	| Int16Conditions   | 0                    | 0                    | short  |
	| Int16Conditions   | 1                    | 1                    | short  |
	| Int16Conditions   | -1                   | -1                   | short  |
	| Int16Conditions   | 1                    | -1                   | short  |
	| Int16Conditions   | -1                   | 1                    | short  |
	| Int16Conditions   | 0                    | 1                    | short  |
	| Int16Conditions   | 1                    | 0                    | short  |
	| Int16Conditions   | 0                    | -1                   | short  |
	| Int16Conditions   | -1                   | 0                    | short  |
	| Int16Conditions   | 32767                | 1                    | short  |
	| Int16Conditions   | -32768               | 1                    | short  |
	| Int16Conditions   | 32767                | -1                   | short  |
	| Int16Conditions   | -32768               | -1                   | short  |
	| Int16Conditions   | 127                  | 256                  | short  |
	| Int16Conditions   | -128                 | 256                  | short  |
	| Int16Conditions   | 127                  | -256                 | short  |
	| Int16Conditions   | -128                 | -256                 | short  |
	| Int8Conditions    | 0                    | 0                    | sbyte  |
	| Int8Conditions    | 1                    | 1                    | sbyte  |
	| Int8Conditions    | -1                   | -1                   | sbyte  |
	| Int8Conditions    | 1                    | -1                   | sbyte  |
	| Int8Conditions    | -1                   | 1                    | sbyte  |
	| Int8Conditions    | 0                    | 1                    | sbyte  |
	| Int8Conditions    | 1                    | 0                    | sbyte  |
	| Int8Conditions    | 0                    | -1                   | sbyte  |
	| Int8Conditions    | -1                   | 0                    | sbyte  |
	| Int8Conditions    | 127                  | 1                    | sbyte  |
	| Int8Conditions    | -128                 | 1                    | sbyte  |
	| Int8Conditions    | 127                  | -1                   | sbyte  |
	| Int8Conditions    | -128                 | -1                   | sbyte  |
	| Int8Conditions    | 7                    | 16                   | sbyte  |
	| Int8Conditions    | -8                   | 16                   | sbyte  |
	| Int8Conditions    | 7                    | -16                  | sbyte  |
	| Int8Conditions    | -8                   | -16                  | sbyte  |
	| Int64Conditions   | 0                    | 0                    | long   |
	| Int64Conditions   | 1                    | 1                    | long   |
	| Int64Conditions   | -1                   | -1                   | long   |
	| Int64Conditions   | 1                    | -1                   | long   |
	| Int64Conditions   | -1                   | 1                    | long   |
	| Int64Conditions   | 0                    | 1                    | long   |
	| Int64Conditions   | 1                    | 0                    | long   |
	| Int64Conditions   | 0                    | -1                   | long   |
	| Int64Conditions   | -1                   | 0                    | long   |
	| Int64Conditions   | 9223372036854775807  | 1                    | long   |
	| Int64Conditions   | -9223372036854775808 | 1                    | long   |
	| Int64Conditions   | 9223372036854775807  | -1                   | long   |
	| Int64Conditions   | -9223372036854775808 | -1                   | long   |
	| Int64Conditions   | 2147483647           | 9223372036854775807  | long   |
	| Int64Conditions   | -2147483648          | 9223372036854775807  | long   |
	| Int64Conditions   | 2147483647           | -9223372036854775808 | long   |
	| Int64Conditions   | -2147483648          | -9223372036854775808 | long   |
	| UInt32Conditions  | 0                    | 0                    | uint   |
	| UInt32Conditions  | 1                    | 1                    | uint   |
	| UInt32Conditions  | 1                    | 0                    | uint   |
	| UInt32Conditions  | 0                    | 1                    | uint   |
	| UInt32Conditions  | 2147483647           | 1                    | uint   |
	| UInt32Conditions  | 4294967295           | 1                    | uint   |
	| UInt32Conditions  | 32767                | 65536                | uint   |
	| UInt16Conditions  | 0                    | 0                    | ushort |
	| UInt16Conditions  | 1                    | 1                    | ushort |
	| UInt16Conditions  | 1                    | 0                    | ushort |
	| UInt16Conditions  | 0                    | 1                    | ushort |
	| UInt16Conditions  | 32767                | 1                    | ushort |
	| UInt16Conditions  | 65535                | 1                    | ushort |
	| UInt16Conditions  | 127                  | 256                  | ushort |
	| UInt8Conditions   | 0                    | 0                    | byte   |
	| UInt8Conditions   | 1                    | 1                    | byte   |
	| UInt8Conditions   | 1                    | 0                    | byte   |
	| UInt8Conditions   | 0                    | 1                    | byte   |
	| UInt8Conditions   | 127                  | 1                    | byte   |
	| UInt8Conditions   | 255                  | 1                    | byte   |
	| UInt8Conditions   | 7                    | 16                   | byte   |
	| UInt64Conditions  | 0                    | 0                    | ulong  |
	| UInt64Conditions  | 1                    | 1                    | ulong  |
	| UInt64Conditions  | 1                    | 0                    | ulong  |
	| UInt64Conditions  | 0                    | 1                    | ulong  |
	| UInt64Conditions  | 9223372036854775807  | 1                    | ulong  |
	| UInt64Conditions  | 18446744073709551615 | 1                    | ulong  |
	| UInt64Conditions  | 2147483647           | 9223372036854775807  | ulong  |
	| BoolConditions    | true                 | false                | bool   |
	| BoolConditions    | false                | false                | bool   |
	| BoolConditions    | false                | true                 | bool   |
	| BoolConditions    | true                 | true                 | bool   |


	| BigIntConditions  | 0                    | 0                    | BigInteger  |
	| BigIntConditions  | 1                    | 1                    | BigInteger  |
	| BigIntConditions  | -1                   | -1                   | BigInteger  |
	| BigIntConditions  | 1                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 1                    | BigInteger  |
	| BigIntConditions  | 0                    | 1                    | BigInteger  |
	| BigIntConditions  | 1                    | 0                    | BigInteger  |
	| BigIntConditions  | 0                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 0                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | 1                    | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | 1                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | -1                   | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | -1                   | BigInteger  |
	| BigIntConditions  | 2147483647           | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | -2147483648          | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | 2147483647           | -92233720368547758080 | BigInteger  |
	| BigIntConditions  | -2147483648          | -92233720368547758080 | BigInteger  |
	


Scenario: Not equals comparison
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are compared for inequality
	Then The integer result is the same as C# would calculate
Examples:
	| TC               | a                    | b                    | c      |
	| Int32Conditions  | 0                    | 0                    | int    |
	| Int32Conditions  | 1                    | 1                    | int    |
	| Int32Conditions  | -1                   | -1                   | int    |
	| Int32Conditions  | 1                    | -1                   | int    |
	| Int32Conditions  | -1                   | 1                    | int    |
	| Int32Conditions  | 0                    | 1                    | int    |
	| Int32Conditions  | 1                    | 0                    | int    |
	| Int32Conditions  | 0                    | -1                   | int    |
	| Int32Conditions  | -1                   | 0                    | int    |
	| Int32Conditions  | 2147483647           | 1                    | int    |
	| Int32Conditions  | -2147483648          | 1                    | int    |
	| Int32Conditions  | 2147483647           | -1                   | int    |
	| Int32Conditions  | -2147483648          | -1                   | int    |
	| Int32Conditions  | 32767                | 65536                | int    |
	| Int32Conditions  | -32768               | 65536                | int    |
	| Int32Conditions  | 32767                | -65536               | int    |
	| Int32Conditions  | -32768               | -65536               | int    |
	| Int16Conditions  | 0                    | 0                    | short  |
	| Int16Conditions  | 1                    | 1                    | short  |
	| Int16Conditions  | -1                   | -1                   | short  |
	| Int16Conditions  | 1                    | -1                   | short  |
	| Int16Conditions  | -1                   | 1                    | short  |
	| Int16Conditions  | 0                    | 1                    | short  |
	| Int16Conditions  | 1                    | 0                    | short  |
	| Int16Conditions  | 0                    | -1                   | short  |
	| Int16Conditions  | -1                   | 0                    | short  |
	| Int16Conditions  | 32767                | 1                    | short  |
	| Int16Conditions  | -32768               | 1                    | short  |
	| Int16Conditions  | 32767                | -1                   | short  |
	| Int16Conditions  | -32768               | -1                   | short  |
	| Int16Conditions  | 127                  | 256                  | short  |
	| Int16Conditions  | -128                 | 256                  | short  |
	| Int16Conditions  | 127                  | -256                 | short  |
	| Int16Conditions  | -128                 | -256                 | short  |
	| Int8Conditions   | 0                    | 0                    | sbyte  |
	| Int8Conditions   | 1                    | 1                    | sbyte  |
	| Int8Conditions   | -1                   | -1                   | sbyte  |
	| Int8Conditions   | 1                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 1                    | sbyte  |
	| Int8Conditions   | 0                    | 1                    | sbyte  |
	| Int8Conditions   | 1                    | 0                    | sbyte  |
	| Int8Conditions   | 0                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 0                    | sbyte  |
	| Int8Conditions   | 127                  | 1                    | sbyte  |
	| Int8Conditions   | -128                 | 1                    | sbyte  |
	| Int8Conditions   | 127                  | -1                   | sbyte  |
	| Int8Conditions   | -128                 | -1                   | sbyte  |
	| Int8Conditions   | 7                    | 16                   | sbyte  |
	| Int8Conditions   | -8                   | 16                   | sbyte  |
	| Int8Conditions   | 7                    | -16                  | sbyte  |
	| Int8Conditions   | -8                   | -16                  | sbyte  |
	| Int64Conditions  | 0                    | 0                    | long   |
	| Int64Conditions  | 1                    | 1                    | long   |
	| Int64Conditions  | -1                   | -1                   | long   |
	| Int64Conditions  | 1                    | -1                   | long   |
	| Int64Conditions  | -1                   | 1                    | long   |
	| Int64Conditions  | 0                    | 1                    | long   |
	| Int64Conditions  | 1                    | 0                    | long   |
	| Int64Conditions  | 0                    | -1                   | long   |
	| Int64Conditions  | -1                   | 0                    | long   |
	| Int64Conditions  | 9223372036854775807  | 1                    | long   |
	| Int64Conditions  | -9223372036854775808 | 1                    | long   |
	| Int64Conditions  | 9223372036854775807  | -1                   | long   |
	| Int64Conditions  | -9223372036854775808 | -1                   | long   |
	| Int64Conditions  | 2147483647           | 9223372036854775807  | long   |
	| Int64Conditions  | -2147483648          | 9223372036854775807  | long   |
	| Int64Conditions  | 2147483647           | -9223372036854775808 | long   |
	| Int64Conditions  | -2147483648          | -9223372036854775808 | long   |
	| UInt32Conditions | 0                    | 0                    | uint   |
	| UInt32Conditions | 1                    | 1                    | uint   |
	| UInt32Conditions | 1                    | 0                    | uint   |
	| UInt32Conditions | 0                    | 1                    | uint   |
	| UInt32Conditions | 2147483647           | 1                    | uint   |
	| UInt32Conditions | 4294967295           | 1                    | uint   |
	| UInt32Conditions | 32767                | 65536                | uint   |
	| UInt16Conditions | 0                    | 0                    | ushort |
	| UInt16Conditions | 1                    | 1                    | ushort |
	| UInt16Conditions | 1                    | 0                    | ushort |
	| UInt16Conditions | 0                    | 1                    | ushort |
	| UInt16Conditions | 32767                | 1                    | ushort |
	| UInt16Conditions | 65535                | 1                    | ushort |
	| UInt16Conditions | 127                  | 256                  | ushort |
	| UInt8Conditions  | 0                    | 0                    | byte   |
	| UInt8Conditions  | 1                    | 1                    | byte   |
	| UInt8Conditions  | 1                    | 0                    | byte   |
	| UInt8Conditions  | 0                    | 1                    | byte   |
	| UInt8Conditions  | 127                  | 1                    | byte   |
	| UInt8Conditions  | 255                  | 1                    | byte   |
	| UInt8Conditions  | 7                    | 16                   | byte   |
	| UInt64Conditions | 0                    | 0                    | ulong  |
	| UInt64Conditions | 1                    | 1                    | ulong  |
	| UInt64Conditions | 1                    | 0                    | ulong  |
	| UInt64Conditions | 0                    | 1                    | ulong  |
	| UInt64Conditions | 9223372036854775807  | 1                    | ulong  |
	| UInt64Conditions | 18446744073709551615 | 1                    | ulong  |
	| UInt64Conditions | 2147483647           | 9223372036854775807  | ulong  |
	| BoolConditions   | true                 | false                | bool   |
	| BoolConditions   | false                | false                | bool   |
	| BoolConditions   | false                | true                 | bool   |
	| BoolConditions   | true                 | true                 | bool   |
	| BigIntConditions  | 0                    | 0                    | BigInteger  |
	| BigIntConditions  | 1                    | 1                    | BigInteger  |
	| BigIntConditions  | -1                   | -1                   | BigInteger  |
	| BigIntConditions  | 1                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 1                    | BigInteger  |
	| BigIntConditions  | 0                    | 1                    | BigInteger  |
	| BigIntConditions  | 1                    | 0                    | BigInteger  |
	| BigIntConditions  | 0                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 0                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | 1                    | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | 1                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | -1                   | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | -1                   | BigInteger  |
	| BigIntConditions  | 2147483647           | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | -2147483648          | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | 2147483647           | -92233720368547758080 | BigInteger  |
	| BigIntConditions  | -2147483648          | -92233720368547758080 | BigInteger  |
	


Scenario: Greater comparison
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are compared for greater than
	Then The integer result is the same as C# would calculate
Examples:
	| TC               | a                    | b                    | c      |
	| Int32Conditions  | 0                    | 0                    | int    |
	| Int32Conditions  | 1                    | 1                    | int    |
	| Int32Conditions  | -1                   | -1                   | int    |
	| Int32Conditions  | 1                    | -1                   | int    |
	| Int32Conditions  | -1                   | 1                    | int    |
	| Int32Conditions  | 0                    | 1                    | int    |
	| Int32Conditions  | 1                    | 0                    | int    |
	| Int32Conditions  | 0                    | -1                   | int    |
	| Int32Conditions  | -1                   | 0                    | int    |
	| Int32Conditions  | 2147483647           | 1                    | int    |
	| Int32Conditions  | -2147483648          | 1                    | int    |
	| Int32Conditions  | 2147483647           | -1                   | int    |
	| Int32Conditions  | -2147483648          | -1                   | int    |
	| Int32Conditions  | 32767                | 65536                | int    |
	| Int32Conditions  | -32768               | 65536                | int    |
	| Int32Conditions  | 32767                | -65536               | int    |
	| Int32Conditions  | -32768               | -65536               | int    |
	| Int16Conditions  | 0                    | 0                    | short  |
	| Int16Conditions  | 1                    | 1                    | short  |
	| Int16Conditions  | -1                   | -1                   | short  |
	| Int16Conditions  | 1                    | -1                   | short  |
	| Int16Conditions  | -1                   | 1                    | short  |
	| Int16Conditions  | 0                    | 1                    | short  |
	| Int16Conditions  | 1                    | 0                    | short  |
	| Int16Conditions  | 0                    | -1                   | short  |
	| Int16Conditions  | -1                   | 0                    | short  |
	| Int16Conditions  | 32767                | 1                    | short  |
	| Int16Conditions  | -32768               | 1                    | short  |
	| Int16Conditions  | 32767                | -1                   | short  |
	| Int16Conditions  | -32768               | -1                   | short  |
	| Int16Conditions  | 127                  | 256                  | short  |
	| Int16Conditions  | -128                 | 256                  | short  |
	| Int16Conditions  | 127                  | -256                 | short  |
	| Int16Conditions  | -128                 | -256                 | short  |
	| Int8Conditions   | 0                    | 0                    | sbyte  |
	| Int8Conditions   | 1                    | 1                    | sbyte  |
	| Int8Conditions   | -1                   | -1                   | sbyte  |
	| Int8Conditions   | 1                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 1                    | sbyte  |
	| Int8Conditions   | 0                    | 1                    | sbyte  |
	| Int8Conditions   | 1                    | 0                    | sbyte  |
	| Int8Conditions   | 0                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 0                    | sbyte  |
	| Int8Conditions   | 127                  | 1                    | sbyte  |
	| Int8Conditions   | -128                 | 1                    | sbyte  |
	| Int8Conditions   | 127                  | -1                   | sbyte  |
	| Int8Conditions   | -128                 | -1                   | sbyte  |
	| Int8Conditions   | 7                    | 16                   | sbyte  |
	| Int8Conditions   | -8                   | 16                   | sbyte  |
	| Int8Conditions   | 7                    | -16                  | sbyte  |
	| Int8Conditions   | -8                   | -16                  | sbyte  |
	| Int64Conditions  | 0                    | 0                    | long   |
	| Int64Conditions  | 1                    | 1                    | long   |
	| Int64Conditions  | -1                   | -1                   | long   |
	| Int64Conditions  | 1                    | -1                   | long   |
	| Int64Conditions  | -1                   | 1                    | long   |
	| Int64Conditions  | 0                    | 1                    | long   |
	| Int64Conditions  | 1                    | 0                    | long   |
	| Int64Conditions  | 0                    | -1                   | long   |
	| Int64Conditions  | -1                   | 0                    | long   |
	| Int64Conditions  | 9223372036854775807  | 1                    | long   |
	| Int64Conditions  | -9223372036854775808 | 1                    | long   |
	| Int64Conditions  | 9223372036854775807  | -1                   | long   |
	| Int64Conditions  | -9223372036854775808 | -1                   | long   |
	| Int64Conditions  | 2147483647           | 9223372036854775807  | long   |
	| Int64Conditions  | -2147483648          | 9223372036854775807  | long   |
	| Int64Conditions  | 2147483647           | -9223372036854775808 | long   |
	| Int64Conditions  | -2147483648          | -9223372036854775808 | long   |
	| UInt32Conditions | 0                    | 0                    | uint   |
	| UInt32Conditions | 1                    | 1                    | uint   |
	| UInt32Conditions | 1                    | 0                    | uint   |
	| UInt32Conditions | 0                    | 1                    | uint   |
	| UInt32Conditions | 2147483647           | 1                    | uint   |
	| UInt32Conditions | 4294967295           | 1                    | uint   |
	| UInt32Conditions | 32767                | 65536                | uint   |
	| UInt16Conditions | 0                    | 0                    | ushort |
	| UInt16Conditions | 1                    | 1                    | ushort |
	| UInt16Conditions | 1                    | 0                    | ushort |
	| UInt16Conditions | 0                    | 1                    | ushort |
	| UInt16Conditions | 32767                | 1                    | ushort |
	| UInt16Conditions | 65535                | 1                    | ushort |
	| UInt16Conditions | 127                  | 256                  | ushort |
	| UInt8Conditions  | 0                    | 0                    | byte   |
	| UInt8Conditions  | 1                    | 1                    | byte   |
	| UInt8Conditions  | 1                    | 0                    | byte   |
	| UInt8Conditions  | 0                    | 1                    | byte   |
	| UInt8Conditions  | 127                  | 1                    | byte   |
	| UInt8Conditions  | 255                  | 1                    | byte   |
	| UInt8Conditions  | 7                    | 16                   | byte   |
	| UInt64Conditions | 0                    | 0                    | ulong  |
	| UInt64Conditions | 1                    | 1                    | ulong  |
	| UInt64Conditions | 1                    | 0                    | ulong  |
	| UInt64Conditions | 0                    | 1                    | ulong  |
	| UInt64Conditions | 9223372036854775807  | 1                    | ulong  |
	| UInt64Conditions | 18446744073709551615 | 1                    | ulong  |
	| UInt64Conditions | 2147483647           | 9223372036854775807  | ulong  |
	| BigIntConditions  | 0                    | 0                    | BigInteger  |
	| BigIntConditions  | 1                    | 1                    | BigInteger  |
	| BigIntConditions  | -1                   | -1                   | BigInteger  |
	| BigIntConditions  | 1                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 1                    | BigInteger  |
	| BigIntConditions  | 0                    | 1                    | BigInteger  |
	| BigIntConditions  | 1                    | 0                    | BigInteger  |
	| BigIntConditions  | 0                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 0                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | 1                    | BigInteger  |
	| BigIntConditions  | -92233720368547758080| 1                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | -1                   | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | -1                   | BigInteger  |
	| BigIntConditions  | 2147483647           | 92233720368547758070 | BigInteger  |
	| BigIntConditions  | -2147483648          | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | 2147483647           | -92233720368547758080 | BigInteger  |
	| BigIntConditions  | -2147483648          | -92233720368547758080 | BigInteger  |



Scenario: Less Than comparison
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are compared for less than
	Then The integer result is the same as C# would calculate
Examples:
	| TC               | a                    | b                    | c      |
	| Int32Conditions  | 0                    | 0                    | int    |
	| Int32Conditions  | 1                    | 1                    | int    |
	| Int32Conditions  | -1                   | -1                   | int    |
	| Int32Conditions  | 1                    | -1                   | int    |
	| Int32Conditions  | -1                   | 1                    | int    |
	| Int32Conditions  | 0                    | 1                    | int    |
	| Int32Conditions  | 1                    | 0                    | int    |
	| Int32Conditions  | 0                    | -1                   | int    |
	| Int32Conditions  | -1                   | 0                    | int    |
	| Int32Conditions  | 2147483647           | 1                    | int    |
	| Int32Conditions  | -2147483648          | 1                    | int    |
	| Int32Conditions  | 2147483647           | -1                   | int    |
	| Int32Conditions  | -2147483648          | -1                   | int    |
	| Int32Conditions  | 32767                | 65536                | int    |
	| Int32Conditions  | -32768               | 65536                | int    |
	| Int32Conditions  | 32767                | -65536               | int    |
	| Int32Conditions  | -32768               | -65536               | int    |
	| Int16Conditions  | 0                    | 0                    | short  |
	| Int16Conditions  | 1                    | 1                    | short  |
	| Int16Conditions  | -1                   | -1                   | short  |
	| Int16Conditions  | 1                    | -1                   | short  |
	| Int16Conditions  | -1                   | 1                    | short  |
	| Int16Conditions  | 0                    | 1                    | short  |
	| Int16Conditions  | 1                    | 0                    | short  |
	| Int16Conditions  | 0                    | -1                   | short  |
	| Int16Conditions  | -1                   | 0                    | short  |
	| Int16Conditions  | 32767                | 1                    | short  |
	| Int16Conditions  | -32768               | 1                    | short  |
	| Int16Conditions  | 32767                | -1                   | short  |
	| Int16Conditions  | -32768               | -1                   | short  |
	| Int16Conditions  | 127                  | 256                  | short  |
	| Int16Conditions  | -128                 | 256                  | short  |
	| Int16Conditions  | 127                  | -256                 | short  |
	| Int16Conditions  | -128                 | -256                 | short  |
	| Int8Conditions   | 0                    | 0                    | sbyte  |
	| Int8Conditions   | 1                    | 1                    | sbyte  |
	| Int8Conditions   | -1                   | -1                   | sbyte  |
	| Int8Conditions   | 1                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 1                    | sbyte  |
	| Int8Conditions   | 0                    | 1                    | sbyte  |
	| Int8Conditions   | 1                    | 0                    | sbyte  |
	| Int8Conditions   | 0                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 0                    | sbyte  |
	| Int8Conditions   | 127                  | 1                    | sbyte  |
	| Int8Conditions   | -128                 | 1                    | sbyte  |
	| Int8Conditions   | 127                  | -1                   | sbyte  |
	| Int8Conditions   | -128                 | -1                   | sbyte  |
	| Int8Conditions   | 7                    | 16                   | sbyte  |
	| Int8Conditions   | -8                   | 16                   | sbyte  |
	| Int8Conditions   | 7                    | -16                  | sbyte  |
	| Int8Conditions   | -8                   | -16                  | sbyte  |
	| Int64Conditions  | 0                    | 0                    | long   |
	| Int64Conditions  | 1                    | 1                    | long   |
	| Int64Conditions  | -1                   | -1                   | long   |
	| Int64Conditions  | 1                    | -1                   | long   |
	| Int64Conditions  | -1                   | 1                    | long   |
	| Int64Conditions  | 0                    | 1                    | long   |
	| Int64Conditions  | 1                    | 0                    | long   |
	| Int64Conditions  | 0                    | -1                   | long   |
	| Int64Conditions  | -1                   | 0                    | long   |
	| Int64Conditions  | 9223372036854775807  | 1                    | long   |
	| Int64Conditions  | -9223372036854775808 | 1                    | long   |
	| Int64Conditions  | 9223372036854775807  | -1                   | long   |
	| Int64Conditions  | -9223372036854775808 | -1                   | long   |
	| Int64Conditions  | 2147483647           | 9223372036854775807  | long   |
	| Int64Conditions  | -2147483648          | 9223372036854775807  | long   |
	| Int64Conditions  | 2147483647           | -9223372036854775808 | long   |
	| Int64Conditions  | -2147483648          | -9223372036854775808 | long   |
	| UInt32Conditions | 0                    | 0                    | uint   |
	| UInt32Conditions | 1                    | 1                    | uint   |
	| UInt32Conditions | 1                    | 0                    | uint   |
	| UInt32Conditions | 0                    | 1                    | uint   |
	| UInt32Conditions | 2147483647           | 1                    | uint   |
	| UInt32Conditions | 4294967295           | 1                    | uint   |
	| UInt32Conditions | 32767                | 65536                | uint   |
	| UInt16Conditions | 0                    | 0                    | ushort |
	| UInt16Conditions | 1                    | 1                    | ushort |
	| UInt16Conditions | 1                    | 0                    | ushort |
	| UInt16Conditions | 0                    | 1                    | ushort |
	| UInt16Conditions | 32767                | 1                    | ushort |
	| UInt16Conditions | 65535                | 1                    | ushort |
	| UInt16Conditions | 127                  | 256                  | ushort |
	| UInt8Conditions  | 0                    | 0                    | byte   |
	| UInt8Conditions  | 1                    | 1                    | byte   |
	| UInt8Conditions  | 1                    | 0                    | byte   |
	| UInt8Conditions  | 0                    | 1                    | byte   |
	| UInt8Conditions  | 127                  | 1                    | byte   |
	| UInt8Conditions  | 255                  | 1                    | byte   |
	| UInt8Conditions  | 7                    | 16                   | byte   |
	| UInt64Conditions | 0                    | 0                    | ulong  |
	| UInt64Conditions | 1                    | 1                    | ulong  |
	| UInt64Conditions | 1                    | 0                    | ulong  |
	| UInt64Conditions | 0                    | 1                    | ulong  |
	| UInt64Conditions | 9223372036854775807  | 1                    | ulong  |
	| UInt64Conditions | 18446744073709551615 | 1                    | ulong  |
	| UInt64Conditions | 2147483647           | 9223372036854775807  | ulong  |
	| BigIntConditions  | 0                    | 0                    | BigInteger  |
	| BigIntConditions  | 1                    | 1                    | BigInteger  |
	| BigIntConditions  | -1                   | -1                   | BigInteger  |
	| BigIntConditions  | 1                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 1                    | BigInteger  |
	| BigIntConditions  | 0                    | 1                    | BigInteger  |
	| BigIntConditions  | 1                    | 0                    | BigInteger  |
	| BigIntConditions  | 0                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 0                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | 1                    | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | 1                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | -1                   | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | -1                   | BigInteger  |
	| BigIntConditions  | 2147483647           | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | -2147483648          | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | 2147483647           | -92233720368547758080 | BigInteger  |
	| BigIntConditions  | -2147483648          | -92233720368547758080 | BigInteger  |
	

Scenario: Greater Than Or Equals comparison
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are compared for greater than orequals
	Then The integer result is the same as C# would calculate
Examples:
	| TC               | a                    | b                    | c      |
	| Int32Conditions  | 0                    | 0                    | int    |
	| Int32Conditions  | 1                    | 1                    | int    |
	| Int32Conditions  | -1                   | -1                   | int    |
	| Int32Conditions  | 1                    | -1                   | int    |
	| Int32Conditions  | -1                   | 1                    | int    |
	| Int32Conditions  | 0                    | 1                    | int    |
	| Int32Conditions  | 1                    | 0                    | int    |
	| Int32Conditions  | 0                    | -1                   | int    |
	| Int32Conditions  | -1                   | 0                    | int    |
	| Int32Conditions  | 2147483647           | 1                    | int    |
	| Int32Conditions  | -2147483648          | 1                    | int    |
	| Int32Conditions  | 2147483647           | -1                   | int    |
	| Int32Conditions  | -2147483648          | -1                   | int    |
	| Int32Conditions  | 32767                | 65536                | int    |
	| Int32Conditions  | -32768               | 65536                | int    |
	| Int32Conditions  | 32767                | -65536               | int    |
	| Int32Conditions  | -32768               | -65536               | int    |
	| Int16Conditions  | 0                    | 0                    | short  |
	| Int16Conditions  | 1                    | 1                    | short  |
	| Int16Conditions  | -1                   | -1                   | short  |
	| Int16Conditions  | 1                    | -1                   | short  |
	| Int16Conditions  | -1                   | 1                    | short  |
	| Int16Conditions  | 0                    | 1                    | short  |
	| Int16Conditions  | 1                    | 0                    | short  |
	| Int16Conditions  | 0                    | -1                   | short  |
	| Int16Conditions  | -1                   | 0                    | short  |
	| Int16Conditions  | 32767                | 1                    | short  |
	| Int16Conditions  | -32768               | 1                    | short  |
	| Int16Conditions  | 32767                | -1                   | short  |
	| Int16Conditions  | -32768               | -1                   | short  |
	| Int16Conditions  | 127                  | 256                  | short  |
	| Int16Conditions  | -128                 | 256                  | short  |
	| Int16Conditions  | 127                  | -256                 | short  |
	| Int16Conditions  | -128                 | -256                 | short  |
	| Int8Conditions   | 0                    | 0                    | sbyte  |
	| Int8Conditions   | 1                    | 1                    | sbyte  |
	| Int8Conditions   | -1                   | -1                   | sbyte  |
	| Int8Conditions   | 1                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 1                    | sbyte  |
	| Int8Conditions   | 0                    | 1                    | sbyte  |
	| Int8Conditions   | 1                    | 0                    | sbyte  |
	| Int8Conditions   | 0                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 0                    | sbyte  |
	| Int8Conditions   | 127                  | 1                    | sbyte  |
	| Int8Conditions   | -128                 | 1                    | sbyte  |
	| Int8Conditions   | 127                  | -1                   | sbyte  |
	| Int8Conditions   | -128                 | -1                   | sbyte  |
	| Int8Conditions   | 7                    | 16                   | sbyte  |
	| Int8Conditions   | -8                   | 16                   | sbyte  |
	| Int8Conditions   | 7                    | -16                  | sbyte  |
	| Int8Conditions   | -8                   | -16                  | sbyte  |
	| Int64Conditions  | 0                    | 0                    | long   |
	| Int64Conditions  | 1                    | 1                    | long   |
	| Int64Conditions  | -1                   | -1                   | long   |
	| Int64Conditions  | 1                    | -1                   | long   |
	| Int64Conditions  | -1                   | 1                    | long   |
	| Int64Conditions  | 0                    | 1                    | long   |
	| Int64Conditions  | 1                    | 0                    | long   |
	| Int64Conditions  | 0                    | -1                   | long   |
	| Int64Conditions  | -1                   | 0                    | long   |
	| Int64Conditions  | 9223372036854775807  | 1                    | long   |
	| Int64Conditions  | -9223372036854775808 | 1                    | long   |
	| Int64Conditions  | 9223372036854775807  | -1                   | long   |
	| Int64Conditions  | -9223372036854775808 | -1                   | long   |
	| Int64Conditions  | 2147483647           | 9223372036854775807  | long   |
	| Int64Conditions  | -2147483648          | 9223372036854775807  | long   |
	| Int64Conditions  | 2147483647           | -9223372036854775808 | long   |
	| Int64Conditions  | -2147483648          | -9223372036854775808 | long   |
	| UInt32Conditions | 0                    | 0                    | uint   |
	| UInt32Conditions | 1                    | 1                    | uint   |
	| UInt32Conditions | 1                    | 0                    | uint   |
	| UInt32Conditions | 0                    | 1                    | uint   |
	| UInt32Conditions | 2147483647           | 1                    | uint   |
	| UInt32Conditions | 4294967295           | 1                    | uint   |
	| UInt32Conditions | 32767                | 65536                | uint   |
	| UInt16Conditions | 0                    | 0                    | ushort |
	| UInt16Conditions | 1                    | 1                    | ushort |
	| UInt16Conditions | 1                    | 0                    | ushort |
	| UInt16Conditions | 0                    | 1                    | ushort |
	| UInt16Conditions | 32767                | 1                    | ushort |
	| UInt16Conditions | 65535                | 1                    | ushort |
	| UInt16Conditions | 127                  | 256                  | ushort |
	| UInt8Conditions  | 0                    | 0                    | byte   |
	| UInt8Conditions  | 1                    | 1                    | byte   |
	| UInt8Conditions  | 1                    | 0                    | byte   |
	| UInt8Conditions  | 0                    | 1                    | byte   |
	| UInt8Conditions  | 127                  | 1                    | byte   |
	| UInt8Conditions  | 255                  | 1                    | byte   |
	| UInt8Conditions  | 7                    | 16                   | byte   |
	| UInt64Conditions | 0                    | 0                    | ulong  |
	| UInt64Conditions | 1                    | 1                    | ulong  |
	| UInt64Conditions | 1                    | 0                    | ulong  |
	| UInt64Conditions | 0                    | 1                    | ulong  |
	| UInt64Conditions | 9223372036854775807  | 1                    | ulong  |
	| UInt64Conditions | 18446744073709551615 | 1                    | ulong  |
	| UInt64Conditions | 2147483647           | 9223372036854775807  | ulong  |
	| BigIntConditions  | 0                    | 0                    | BigInteger  |
	| BigIntConditions  | 1                    | 1                    | BigInteger  |
	| BigIntConditions  | -1                   | -1                   | BigInteger  |
	| BigIntConditions  | 1                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 1                    | BigInteger  |
	| BigIntConditions  | 0                    | 1                    | BigInteger  |
	| BigIntConditions  | 1                    | 0                    | BigInteger  |
	| BigIntConditions  | 0                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 0                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | 1                    | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | 1                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | -1                   | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | -1                   | BigInteger  |
	| BigIntConditions  | 2147483647           | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | -2147483648          | 92233720368547758070  | BigInteger  |
	| BigIntConditions  | 2147483647           | -92233720368547758080 | BigInteger  |
	| BigIntConditions  | -2147483648          | -92233720368547758080 | BigInteger  |
	

Scenario: Less Than Or Equals comparison
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are compared for less than or equals
	Then The integer result is the same as C# would calculate
Examples:
	| TC               | a                    | b                    | c      |
	| Int32Conditions  | 0                    | 0                    | int    |
	| Int32Conditions  | 1                    | 1                    | int    |
	| Int32Conditions  | -1                   | -1                   | int    |
	| Int32Conditions  | 1                    | -1                   | int    |
	| Int32Conditions  | -1                   | 1                    | int    |
	| Int32Conditions  | 0                    | 1                    | int    |
	| Int32Conditions  | 1                    | 0                    | int    |
	| Int32Conditions  | 0                    | -1                   | int    |
	| Int32Conditions  | -1                   | 0                    | int    |
	| Int32Conditions  | 2147483647           | 1                    | int    |
	| Int32Conditions  | -2147483648          | 1                    | int    |
	| Int32Conditions  | 2147483647           | -1                   | int    |
	| Int32Conditions  | -2147483648          | -1                   | int    |
	| Int32Conditions  | 32767                | 65536                | int    |
	| Int32Conditions  | -32768               | 65536                | int    |
	| Int32Conditions  | 32767                | -65536               | int    |
	| Int32Conditions  | -32768               | -65536               | int    |
	| Int16Conditions  | 0                    | 0                    | short  |
	| Int16Conditions  | 1                    | 1                    | short  |
	| Int16Conditions  | -1                   | -1                   | short  |
	| Int16Conditions  | 1                    | -1                   | short  |
	| Int16Conditions  | -1                   | 1                    | short  |
	| Int16Conditions  | 0                    | 1                    | short  |
	| Int16Conditions  | 1                    | 0                    | short  |
	| Int16Conditions  | 0                    | -1                   | short  |
	| Int16Conditions  | -1                   | 0                    | short  |
	| Int16Conditions  | 32767                | 1                    | short  |
	| Int16Conditions  | -32768               | 1                    | short  |
	| Int16Conditions  | 32767                | -1                   | short  |
	| Int16Conditions  | -32768               | -1                   | short  |
	| Int16Conditions  | 127                  | 256                  | short  |
	| Int16Conditions  | -128                 | 256                  | short  |
	| Int16Conditions  | 127                  | -256                 | short  |
	| Int16Conditions  | -128                 | -256                 | short  |
	| Int8Conditions   | 0                    | 0                    | sbyte  |
	| Int8Conditions   | 1                    | 1                    | sbyte  |
	| Int8Conditions   | -1                   | -1                   | sbyte  |
	| Int8Conditions   | 1                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 1                    | sbyte  |
	| Int8Conditions   | 0                    | 1                    | sbyte  |
	| Int8Conditions   | 1                    | 0                    | sbyte  |
	| Int8Conditions   | 0                    | -1                   | sbyte  |
	| Int8Conditions   | -1                   | 0                    | sbyte  |
	| Int8Conditions   | 127                  | 1                    | sbyte  |
	| Int8Conditions   | -128                 | 1                    | sbyte  |
	| Int8Conditions   | 127                  | -1                   | sbyte  |
	| Int8Conditions   | -128                 | -1                   | sbyte  |
	| Int8Conditions   | 7                    | 16                   | sbyte  |
	| Int8Conditions   | -8                   | 16                   | sbyte  |
	| Int8Conditions   | 7                    | -16                  | sbyte  |
	| Int8Conditions   | -8                   | -16                  | sbyte  |
	| Int64Conditions  | 0                    | 0                    | long   |
	| Int64Conditions  | 1                    | 1                    | long   |
	| Int64Conditions  | -1                   | -1                   | long   |
	| Int64Conditions  | 1                    | -1                   | long   |
	| Int64Conditions  | -1                   | 1                    | long   |
	| Int64Conditions  | 0                    | 1                    | long   |
	| Int64Conditions  | 1                    | 0                    | long   |
	| Int64Conditions  | 0                    | -1                   | long   |
	| Int64Conditions  | -1                   | 0                    | long   |
	| Int64Conditions  | 9223372036854775807  | 1                    | long   |
	| Int64Conditions  | -9223372036854775808 | 1                    | long   |
	| Int64Conditions  | 9223372036854775807  | -1                   | long   |
	| Int64Conditions  | -9223372036854775808 | -1                   | long   |
	| Int64Conditions  | 2147483647           | 9223372036854775807  | long   |
	| Int64Conditions  | -2147483648          | 9223372036854775807  | long   |
	| Int64Conditions  | 2147483647           | -9223372036854775808 | long   |
	| Int64Conditions  | -2147483648          | -9223372036854775808 | long   |
	| UInt32Conditions | 0                    | 0                    | uint   |
	| UInt32Conditions | 1                    | 1                    | uint   |
	| UInt32Conditions | 1                    | 0                    | uint   |
	| UInt32Conditions | 0                    | 1                    | uint   |
	| UInt32Conditions | 2147483647           | 1                    | uint   |
	| UInt32Conditions | 4294967295           | 1                    | uint   |
	| UInt32Conditions | 32767                | 65536                | uint   |
	| UInt16Conditions | 0                    | 0                    | ushort |
	| UInt16Conditions | 1                    | 1                    | ushort |
	| UInt16Conditions | 1                    | 0                    | ushort |
	| UInt16Conditions | 0                    | 1                    | ushort |
	| UInt16Conditions | 32767                | 1                    | ushort |
	| UInt16Conditions | 65535                | 1                    | ushort |
	| UInt16Conditions | 127                  | 256                  | ushort |
	| UInt8Conditions  | 0                    | 0                    | byte   |
	| UInt8Conditions  | 1                    | 1                    | byte   |
	| UInt8Conditions  | 1                    | 0                    | byte   |
	| UInt8Conditions  | 0                    | 1                    | byte   |
	| UInt8Conditions  | 127                  | 1                    | byte   |
	| UInt8Conditions  | 255                  | 1                    | byte   |
	| UInt8Conditions  | 7                    | 16                   | byte   |
	| UInt64Conditions | 0                    | 0                    | ulong  |
	| UInt64Conditions | 1                    | 1                    | ulong  |
	| UInt64Conditions | 1                    | 0                    | ulong  |
	| UInt64Conditions | 0                    | 1                    | ulong  |
	| UInt64Conditions | 9223372036854775807  | 1                    | ulong  |
	| UInt64Conditions | 18446744073709551615 | 1                    | ulong  |
	| UInt64Conditions | 2147483647           | 9223372036854775807  | ulong  |
	| BigIntConditions  | 0                    | 0                    | BigInteger  |
	| BigIntConditions  | 1                    | 1                    | BigInteger  |
	| BigIntConditions  | -1                   | -1                   | BigInteger  |
	| BigIntConditions  | 1                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 1                    | BigInteger  |
	| BigIntConditions  | 0                    | 1                    | BigInteger  |
	| BigIntConditions  | 1                    | 0                    | BigInteger  |
	| BigIntConditions  | 0                    | -1                   | BigInteger  |
	| BigIntConditions  | -1                   | 0                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | 1                    | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | 1                    | BigInteger  |
	| BigIntConditions  | 92233720368547758070  | -1                   | BigInteger  |
	| BigIntConditions  | -92233720368547758080 | -1                   | BigInteger  |
	| BigIntConditions  | 2147483647           | 9223372036854775807  | BigInteger  |
	| BigIntConditions  | -2147483648          | 9223372036854775807  | BigInteger  |
	| BigIntConditions  | 2147483647           | -9223372036854775808 | BigInteger  |
	| BigIntConditions  | -2147483648          | -9223372036854775808 | BigInteger  |
	



Scenario: Unary Postfix Increment
	Given a sandbox connection
	And a deployed test contract called <TC>
	When An integer '<a>' of integer type '<c>' has postfix increment applied
	Then The integer result is the same as C# would calculate
Examples:
	| TC          | a                    | c      |
	| Int32Unary  | 10                   | int    |
	| Int32Unary  | 0                    | int    |
	| Int32Unary  | -10                  | int    |
	| Int32Unary  | -2147483648          | int    |
	| Int32Unary  | 2147483647           | int    |
	| Int16Unary  | 10                   | short  |
	| Int16Unary  | 0                    | short  |
	| Int16Unary  | -10                  | short  |
	| Int16Unary  | -32768               | short  |
	| Int16Unary  | 32767                | short  |
	| Int8Unary   | 10                   | sbyte  |
	| Int8Unary   | 0                    | sbyte  |
	| Int8Unary   | -10                  | sbyte  |
	| Int8Unary   | -128                 | sbyte  |
	| Int8Unary   | 127                  | sbyte  |
	| Int64Unary  | 10                   | long   |
	| Int64Unary  | 0                    | long   |
	| Int64Unary  | -10                  | long   |
	| Int64Unary  | -9223372036854775808 | long   |
	| Int64Unary  | 9223372036854775807  | long   |
	| UInt32Unary | 10                   | uint   |
	| UInt32Unary | 0                    | uint   |
	| UInt32Unary | 2147483647           | uint   |
	| UInt32Unary | 4294967295           | uint   |
	| UInt16Unary | 10                   | ushort |
	| UInt16Unary | 0                    | ushort |
	| UInt16Unary | 32767                | ushort |
	| UInt16Unary | 65535                | ushort |
	| UInt8Unary  | 10                   | byte   |
	| UInt8Unary  | 0                    | byte   |
	| UInt8Unary  | 127                  | byte   |
	| UInt8Unary  | 255                  | byte   |
	| UInt64Unary | 10                   | ulong  |
	| UInt64Unary | 0                    | ulong  |
	| UInt64Unary | 9223372036854775807  | ulong  |
	| UInt64Unary | 18446744073709551615 | ulong  |
	| BigIntUnary  | 0                    | BigInteger  |
	| BigIntUnary  | -10                  | BigInteger  |
	| BigIntUnary  | -92233720368547758080 | BigInteger  |
	| BigIntUnary  | 92233720368547758070  | BigInteger  |
	



Scenario: Unary Postfix Decrement
	Given a sandbox connection
	And a deployed test contract called <TC>
	When An integer '<a>' of integer type '<c>' has postfix decrement applied
	Then The integer result is the same as C# would calculate
Examples:
	| TC          | a                    | c      |
	| Int32Unary  | 10                   | int    |
	| Int32Unary  | 0                    | int    |
	| Int32Unary  | -10                  | int    |
	| Int32Unary  | -2147483648          | int    |
	| Int32Unary  | 2147483647           | int    |
	| Int16Unary  | 10                   | short  |
	| Int16Unary  | 0                    | short  |
	| Int16Unary  | -10                  | short  |
	| Int16Unary  | -32768               | short  |
	| Int16Unary  | 32767                | short  |
	| Int8Unary   | 10                   | sbyte  |
	| Int8Unary   | 0                    | sbyte  |
	| Int8Unary   | -10                  | sbyte  |
	| Int8Unary   | -128                 | sbyte  |
	| Int8Unary   | 127                  | sbyte  |
	| Int64Unary  | 10                   | long   |
	| Int64Unary  | 0                    | long   |
	| Int64Unary  | -10                  | long   |
	| Int64Unary  | -9223372036854775808 | long   |
	| Int64Unary  | 9223372036854775807  | long   |
	| UInt32Unary | 10                   | uint   |
	| UInt32Unary | 0                    | uint   |
	| UInt32Unary | 2147483647           | uint   |
	| UInt32Unary | 4294967295           | uint   |
	| UInt16Unary | 10                   | ushort |
	| UInt16Unary | 0                    | ushort |
	| UInt16Unary | 32767                | ushort |
	| UInt16Unary | 65535                | ushort |
	| UInt8Unary  | 10                   | byte   |
	| UInt8Unary  | 0                    | byte   |
	| UInt8Unary  | 127                  | byte   |
	| UInt8Unary  | 255                  | byte   |
	| UInt64Unary | 10                   | ulong  |
	| UInt64Unary | 0                    | ulong  |
	| UInt64Unary | 9223372036854775807  | ulong  |
	| UInt64Unary | 18446744073709551615 | ulong  |
	| BigIntUnary  | 0                    | BigInteger  |
	| BigIntUnary  | -10                  | BigInteger  |
	| BigIntUnary  | -92233720368547758080 | BigInteger  |
	| BigIntUnary  | 92233720368547758070  | BigInteger  |




Scenario: Unary Prefix Increment
	Given a sandbox connection
	And a deployed test contract called <TC>
	When An integer '<a>' of integer type '<c>' has prefix increment applied
	Then The integer result is the same as C# would calculate
Examples:
	| TC          | a                    | c      |
	| Int32Unary  | 10                   | int    |
	| Int32Unary  | 0                    | int    |
	| Int32Unary  | -10                  | int    |
	| Int32Unary  | -2147483648          | int    |
	| Int32Unary  | 2147483647           | int    |
	| Int16Unary  | 10                   | short  |
	| Int16Unary  | 0                    | short  |
	| Int16Unary  | -10                  | short  |
	| Int16Unary  | -32768               | short  |
	| Int16Unary  | 32767                | short  |
	| Int8Unary   | 10                   | sbyte  |
	| Int8Unary   | 0                    | sbyte  |
	| Int8Unary   | -10                  | sbyte  |
	| Int8Unary   | -128                 | sbyte  |
	| Int8Unary   | 127                  | sbyte  |
	| Int64Unary  | 10                   | long   |
	| Int64Unary  | 0                    | long   |
	| Int64Unary  | -10                  | long   |
	| Int64Unary  | -9223372036854775808 | long   |
	| Int64Unary  | 9223372036854775807  | long   |
	| UInt32Unary | 10                   | uint   |
	| UInt32Unary | 0                    | uint   |
	| UInt32Unary | 2147483647           | uint   |
	| UInt32Unary | 4294967295           | uint   |
	| UInt16Unary | 10                   | ushort |
	| UInt16Unary | 0                    | ushort |
	| UInt16Unary | 32767                | ushort |
	| UInt16Unary | 65535                | ushort |
	| UInt8Unary  | 10                   | byte   |
	| UInt8Unary  | 0                    | byte   |
	| UInt8Unary  | 127                  | byte   |
	| UInt8Unary  | 255                  | byte   |
	| UInt64Unary | 10                   | ulong  |
	| UInt64Unary | 0                    | ulong  |
	| UInt64Unary | 9223372036854775807  | ulong  |
	| UInt64Unary | 18446744073709551615 | ulong  |
		| BigIntUnary  | 0                    | BigInteger  |
	| BigIntUnary  | -10                  | BigInteger  |
	| BigIntUnary  | -92233720368547758080 | BigInteger  |
	| BigIntUnary  | 92233720368547758070  | BigInteger  |
	






Scenario: Unary Prefix Decrement
	Given a sandbox connection
	And a deployed test contract called <TC>
	When An integer '<a>' of integer type '<c>' has prefix decrement applied
	Then The integer result is the same as C# would calculate
Examples:
	| TC          | a                    | c      |
	| Int32Unary  | 10                   | int    |
	| Int32Unary  | 0                    | int    |
	| Int32Unary  | -10                  | int    |
	| Int32Unary  | -2147483648          | int    |
	| Int32Unary  | 2147483647           | int    |
	| Int16Unary  | 10                   | short  |
	| Int16Unary  | 0                    | short  |
	| Int16Unary  | -10                  | short  |
	| Int16Unary  | -32768               | short  |
	| Int16Unary  | 32767                | short  |
	| Int8Unary   | 10                   | sbyte  |
	| Int8Unary   | 0                    | sbyte  |
	| Int8Unary   | -10                  | sbyte  |
	| Int8Unary   | -128                 | sbyte  |
	| Int8Unary   | 127                  | sbyte  |
	| Int64Unary  | 10                   | long   |
	| Int64Unary  | 0                    | long   |
	| Int64Unary  | -10                  | long   |
	| Int64Unary  | -9223372036854775808 | long   |
	| Int64Unary  | 9223372036854775807  | long   |
	| UInt32Unary | 10                   | uint   |
	| UInt32Unary | 0                    | uint   |
	| UInt32Unary | 2147483647           | uint   |
	| UInt32Unary | 4294967295           | uint   |
	| UInt16Unary | 10                   | ushort |
	| UInt16Unary | 0                    | ushort |
	| UInt16Unary | 32767                | ushort |
	| UInt16Unary | 65535                | ushort |
	| UInt8Unary  | 10                   | byte   |
	| UInt8Unary  | 0                    | byte   |
	| UInt8Unary  | 127                  | byte   |
	| UInt8Unary  | 255                  | byte   |
	| UInt64Unary | 10                   | ulong  |
	| UInt64Unary | 0                    | ulong  |
	| UInt64Unary | 9223372036854775807  | ulong  |
	| UInt64Unary | 18446744073709551615 | ulong  |
		| BigIntUnary  | 0                    | BigInteger  |
	| BigIntUnary  | -10                  | BigInteger  |
	| BigIntUnary  | -92233720368547758080 | BigInteger  |
	| BigIntUnary  | 92233720368547758070  | BigInteger  |







Scenario: Unary Plus
	Given a sandbox connection
	And a deployed test contract called <TC>
	When An integer '<a>' of integer type '<c>' has unary plus applied
	Then The integer result is the same as C# would calculate
Examples:
	| TC          | a                    | c      |
	| Int32Unary  | 10                   | int    |
	| Int32Unary  | 0                    | int    |
	| Int32Unary  | -10                  | int    |
	| Int32Unary  | -2147483648          | int    |
	| Int32Unary  | 2147483647           | int    |
	| Int16Unary  | 10                   | short  |
	| Int16Unary  | 0                    | short  |
	| Int16Unary  | -10                  | short  |
	| Int16Unary  | -32768               | short  |
	| Int16Unary  | 32767                | short  |
	| Int8Unary   | 10                   | sbyte  |
	| Int8Unary   | 0                    | sbyte  |
	| Int8Unary   | -10                  | sbyte  |
	| Int8Unary   | -128                 | sbyte  |
	| Int8Unary   | 127                  | sbyte  |
	| Int64Unary  | 10                   | long   |
	| Int64Unary  | 0                    | long   |
	| Int64Unary  | -10                  | long   |
	| Int64Unary  | -9223372036854775808 | long   |
	| Int64Unary  | 9223372036854775807  | long   |
	| UInt32Unary | 10                   | uint   |
	| UInt32Unary | 0                    | uint   |
	| UInt32Unary | 2147483647           | uint   |
	| UInt32Unary | 4294967295           | uint   |
	| UInt16Unary | 10                   | ushort |
	| UInt16Unary | 0                    | ushort |
	| UInt16Unary | 32767                | ushort |
	| UInt16Unary | 65535                | ushort |
	| UInt8Unary  | 10                   | byte   |
	| UInt8Unary  | 0                    | byte   |
	| UInt8Unary  | 127                  | byte   |
	| UInt8Unary  | 255                  | byte   |
	| UInt64Unary | 10                   | ulong  |
	| UInt64Unary | 0                    | ulong  |
	| UInt64Unary | 9223372036854775807  | ulong  |
	| UInt64Unary | 18446744073709551615 | ulong  |
		| BigIntUnary  | 0                    | BigInteger  |
	| BigIntUnary  | -10                  | BigInteger  |
	| BigIntUnary  | -92233720368547758080 | BigInteger  |
	| BigIntUnary  | 92233720368547758070  | BigInteger  |
	






Scenario: Unary Minus
	Given a sandbox connection
	And a deployed test contract called <TC>
	When An integer '<a>' of integer type '<c>' has unary minus applied
	Then The integer result is the same as C# would calculate
Examples:
	| TC          | a                    | c      |
	| Int32Unary  | 10                   | int    |
	| Int32Unary  | 0                    | int    |
	| Int32Unary  | -10                  | int    |
	| Int32Unary  | -2147483648          | int    |
	| Int32Unary  | 2147483647           | int    |
	| Int16Unary  | 10                   | short  |
	| Int16Unary  | 0                    | short  |
	| Int16Unary  | -10                  | short  |
	| Int16Unary  | -32768               | short  |
	| Int16Unary  | 32767                | short  |
	| Int8Unary   | 10                   | sbyte  |
	| Int8Unary   | 0                    | sbyte  |
	| Int8Unary   | -10                  | sbyte  |
	| Int8Unary   | -128                 | sbyte  |
	| Int8Unary   | 127                  | sbyte  |
	| Int64Unary  | 10                   | long   |
	| Int64Unary  | 0                    | long   |
	| Int64Unary  | -10                  | long   |
	| Int64Unary  | -9223372036854775808 | long   |
	| Int64Unary  | 9223372036854775807  | long   |
	| UInt32Unary | 10                   | uint   |
	| UInt32Unary | 0                    | uint   |
	| UInt32Unary | 2147483647           | uint   |
	| UInt32Unary | 4294967295           | uint   |
	| UInt16Unary | 10                   | ushort |
	| UInt16Unary | 0                    | ushort |
	| UInt16Unary | 32767                | ushort |
	| UInt16Unary | 65535                | ushort |
	| UInt8Unary  | 10                   | byte   |
	| UInt8Unary  | 0                    | byte   |
	| UInt8Unary  | 127                  | byte   |
	| UInt8Unary  | 255                  | byte   |
	| UInt64Unary | 10                   | ulong  |
	| UInt64Unary | 0                    | ulong  |
	| UInt64Unary | 9223372036854775807  | ulong  |
	| UInt64Unary | 18446744073709551615 | ulong  |
	| BigIntUnary  | 0                    | BigInteger  |
	| BigIntUnary  | -10                  | BigInteger  |
	| BigIntUnary  | -92233720368547758080 | BigInteger  |
	| BigIntUnary  | 92233720368547758070  | BigInteger  |





Scenario: Unary Not
	Given a sandbox connection
	And a deployed test contract called <TC>
	When An integer '<a>' of integer type '<c>' has unary not applied
	Then The integer result is the same as C# would calculate
Examples:
	| TC          | a                    | c      |
	| Int32Unary  | 10                   | int    |
	| Int32Unary  | 0                    | int    |
	| Int32Unary  | -10                  | int    |
	| Int32Unary  | -2147483648          | int    |
	| Int32Unary  | 2147483647           | int    |
	| Int16Unary  | 10                   | short  |
	| Int16Unary  | 0                    | short  |
	| Int16Unary  | -10                  | short  |
	| Int16Unary  | -32768               | short  |
	| Int16Unary  | 32767                | short  |
	| Int8Unary   | 10                   | sbyte  |
	| Int8Unary   | 0                    | sbyte  |
	| Int8Unary   | -10                  | sbyte  |
	| Int8Unary   | -128                 | sbyte  |
	| Int8Unary   | 127                  | sbyte  |
	| Int64Unary  | 10                   | long   |
	| Int64Unary  | 0                    | long   |
	| Int64Unary  | -10                  | long   |
	| Int64Unary  | -9223372036854775808 | long   |
	| Int64Unary  | 9223372036854775807  | long   |
	| UInt32Unary | 10                   | uint   |
	| UInt32Unary | 0                    | uint   |
	| UInt32Unary | 2147483647           | uint   |
	| UInt32Unary | 4294967295           | uint   |
	| UInt16Unary | 10                   | ushort |
	| UInt16Unary | 0                    | ushort |
	| UInt16Unary | 32767                | ushort |
	| UInt16Unary | 65535                | ushort |
	| UInt8Unary  | 10                   | byte   |
	| UInt8Unary  | 0                    | byte   |
	| UInt8Unary  | 127                  | byte   |
	| UInt8Unary  | 255                  | byte   |
	| UInt64Unary | 10                   | ulong  |
	| UInt64Unary | 0                    | ulong  |
	| UInt64Unary | 9223372036854775807  | ulong  |
	| UInt64Unary | 18446744073709551615 | ulong  |
	| BoolUnary   | true                 | bool   |
	| BoolUnary   | false                | bool   |
	| BigIntUnary  | 0                    | BigInteger  |
	| BigIntUnary  | -10                  | BigInteger  |
	| BigIntUnary  | -92233720368547758080 | BigInteger  |
	| BigIntUnary  | 92233720368547758070  | BigInteger  |



