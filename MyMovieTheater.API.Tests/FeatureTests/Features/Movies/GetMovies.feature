Feature: GetMovies

Scenario: Should return all movies
	When I GET ''
	Then the status should be 200
	And the JSON at '' should be ''