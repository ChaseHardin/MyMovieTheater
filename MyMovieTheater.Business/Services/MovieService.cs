using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyMovieTheater.Business.ViewModels;
using MyMovieTheater.Data;
using MyMovieTheater.Data.Models;

namespace MyMovieTheater.Business.Services
{
    public class MovieService : BaseService
    {
        public List<MovieViewModel> GetMovies()
        {
            using (var db = Application.GetDatabaseInstance())
            {
                return db.Movies.Select(Mapper.Map<MovieViewModel>).ToList();
            }
        }

        public MovieViewModel CreateMovie(MovieViewModel movie)
        {
            using (var db = Application.GetDatabaseInstance())
            {
                movie.MovieId = Guid.NewGuid();
                db.Movies.Add(Mapper.Map<MovieViewModel, Movie>(movie));
                db.SaveChanges();
            }

            return movie;
        }
    }
}