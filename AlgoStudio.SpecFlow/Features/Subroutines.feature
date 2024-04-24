Feature: Subroutines

Various subroutine related tests

Scenario: Test duplicate labels
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning an integer is called
	Then The integer result is the same as C# would calculate

Examples:
	| TC          | Method     |
	| Subroutines | DoNothing1 |