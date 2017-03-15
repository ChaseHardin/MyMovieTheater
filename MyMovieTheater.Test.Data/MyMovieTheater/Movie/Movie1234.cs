using System;

namespace MyMovieTheater.Test.Data.MyMovieTheater.Movie
{
    public class Movie1234
    {
        public static global::MyMovieTheater.Data.Models.Movie Build(Guid movieId)
        {
            return new global::MyMovieTheater.Data.Models.Movie
            {
                MovieId = movieId,
                TicketPrice = 8.95m,
                Rating = "R",
                Name = "Batman",
                ShowTime = TestClock.Date("2017-02-18")
            };
        }
    }
}