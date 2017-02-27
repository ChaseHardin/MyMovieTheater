using System.Web.Http;
using MyMovieTheater.Business.Services;

namespace MyMovieTheater.API.Controllers
{
    [RoutePrefix("api/admin/movies")]
    public class MoviesController : ApiController
    {
        private readonly MovieService _service = new MovieService();

        [HttpGet, Route("")]
        public virtual IHttpActionResult Get()
        {
            var movies = _service.GetMovies();
            return Ok(new { movies });
        }
    }
}
