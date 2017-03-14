using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMovieTheater.Business.Services;
using MyMovieTheater.Business.ViewModels;
using MyMovieTheater.Data;
using MyMovieTheater.Data.Models;

namespace MyMovieTheater.Business.Tests.Services.MovieServiceTests
{
    [TestClass]
    public class MoveServicePostTests
    {
        private MovieService _movieService;

        [TestInitialize]
        public void Initialize()
        {
            _movieService = new MovieService();
        }

        [TestMethod]
        public void AddMovie_ShouldAddNewMovieRecord()
        {
            var initialCount = GetMovies().Count;
            var title = Guid.NewGuid().ToString();

            var movieViewModel = new MovieViewModel
            {
                Name = title,
                MovieTimes = new List<MovieTime>(),
                Rating = "PG-13",
                ReleaseDate = DateTime.Now,
                TicketPrice = 8.85m
            };

            _movieService.AddMovie(movieViewModel);

            var actual = GetMovieByTitle(title);
            Assert.AreEqual(initialCount + 1, GetMovies().Count);
            Assert.AreEqual(movieViewModel.Name, actual.Name);
            Assert.AreEqual(movieViewModel.Rating, actual.Rating);
            Assert.AreEqual(movieViewModel.TicketPrice, actual.TicketPrice);
        }

        private Movie GetMovieByTitle(string title)
        {
            using (var db = Application.GetDatabaseInstance())
            {
                return db.Movies.First(x => x.Name == title);
            }
        }

        private static List<Movie> GetMovies()
        {
            using (var db = Application.GetDatabaseInstance())
            {
                return db.Movies.ToList();
            }
        }
    }
}