@gui
Feature: WebDriver2Site

A short summary of the feature

@tag1
Scenario: Logging in with a valid username and password
	Given That I am on the login page
	When I use the username "edgewords" and password "edgewords123xxxxxxxxxx" to log in
	Then I am logged in
