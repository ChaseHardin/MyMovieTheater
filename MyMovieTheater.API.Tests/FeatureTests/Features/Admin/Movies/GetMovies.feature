Feature: GetMovies

Scenario: Should return all movies
	Given movie exists
	When I GET 'api/admin/movies'
	Then the status should be 200
	And the JSON at '[movieId=movieId].movieId' should be '{movieId}'
	#And the JSON at '[name=King Kong].name' should be 'King Kong'
	#And the JSON at '[name=King Kong].releaseDate' should be '3/22/2013 00:00:00'
	#And the JSON at '[name=King Kong].ticketPrice' should be '8.9'
	#And the JSON at '[name=King Kong].rating' should be 'R'