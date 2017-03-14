using System;
using System.Linq;
using AutoMapper;
using MyMovieTheater.Business.ViewModels;
using MyMovieTheater.Data;
using MyMovieTheater.Data.Models;

namespace MyMovieTheater.Business.Commands
{
    public class DeleteMovieCommand
    {
        public MovieViewModel Delete(Guid movieId)
        {
            using (var db = Application.GetDatabaseInstance())
            {
                var movie = db.Movies.FirstOrDefault(x => x.MovieId == movieId);
                db.Movies.Remove(movie);
                db.SaveChanges();

                return Mapper.Map<Movie, MovieViewModel>(movie);
            }
        }
    }
}