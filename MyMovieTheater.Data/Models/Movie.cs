using System;
using System.Collections.Generic;

namespace MyMovieTheater.Data.Models
{
    public class Movie
    {
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<MovieTime> MovieTimes { get; set; }
        public decimal TicketPrice { get; set; }
        public string Rating { get; set; }
    }
}