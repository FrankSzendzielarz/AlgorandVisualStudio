Feature: InnerTransactions

Various scenarios of inner transactions, including contract to contract calls 


Scenario: A contract to contract call where the outer contract accesses both the result and the transaction
	Given a sandbox connection
	And a deployed test contract called <TC>
	And a deployed callee test contract called '<TCInner>'
	And the contract is funded with the minimum balance by acccount1
	And the contract is funded with the minimum balance by acccount1
	When the outer test method addtwonumbers is called
	Then the result is a txid and this is a payment for 1234 millialgos

Examples:
	| TC                 | TCInner |
	| ContractReferences | ReferencedContract |

