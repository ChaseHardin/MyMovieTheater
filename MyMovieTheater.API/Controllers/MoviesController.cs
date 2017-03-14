using System;
using System.Web.Http;
using MyMovieTheater.Business.Services;
using MyMovieTheater.Business.ViewModels;

namespace MyMovieTheater.API.Controllers
{
    [RoutePrefix("api")]
    public class MoviesController : ApiController
    {
        private readonly MovieService _service = new MovieService();

        [HttpGet, Route("movies")]
        public virtual IHttpActionResult Get()
        {
            return Ok(_service.GetMovies());
        }

        [HttpPost, Route("admin/movies")]
        public virtual IHttpActionResult Post(MovieViewModel movie)
        {
            return Created(Request.RequestUri.PathAndQuery, _service.AddMovie(movie));
        }

        [HttpPut, Route("admin/movies/{movieId}")]
        public virtual IHttpActionResult Put(Guid movieId, MovieViewModel movie)
        {
            return Ok(_service.UpdateMovie(movieId, movie));
        }

        [HttpDelete, Route("admin/movies/{movieId}")]
        public virtual IHttpActionResult Delete(Guid movieId)
        {
            return Ok();
        }
    }
}