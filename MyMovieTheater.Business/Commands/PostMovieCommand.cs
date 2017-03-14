using System;
using AutoMapper;
using MyMovieTheater.Business.ViewModels;
using MyMovieTheater.Data;
using MyMovieTheater.Data.Models;

namespace MyMovieTheater.Business.Commands
{
    public class PostMovieCommand
    {
        public MovieViewModel Add(MovieViewModel movie)
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