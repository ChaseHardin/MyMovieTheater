using System;
using System.Web.Http;
using MyMovieTheater.Business.Services;
using MyMovieTheater.Business.ViewModels;

namespace MyMovieTheater.API.Controllers.Admin
{
    [RoutePrefix("api/admin/movies")]
    public class AdminMoviesControllerController : ApiController
    {
        private readonly MovieService _service = new MovieService();

        [HttpGet, Route("")]
        public virtual IHttpActionResult Get()
        {
            return Ok(_service.GetMovies());
        }

        [HttpPost, Route("")]
        public virtual IHttpActionResult Post(MovieViewModel movie)
        {
            return Created(Request.RequestUri.PathAndQuery, _service.AddMovie(movie));
        }

        [HttpPut, Route("{movieId}")]
        public virtual IHttpActionResult Put(Guid movieId, MovieViewModel movie)
        {
            return Ok(_service.UpdateMovie(movieId, movie));
        }

        [HttpDelete, Route("{movieId}")]
        public virtual IHttpActionResult Delete(Guid movieId)
        {
            return Ok();
        }
    }
}