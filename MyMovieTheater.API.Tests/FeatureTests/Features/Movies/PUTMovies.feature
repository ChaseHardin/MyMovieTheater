Feature: PutMovies

Scenario: Should update movie with a response of 200
	Given movie exists
	When I PUT 'api/admin/movies/{movieId}' with the following:
	"""
	{
		"movieId": "{movieId}",
		"name": "John Wick 2",
		"releaseDate": "03/22/17",
		"ticketPrice": "8.65",
		"rating": "R"
	}
	"""
	Then the status should be 200
	And the 'Content-Type' header should match regex 'application/json'