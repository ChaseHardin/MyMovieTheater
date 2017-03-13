Feature: GetMovies

Scenario: Should return all movies
	When I GET 'api/movies'
	Then the status should be 200
	And the JSON at '[0].MovieId' should be '225f2e51-59a2-41c9-8fda-e1e062b4bc83'
	And the JSON at '[0].Name' should be 'King Kong'
	And the JSON at '[0].ReleaseDate' should be '3/22/2013 00:00:00'
	And the JSON at '[0].TicketPrice' should be '8.9'
	And the JSON at '[0].Rating' should be 'R'