using System.Web.Http;
using MyMovieTheater.Business.Services;

namespace MyMovieTheater.API.Controllers.Client
{
    [RoutePrefix("api/movies")]
    public class ClientMoviesController : ApiController
    {
        private readonly MovieService _service = new MovieService();

        [HttpGet, Route("")]
        public virtual IHttpActionResult Get()
        {
            return Ok(_service.GetMovies());
        }
    }
}