Feature: PostMovies
	
Scenario: Should return 201 on successful create
	When I POST 'api/admin/movies/' with the following:
	"""
	{
		"movieId": "{movieId}",
		"name": "John Wick",
		"releaseDate": "03/22/17",
		"ticketPrice": "8.65",
		"rating": "R"
	}
	"""
	Then the status should be 201
	#And the Content-Type header should match regex 'application/json'