using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

        public MovieViewModel AddMovie(MovieViewModel movie)
        {
            using (var db = Application.GetDatabaseInstance())
            {
                movie.MovieId = Guid.NewGuid();
                db.Movies.Add(Mapper.Map<MovieViewModel, Movie>(movie));
                db.SaveChanges();
            }

            return movie;
        }

        public MovieViewModel UpdateMovie(Guid movieId, MovieViewModel movie)
        {
            using (var db = Application.GetDatabaseInstance())
            {
                db.Movies.AddOrUpdate(Mapper.Map<MovieViewModel, Movie>(movie));
                db.SaveChanges();
            }

            return movie;
        }
    }
}