using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyMovieTheater.Business.ViewModels;
using MyMovieTheater.Data;

namespace MyMovieTheater.Business.Commands
{
    public class GetMoviesCommand
    {
        public List<MovieViewModel> Get()
        {
            using (var db = Application.GetDatabaseInstance())
            {
                return db.Movies.Select(Mapper.Map<MovieViewModel>).ToList();
            }
        }
    }
}