using System;
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
        public DateTime ShowTime { get; set; }
        public decimal TicketPrice { get; set; }
        public string Rating { get; set; }
    }
}