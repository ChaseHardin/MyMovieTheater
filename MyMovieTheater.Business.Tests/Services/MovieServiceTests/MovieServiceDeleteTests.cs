using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMovieTheater.Business.Services;
using MyMovieTheater.Data;
using MyMovieTheater.Data.Models;
using MyMovieTheater.Test.Data.MyMovieTheater.Movie;

namespace MyMovieTheater.Business.Tests.Services.MovieServiceTests
{
    [TestClass]
    public class MovieServiceDeleteTests
    {
        private MovieService _movieService;

        [TestInitialize]
        public void Initialize()
        {
            _movieService = new MovieService();
        }

        [TestMethod]
        public void DeleteMovie_ShouldDeleteMovie()
        {
            var addMovie = AddMovie(Guid.NewGuid());
            var initialCount = GetMovies().Count;

            var actual = _movieService.DeleteMovie(addMovie.MovieId);
            Assert.AreEqual(initialCount - 1, GetMovies().Count);
            Assert.AreEqual(addMovie.MovieId, actual.MovieId);
            Assert.AreEqual(addMovie.Name, actual.Name);
            Assert.AreEqual(addMovie.TicketPrice, actual.TicketPrice);
            Assert.AreEqual(addMovie.ShowTime, actual.ShowTime);
            Assert.AreEqual(addMovie.Rating, actual.Rating);
        }

        private List<Movie> GetMovies()
        {
            using (var db = Application.GetDatabaseInstance())
            {
                return db.Movies.ToList();
            }
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
    }
}
