Feature: GetMovies

Scenario: Should return all movies
	When I GET 'api/movies'
	Then the status should be 200
	And the JSON at '[name=King Kong].movieId' should be '225f2e51-59a2-41c9-8fda-e1e062b4bc83'
	And the JSON at '[name=King Kong].name' should be 'King Kong'
	And the JSON at '[name=King Kong].releaseDate' should be '3/22/2013 00:00:00'
	And the JSON at '[name=King Kong].ticketPrice' should be '8.9'
	And the JSON at '[name=King Kong].rating' should be 'R'