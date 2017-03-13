using System.Web.Http;
using MyMovieTheater.Business.Services;

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
    }
}
