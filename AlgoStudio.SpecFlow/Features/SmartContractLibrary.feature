Feature: SmartContractLibrary

Library usage

Scenario: Library calls
	Given a sandbox connection
	And a deployed test contract called <TC>
	When a method named '<Method>' returning an integer is called
	Then The integer result is the same as C# would calculate

Examples:
	| TC                       | Method                |
	| UsesSmartContractLibrary | DirectDependency      |
	| UsesSmartContractLibrary | MultipleDependencies  |
	| UsesSmartContractLibrary | IndirectDependencies  |
	| UsesSmartContractLibrary | CircularDependencies  |
	| UsesSmartContractLibrary | PredefinedMethodUsage |

