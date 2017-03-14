using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMovieTheater.Business.Services;
using MyMovieTheater.Business.ViewModels;
using MyMovieTheater.Data;
using MyMovieTheater.Data.Models;
using MyMovieTheater.Test.Data;
using MyMovieTheater.Test.Data.MyMovieTheater.Movie;

namespace MyMovieTheater.Business.Tests.Services.MovieServiceTests
{
    [TestClass]
    public class MovieServicePutTests
    {
        private MovieService _movieService;

        [TestInitialize]
        public void Initialize()
        {
            _movieService = new MovieService();
        }

        [TestMethod]
        public void UpdateMovie_ShouldUpdateNewRecordAndMapProperties()
        {
            var addMovie = AddMovie(Guid.NewGuid());
            var initialCount = GetMovies().Count;
            var movieViewModel = new MovieViewModel
            {
                Name = "Updated Name",
                ReleaseDate = TestClock.Now,
                TicketPrice = 55.5m,
                Rating = "PG-13",
                MovieId = addMovie.MovieId
            };

            var actual = _movieService.UpdateMovie(addMovie.MovieId, movieViewModel);

            Assert.AreEqual(initialCount, GetMovies().Count);
            Assert.AreEqual(movieViewModel.Name, actual.Name);
            Assert.AreEqual(movieViewModel.ReleaseDate, actual.ReleaseDate);
            Assert.AreEqual(movieViewModel.TicketPrice, actual.TicketPrice);
            Assert.AreEqual(movieViewModel.ReleaseDate, actual.ReleaseDate);
            Assert.AreEqual(movieViewModel.MovieId, actual.MovieId);
        }

        private Movie AddMovie(Guid movieId)
        {
            using (var db = Application.GetDatabaseInstance())
            {
                var movie = Movie1234.Build(movieId);
                db.Movies.Add(movie);
                db.SaveChanges();

                return movie;
            }
        }

        private List<Movie> GetMovies()
        {
            using (var db = Application.GetDatabaseInstance())
            {
                return db.Movies.ToList();
            }
        }
    }
}
