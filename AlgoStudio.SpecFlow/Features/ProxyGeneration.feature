Feature: ProxyGeneration 
	As a developer
	I want to be able to generate proxies for smart contracts (SCs)
	So that I can easily call them like Web API methods or other well known types of proxy


Scenario: Generate a transaction group to sign and send 
	Given a sandbox connection
	And we get the account balances
	And a deployed test contract called <TC>
	And the contract is funded with the minimum balance by acccount1
	When the payment and split payment proxy method to return a transaction group is called with percentage, <P>, and amount <X>
	And the transaction group is signed
	And the signed group is sent
	And we get the account balances
	Then the first Account is debited by <X> plus the fee <F>
	And the second Account is credited with <Y>
	And the third Account is credited with <Z>

Examples:
	| TC			| F    | P	   | X     | Y    | Z    |
	| SplitPayments | 6000 | 5000  | 10000 | 5000 | 5000 |

	
	