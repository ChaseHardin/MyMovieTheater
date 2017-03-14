using System;
using System.Collections.Generic;
using MyMovieTheater.Business.Commands;
using MyMovieTheater.Business.ViewModels;

namespace MyMovieTheater.Business.Services
{
    public class MovieService : BaseService
    {
        public List<MovieViewModel> GetMovies()
        {
            return new GetMoviesCommand().Get();
        }

        public MovieViewModel AddMovie(MovieViewModel movie)
        {
            return new PostMovieCommand().Add(movie);
        }

        public MovieViewModel UpdateMovie(Guid movieId, MovieViewModel movie)
        {
            return new PutMovieCommand().Update(movieId, movie);
        }

        public MovieViewModel DeleteMovie(Guid movieId)
        {
            return new DeleteMovieCommand().Delete(movieId);
        }
    }
}