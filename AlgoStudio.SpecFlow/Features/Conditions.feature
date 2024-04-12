Feature: Conditions

If statement checks

Scenario: Conditions
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning an integer is called
	Then The integer result is the same as C# would calculate

Examples:
	| TC         | Method |
	| Conditions | If1    |
	| Conditions | If2    |
	| Conditions | If3    |
	| Conditions | If4    |
	| Conditions | If5    |
	| Conditions | If6    |
	| Conditions | If7    |
	| Conditions | If8    |
	| Conditions | If9    |
	| Conditions | If10   |
	| Conditions | If11   |
