Feature: Loops

C# for, do, while loops


Scenario: Loops
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning an integer is called
	Then The integer result is the same as C# would calculate

Examples:
	| TC         | Method |
	| ForLoops   | For1   |
	| ForLoops   | For1a  |
	| ForLoops   | For1b  |
	| ForLoops   | For2   |
	| ForLoops   | For3   |
	| ForLoops   | For4   |
	| ForLoops   | For5   |
	| ForLoops   | For6   |
	| DoLoops    | Do1    |
	| DoLoops    | Do2    |
	| DoLoops    | Do3    |
	| DoLoops    | Do4    |
	| DoLoops    | Do5    |
	| WhileLoops | While1 |
	| WhileLoops | While2 |
	| WhileLoops | While3 |
	| WhileLoops | While4 |
	| WhileLoops | While5 |
