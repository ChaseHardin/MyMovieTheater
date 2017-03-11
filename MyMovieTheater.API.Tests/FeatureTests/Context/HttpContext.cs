using System.Collections.Generic;
using RestSharp;
using TechTalk.SpecFlow;

namespace MyMovieTheater.API.Tests.FeatureTests.Context
{
    public class HttpContext
    {
        private HttpContext()
        {
        }

        public string ImpersonateUser { get; set; }
        public int StatusCode { get { return (int)Response.StatusCode; } }
        public string Json { get { return Response.Content; } }
        public object CurrentObject { get; set; }
        public IList<Parameter> Headers { get { return Response.Headers; } }
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