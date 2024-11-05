Feature: Users
	For checking User Features


Scenario: Check a single User
	When  I request user number '1'
	Then I get a '200' response code
	And the user is 'Bob Jones'
