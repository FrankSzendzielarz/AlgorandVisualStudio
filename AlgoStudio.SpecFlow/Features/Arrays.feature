Feature: Arrays

Various array usage tests

Scenario: Accessing non-byte arrays declared as ABI method parameters
	Given a sandbox connection
	And a deployed test contract called <TC>
	When an ABI method with an int array is called that returns an array element
	Then The integer result is the same as C# would calculate

Examples:
	| TC                 | 
	| ArrayTestContract1 | 
	
	
Scenario: Updating byte arrays declared as ABI method parameters
	Given a sandbox connection
	And a deployed test contract called <TC>
	When an ABI method with a byte array is called that updates and element and returns it from the array
	Then The result is as expected

Examples:
	| TC                 | 
	| ArrayTestContract1 | 
	

Scenario: Concatenating byte arrays
	Given a sandbox connection
	And a deployed test contract called <TC>
	When an ABI method with two byte arrays is called that concatenates them
	Then The arrays result is as expected

Examples:
	| TC                 | 
	| ArrayTestContract1 | 
	
Scenario: Assigning byte array outputs to variables and converting to string
	Given a sandbox connection
	And a deployed test contract called <TC>
	When an ABI method with two byte arrays is called that concatenates them and assigns them to a variable
	Then The result is as expected

Examples:
	| TC                 | 
	| ArrayTestContract1 | 

Scenario: Getting the bit length of a byte array
	Given a sandbox connection
	And a deployed test contract called <TC>
	When an ABI method with a byte array is called that returns the bit length of the array
	Then The result is as expected

Examples:
	| TC                 | 
	| ArrayTestContract1 | 

Scenario: Getting a specific bit from a byte array
	Given a sandbox connection
	And a deployed test contract called <TC>
	When an ABI method with a byte array is called that returns a specific bit from the array
	Then The result is as expected

Examples:
	| TC                 | 
	| ArrayTestContract1 | 
	