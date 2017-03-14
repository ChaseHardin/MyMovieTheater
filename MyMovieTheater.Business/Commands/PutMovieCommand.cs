using System;
using System.Data.Entity.Migrations;
using AutoMapper;
using MyMovieTheater.Business.ViewModels;
using MyMovieTheater.Data;
using MyMovieTheater.Data.Models;

namespace MyMovieTheater.Business.Commands
{
    public class PutMovieCommand
    {
        public MovieViewModel Update(Guid movieId, MovieViewModel movie)
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