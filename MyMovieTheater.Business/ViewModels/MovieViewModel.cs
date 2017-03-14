using System;
using System.Collections.Generic;
using MyMovieTheater.Data.Models;

namespace MyMovieTheater.Business.ViewModels
{
    public class MovieViewModel
    {
        static MovieViewModel()
        {
            AutoMapper.Mapper.CreateMap<Movie, MovieViewModel>().ReverseMap();
        }

        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<MovieTime> MovieTimes { get; set; }
        public decimal TicketPrice { get; set; }
        public string Rating { get; set; }
    }
}