Feature: DeleteMovies

Scenario: Should delete movie with a response of 200
	Given movie exists
	When I DELETE 'api/admin/movies/{movieId}' with the following:
	"""
	{
		"movieId": "{movieId}",
	}
	"""
	Then the status should be 200