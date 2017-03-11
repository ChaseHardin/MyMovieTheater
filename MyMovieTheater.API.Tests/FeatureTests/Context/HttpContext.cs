using RestSharp;
using TechTalk.SpecFlow;

namespace MyMovieTheater.API.Tests.FeatureTests.Context
{
    public class HttpContext
    {
        public IRestResponse Response { get; set; }

        public static HttpContext Get()
        {
            HttpContext ctx;
            return ScenarioContext.Current.TryGetValue(out ctx) ? ctx : NewContext();
        }

        private static HttpContext NewContext()
        {
            var ctx = new HttpContext();
            ScenarioContext.Current.Set(ctx);
            return ctx;
        }
    }
}