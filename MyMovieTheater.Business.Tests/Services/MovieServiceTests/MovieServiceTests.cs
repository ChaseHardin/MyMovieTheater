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
    public class MovieServiceTests
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

        private static void AddMovie()
        {
            using (var db = Application.GetDatabaseInstance())
            {
                var movie = new Movie
                {
                    MovieId = Guid.NewGuid(),
                    Name = "TTitle",
                    MovieTimes = new List<MovieTime>(),
                    Rating = "R",
                    ReleaseDate = DateTime.Now,
                    TicketPrice = 7.75m
                };

                db.Movies.Add(movie);
                db.SaveChanges();
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