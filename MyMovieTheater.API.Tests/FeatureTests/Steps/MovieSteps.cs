using System;
using System.Collections.Generic;
using System.Data;
using MyMovieTheater.API.Tests.FeatureTests.Context;
using MyMovieTheater.Data;
using MyMovieTheater.Data.Models;
using MyMovieTheater.Test.Data.MyMovieTheater.Movie;
using TechTalk.SpecFlow;

namespace MyMovieTheater.API.Tests.FeatureTests.Steps
{
    [Binding]
    public sealed class MovieSteps
    {
        [Given(@"movie exists")]
        public void GivenMovieExists()
        {
            var movie = Movie1234.Build(Guid.NewGuid());

            SaveMovie(movie);

            var context = MyMovieTheaterFeatureContext.Get();
            context.Movie = movie;
            context.AddSubstitute("movieId", movie.MovieId.ToString());
        }

        private static void SaveMovie(Movie movie)
        {
            using (var db = Application.GetDatabaseInstance())
            {
                db.Movies.Add(movie);
                db.SaveChanges();
            }
        }
    }
}