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
    public class MovieServiceGetTests
    {
        private MovieService _movieService;

        [TestInitialize]
        public void Initialize()
        {
            _movieService = new MovieService();
        }

        [TestMethod]
        public void GetMovies_ShouldReturnAllMovies()
        {
            var initialCount = GetMovies().Count;
            AddMovie();

            var result = _movieService.GetMovies();
            Assert.AreEqual(initialCount + 1, result.Count);
        }

        private static List<Movie> GetMovies()
        {
            using (var db = Application.GetDatabaseInstance())
            {
                return db.Movies.ToList();
            }
        }

        private static void AddMovie()
        {
            using (var db = Application.GetDatabaseInstance())
            {
                var movie = Movie1234.Build(Guid.NewGuid());

                db.Movies.Add(movie);
                db.SaveChanges();
            }
        }
    }
}