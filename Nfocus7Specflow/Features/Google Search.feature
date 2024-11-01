@gui @functional
Feature: Google Search

@smoke
Scenario: Edgewords should be the top result for a search of edgewords
	Given I am on the Google Homepage
	When I search for 'Edgewords'
	#Steps with parameters using " " make uglier regex in the step definitions
	Then "Edgewords" is the top result
	#Then "Edgewords" is the number '1' result

@ignore
Scenario Outline: Check various results
	Given I am on the Google Homepage
	When I search for '<searchTerm>'
	Then "<searchResult>" is the top result
Examples:
	| searchTerm         | searchResult            |
	| edgewords          | edgewords               |
	| Edgewords Training | edgewordstraining.co.uk |

@smoke @run
Scenario: Check result details
	Given I am on the Google Homepage
	When I search for 'Edgewords'
	Then I should see in the results
		| title              | url                                 |
		| Edgewords Training | https://www.edgewordstraining.co.uk |
		| GitHub             | https://github.com › edgewords      |