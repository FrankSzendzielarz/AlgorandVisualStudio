Feature: DecimalTypeSupport
	As a developer
	I want to use Floating Point Decimal 
	So that I can have a wide range of precise values for finance and acConvert decimal to bytes

	TODOs: Add all casting tests

Scenario: Convert decimal to bytes
	Given a sandbox connection
	And a deployed test contract called <TC>
	When the convert decimals to bytes method is called
	Then the returned value is a byte array that can be converted back to a decimal

Examples:
	| TC                     |
	| DecimalCastTypeSupport |

Scenario: Convert bytes to decimal
	Given a sandbox connection
	And a deployed test contract called <TC>
	When the convert bytes to decimal method is called
	Then the returned value is a decimal that matches the original value

Examples:
	| TC                     |
	| DecimalCastTypeSupport |

Scenario: Convert decimal to bytes and back
	Given a sandbox connection
	And a deployed test contract called <TC>
	When the convert to bytes and back again method is called
	Then the exact same decimal is returned

Examples:
	| TC                     |
	| DecimalCastTypeSupport |

Scenario: Convert bytes to decimal and back
	Given a sandbox connection
	And a deployed test contract called <TC>
	When the convert from bytes to decimal and back again method is called
	Then the exact same bytes are returned

Examples:
	| TC                     |
	| DecimalCastTypeSupport |


Scenario: Unary PreIncrement
	Given a sandbox connection
	And a deployed test contract called <TC>
	When The number '<a>' is pre-incremented
	Then The bool result is the same as C# would calculate

Examples:
	| TC                  | a                              |
	| DecimalUnarySupport | 0.0000000000000000000000000001 |
	| DecimalUnarySupport | 0.0000000000000000000000000001 |
	| DecimalUnarySupport | 0.0000000000000000000000000006 |
	| DecimalUnarySupport | 1.0                            |
	| DecimalUnarySupport | 0.0                            |
	| DecimalUnarySupport | 1111111111111111111111111111   |
	| DecimalUnarySupport | 0.0                            |
	| DecimalUnarySupport | -1.0                           |
	| DecimalUnarySupport | -1.0                           |
	| DecimalUnarySupport | 111111111111111111111111111.4  |


Scenario: Unary PostIncrement
	Given a sandbox connection
	And a deployed test contract called <TC>
	When The number '<a>' is post-incremented
	Then The bool result is the same as C# would calculate

Examples:
	| TC                  | a                              |
	| DecimalUnarySupport | 0.0000000000000000000000000001 |
	| DecimalUnarySupport | 0.0000000000000000000000000001 |
	| DecimalUnarySupport | 0.0000000000000000000000000006 |
	| DecimalUnarySupport | 1.0                            |
	| DecimalUnarySupport | 0.0                            |
	| DecimalUnarySupport | 1111111111111111111111111111   |
	| DecimalUnarySupport | 0.0                            |
	| DecimalUnarySupport | -1.0                           |
	| DecimalUnarySupport | -1.0                           |
	| DecimalUnarySupport | 111111111111111111111111111.4  |

Scenario: Unary PreDecrement
	Given a sandbox connection
	And a deployed test contract called <TC>
	When The number '<a>' is pre-decremented
	Then The bool result is the same as C# would calculate

Examples:
	| TC                  | a                              |
	| DecimalUnarySupport | 0.0000000000000000000000000001 |
	| DecimalUnarySupport | 0.0000000000000000000000000001 |
	| DecimalUnarySupport | 0.0000000000000000000000000006 |
	| DecimalUnarySupport | 1.0                            |
	| DecimalUnarySupport | 0.0                            |
	| DecimalUnarySupport | 1111111111111111111111111111   |
	| DecimalUnarySupport | 0.0                            |
	| DecimalUnarySupport | -1.0                           |
	| DecimalUnarySupport | -1.0                           |
	| DecimalUnarySupport | 111111111111111111111111111.4  |


Scenario: Unary PostDecrement
	Given a sandbox connection
	And a deployed test contract called <TC>
	When The number '<a>' is post-decremented
	Then The bool result is the same as C# would calculate

Examples:
	| TC                  | a                              |
	| DecimalUnarySupport | 0.0000000000000000000000000001 |
	| DecimalUnarySupport | 0.0000000000000000000000000001 |
	| DecimalUnarySupport | 0.0000000000000000000000000006 |
	| DecimalUnarySupport | 1.0                            |
	| DecimalUnarySupport | 0.0                            |
	| DecimalUnarySupport | 1111111111111111111111111111   |
	| DecimalUnarySupport | 0.0                            |
	| DecimalUnarySupport | -1.0                           |
	| DecimalUnarySupport | -1.0                           |
	| DecimalUnarySupport | 111111111111111111111111111.4  |

Scenario: Addition
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are added
	Then The result is the same as C# would calculate

Examples:
	| TC                       | a                              | b                              |
	| DecimalArithmeticSupport | 0.0000000000000000000000000001 | 0.0000000000000000000000000001 |
	| DecimalArithmeticSupport | 0.0000000000000000000000000001 | 1.0 |
	| DecimalArithmeticSupport | 0.0000000000000000000000000001 | 0.0000000000000000000000000002 |
	| DecimalArithmeticSupport | 0.0000000000000000000000000006 | 0.0000000000000000000000000006 |
	| DecimalArithmeticSupport | 1.0                            | 0.0                            |
	| DecimalArithmeticSupport | 0.0                            | 0.0                            |
	| DecimalArithmeticSupport | 1111111111111111111111111111   | 1111111111111111111111111111   |
	| DecimalArithmeticSupport | 0.0                            | -1.0                           |
	| DecimalArithmeticSupport | -1.0                           | 0.0                            |
	| DecimalArithmeticSupport | -1.0                           | -1.0                           |
	| DecimalArithmeticSupport | 111111111111111111111111111.4  | 111111111111111111111111111.8  |
		


Scenario: Multiplication
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are multiplied
	Then The result is the same as C# would calculate
Examples:
	| TC                       | a                              | b                               |
	| DecimalArithmeticSupport | 1.0                            | 2.0                             |
	| DecimalArithmeticSupport | 0.1                            | 10.0                            |
	| DecimalArithmeticSupport | 0.333                          | 3.0                             |
	| DecimalArithmeticSupport | 0.5                          | 3.0                             |
	| DecimalArithmeticSupport | 0.1                          | 3.0                             |
	| DecimalArithmeticSupport | 0.05                          | 3.0                             |
	| DecimalArithmeticSupport | 3.0                            | 0.333                           |
	| DecimalArithmeticSupport | 3.0                            | 30.0                           |
	| DecimalArithmeticSupport | 30.0                              | 0.333                          |
	| DecimalArithmeticSupport | 300.0                              | 0.333                          |
	| DecimalArithmeticSupport | 1.23456789123456789            | 1.0                             |
	| DecimalArithmeticSupport | 1.23456789123456789            | 3.33                            |
	| DecimalArithmeticSupport | 0.0000000000000000000000000001 | 10000000000000000000000000000.0 |
	| DecimalArithmeticSupport | 79228162514264337593543950335  | 1.0                             |
	| DecimalArithmeticSupport | 79228162514264337593543950335  | 2.0                             |
	| DecimalArithmeticSupport | -79228162514264337593543950335 | -1.0                            |
	| DecimalArithmeticSupport | -79228162514264337593543950335 | 1.0                             |

Scenario: Division
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are divided
	Then The result is the same as C# would calculate
Examples:
	| TC                       | a                                | b                              |
	| DecimalArithmeticSupport | 1.0                              | 2.0                            |
	| DecimalArithmeticSupport | 10.0                             | 0.1                            |
	| DecimalArithmeticSupport | 3.0                              | 0.333                          |
	
	| DecimalArithmeticSupport | 0.333                            | 3                              |
	| DecimalArithmeticSupport | 1.23456789123456789              | 1.0                            |
	| DecimalArithmeticSupport | 10000000000000000000000000000.0  | 0.0000000000000000000000000001 |
	| DecimalArithmeticSupport | 79228162514264337593543950335.0  | 1.0                            |
	| DecimalArithmeticSupport | -79228162514264337593543950335.0 | -1.0                           |
	| DecimalArithmeticSupport | -79228162514264337593543950335.0 | 1.0                            |
	| DecimalArithmeticSupport | 1.0                              | 0.0                            |


Scenario: Subtraction
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are subtracted
	Then The result is the same as C# would calculate
Examples:
	| TC                       | a                                | b                              |
	| DecimalArithmeticSupport | 1.0                              | 2.0                            |
	| DecimalArithmeticSupport | 10.0                             | 0.1                            |
	| DecimalArithmeticSupport | 3.0                              | 0.333                          |
	| DecimalArithmeticSupport | 1.23456789123456789              | 1.0                            |
	| DecimalArithmeticSupport | 10000000000000000000000000000.0  | 0.0000000000000000000000000001 |
	| DecimalArithmeticSupport | 79228162514264337593543950335.0  | 1.0                            |
	| DecimalArithmeticSupport | -79228162514264337593543950335.0 | -1.0                           |
	| DecimalArithmeticSupport | -79228162514264337593543950335.0 | 1.0                            |
	| DecimalArithmeticSupport | 79228162514264337593543950335.0  | 100.0                            |
	| DecimalArithmeticSupport | -79228162514264337593543950335.0 | -100.0                           |
	| DecimalArithmeticSupport | -79228162514264337593543950335.0 | 100.0                            |
	| DecimalArithmeticSupport | 1.0                              | 0.0                            |

Scenario: Complex Expressions
	Given a sandbox connection
	And a deployed test contract called <TC>
	And a deployed opup contract
	When Three numbers '<a>' and '<b>' and '<c>' are used as input to a complex expression
	Then The result is the same as C# would calculate
Examples:
	| TC                       | a                                | b                              | c   |
	| DecimalArithmeticSupport | 1.0                              | 1.0                            | 1.0 |
	| DecimalArithmeticSupport | 0.0                              | 0.0                            | 1.0 |
	| DecimalArithmeticSupport | -1.0                             | -1.0                           | 1.0 |
	| DecimalArithmeticSupport | 0.1                              | 10.0                           | 1.0 |
	| DecimalArithmeticSupport | 3.0                              | 0.333                          | 1.0 |
	| DecimalArithmeticSupport | 1.23456789123456789              | 1.0                            | 1.0 |
	| DecimalArithmeticSupport | 10000000000000000000000000000.0  | 0.0000000000000000000000000001 | 1.0 |
	| DecimalArithmeticSupport | 79228162514264337593543950335.0  | 1.0                            | 1.0 |
	| DecimalArithmeticSupport | -79228162514264337593543950335.0 | -1.0                           | 1.0 |
	| DecimalArithmeticSupport | -79228162514264337593543950335.0 | 1.0                            | 1.0 |

		
Scenario: Addition Casted To Ulong
	Given a sandbox connection
	And a deployed opup contract
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are added and casted to ulong
	Then The ulong result is the same as C# would calculate
	

Examples:
	| TC                     | a                              | b                              |
	| DecimalCastTypeSupport | 0.0000000000000000000000000001 | 0.0000000000000000000000000001 |
	| DecimalCastTypeSupport | 0.0000000000000000000000000001 | 0.0000000000000000000000000002 |
	| DecimalCastTypeSupport | 0.0000000000000000000000000006 | 0.0000000000000000000000000006 |
	| DecimalCastTypeSupport | 1.0                            | 0.0                            |
	| DecimalCastTypeSupport | 0.0                            | 0.0                            |
	| DecimalCastTypeSupport | 18446744073709551615           | 0.0                            |
	| DecimalCastTypeSupport | 50.0                           | 50.0000000                     |



Scenario: Subtraction Casted To Ulong
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are subtracted and cast to ulong
	Then The ulong result is the same as C# would calculate
Examples:
	| TC                     | a                                | b                              |
	| DecimalCastTypeSupport | 10.0                             | 0.1                            |
	| DecimalCastTypeSupport | 3.0                              | 0.333                          |
	| DecimalCastTypeSupport | 1.23456789123456789              | 1.0                            |
	| DecimalCastTypeSupport | 10000000000000000000000000000.0  | 0.0000000000000000000000000001 |
	| DecimalCastTypeSupport | 79228162514264337593543950335.0  | 1.0                            |
	| DecimalCastTypeSupport | 1.0                              | 0.0                            |

Scenario: Multiplication Casted To Ulong
	Given a sandbox connection
	And a deployed opup contract
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are multiplied and cast to ulong
	Then The ulong result is the same as C# would calculate
Examples:
	| TC                     | a                              | b                               |
	| DecimalCastTypeSupport | 1.0                            | 2.0                             |
	| DecimalCastTypeSupport | 0.1                            | 10.0                            |
	| DecimalCastTypeSupport | 0.333                          | 3.0                             |
	| DecimalCastTypeSupport | 3.0                            | 0.333                           |
	| DecimalCastTypeSupport | 6.0                            | 0.333                           |
	| DecimalCastTypeSupport | 1.23456789123456789            | 1.0                             |
	| DecimalCastTypeSupport | 1.23456789123456789            | 3.33                            |
	| DecimalCastTypeSupport | 0.0000000000000000000000000001 | 10000000000000000000000000000.0 |
	| DecimalCastTypeSupport | 79228162514264337593543950335  | 1.0                             |
	| DecimalCastTypeSupport | 79228162514264337593543950335  | 2.0                             |
	| DecimalCastTypeSupport | -79228162514264337593543950335 | -1.0                            |
	| DecimalCastTypeSupport | -79228162514264337593543950335 | 1.0                             |

Scenario: Division Casted To Ulong
	Given a sandbox connection
	And a deployed opup contract
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are divided and cast to ulong
	Then The ulong result is the same as C# would calculate
Examples:
	| TC                     | a                                | b                              |
	| DecimalCastTypeSupport | 1.0                              | 2.0                            |
	| DecimalCastTypeSupport | 10.0                             | 0.1                            |
	| DecimalCastTypeSupport | 3.0                              | 0.333                          |
	| DecimalCastTypeSupport | 0.333                            | 3                              |
	| DecimalCastTypeSupport | 1.23456789123456789              | 1.0                            |
	| DecimalCastTypeSupport | 10000000000000000000000000000.0  | 0.0000000000000000000000000001 |
	| DecimalCastTypeSupport | 79228162514264337593543950335.0  | 1.0                            |
	| DecimalCastTypeSupport | -79228162514264337593543950335.0 | -1.0                           |
	| DecimalCastTypeSupport | -79228162514264337593543950335.0 | 1.0                            |
	| DecimalCastTypeSupport | 1.0                              | 0.0                            |

Scenario: Equals
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are compared for equality
	Then The bool result is the same as C# would calculate
Examples:
	| TC                       | a    | b    |
	| DecimalConditionsSupport | 1.0  | 1.0  |
	| DecimalConditionsSupport | -1.0 | 1.0  |
	| DecimalConditionsSupport | 0.0  | 0.0  |
	| DecimalConditionsSupport | -0.0 | 0.0  |
	| DecimalConditionsSupport | 0.0  | -0.0 |
	| DecimalConditionsSupport | 1.0  | 2.0  |
	| DecimalConditionsSupport | 2.0  | 1.0  |
	| DecimalConditionsSupport | -2.0 | -1.0 |
	| DecimalConditionsSupport | -1.0 | -2.0 |
	| DecimalConditionsSupport | 123.0 | 123.000 |

Scenario: Not Equals
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are compared for inequality
	Then The bool result is the same as C# would calculate
Examples:
	| TC                       | a    | b    |
	| DecimalConditionsSupport | 1.0  | 1.0  |
	| DecimalConditionsSupport | -1.0 | 1.0  |
	| DecimalConditionsSupport | 0.0  | 0.0  |
	| DecimalConditionsSupport | -0.0 | 0.0  |
	| DecimalConditionsSupport | 0.0  | -0.0 |
	| DecimalConditionsSupport | 1.0  | 2.0  |
	| DecimalConditionsSupport | 2.0  | 1.0  |
	| DecimalConditionsSupport | -2.0 | -1.0 |
	| DecimalConditionsSupport | -1.0 | -2.0 |

Scenario: Greater Than
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are compared for greater than
	Then The bool result is the same as C# would calculate
Examples:
	| TC                       | a    | b    |
	| DecimalConditionsSupport | 1.0  | 1.0  |
	| DecimalConditionsSupport | -1.0 | 1.0  |
	| DecimalConditionsSupport | 0.0  | 0.0  |
	| DecimalConditionsSupport | -0.0 | 0.0  |
	| DecimalConditionsSupport | 0.0  | -0.0 |
	| DecimalConditionsSupport | 1.0  | 2.0  |
	| DecimalConditionsSupport | 2.0  | 1.0  |
	| DecimalConditionsSupport | -2.0 | -1.0 |
	| DecimalConditionsSupport | -1.0 | -2.0 |

Scenario: Less Than
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are compared for less than
	Then The bool result is the same as C# would calculate
Examples:
	| TC                       | a    | b    |
	| DecimalConditionsSupport | 1.0  | 1.0  |
	| DecimalConditionsSupport | -1.0 | 1.0  |
	| DecimalConditionsSupport | 0.0  | 0.0  |
	| DecimalConditionsSupport | -0.0 | 0.0  |
	| DecimalConditionsSupport | 0.0  | -0.0 |
	| DecimalConditionsSupport | 1.0  | 2.0  |
	| DecimalConditionsSupport | 2.0  | 1.0  |
	| DecimalConditionsSupport | -2.0 | -1.0 |
	| DecimalConditionsSupport | -1.0 | -2.0 |

Scenario: Greater Than Or Equal To
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are compared for greater than or equal to
	Then The bool result is the same as C# would calculate
Examples:
	| TC                       | a    | b    |
	| DecimalConditionsSupport | 1.0  | 1.0  |
	| DecimalConditionsSupport | -1.0 | 1.0  |
	| DecimalConditionsSupport | 1.0 | -1.0  |
	| DecimalConditionsSupport | 0.0  | 0.0  |
	| DecimalConditionsSupport | -0.0 | 0.0  |
	| DecimalConditionsSupport | 0.0  | -0.0 |
	| DecimalConditionsSupport | 1.0  | 2.0  |
	| DecimalConditionsSupport | 2.0  | 1.0  |
	| DecimalConditionsSupport | -2.0 | -1.0 |
	| DecimalConditionsSupport | -1.0 | -2.0 |

Scenario: Less Than Or Equal To
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two numbers '<a>' and '<b>' are compared for less than or equal to
	Then The bool result is the same as C# would calculate
Examples:
	| TC                       | a    | b    |
	| DecimalConditionsSupport | 1.0  | 1.0  |
	| DecimalConditionsSupport | 1.0  | -1.0  |
	| DecimalConditionsSupport | -1.0 | 1.0  |
	| DecimalConditionsSupport | 0.0  | 0.0  |
	| DecimalConditionsSupport | -0.0 | 0.0  |
	| DecimalConditionsSupport | 0.0  | -0.0 |
	| DecimalConditionsSupport | 1.0  | 2.0  |
	| DecimalConditionsSupport | 2.0  | 1.0  |
	| DecimalConditionsSupport | -2.0 | -1.0 |
	| DecimalConditionsSupport | -1.0 | -2.0 |






	