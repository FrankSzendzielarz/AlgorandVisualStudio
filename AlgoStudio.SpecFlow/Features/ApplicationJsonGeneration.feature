Feature: ApplicationJsonGeneration
	As a developer 
	I want to be able to export interface metadata as JSON in a format compatible with the Python compiler (and others)
	So that other developers can generate proxy clients regardless of the language

Scenario: Method contains unsigned BigInteger that should be treated as a fixed point decimal number according to ARC4


Scenario: Check method selector from method
	Given a sandbox connection
	And a deployed test contract called <TC>
	When Two integers '<a>' and '<b>' of integer type '<c>' are used in a method called '<method>'
	Then The integer result is the same as C# would calculate
Examples:
	| TC             | method  | a | b | c    |
	| JsonAddNumbers | add     | 1 | 2 | uint |
	| JsonAddNumbers | Method2 | 3 | 5 | int  |



	