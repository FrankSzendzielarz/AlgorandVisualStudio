Feature: Expressions

Expression sanity checks

Scenario: Make sure expression stack usage is correct
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning an integer is called
	Then The integer result is the same as C# would calculate

Examples:
	| TC              | Method |
	| Expressions1 | StackCheck      |
