using MyMovieTheater.API.Tests.FeatureTests.Infrastructure;
using MyMovieTheater.Data.Models;
using TechTalk.SpecFlow;

namespace MyMovieTheater.API.Tests.FeatureTests.Context
{
    public class MyMovieTheaterFeatureContext
    {
        private readonly Substituter _substituter = new Substituter();

        public Movie Movie { get; set; }

        public MyMovieTheaterFeatureContext AddSubstitute(string key, string value)
        {
            _substituter.AddSubstitute(key, value);
            return this;
        }

        public string SubstitueKeys(string s)
        {
            return _substituter.SubstitueKeys(s);
        }

        private MyMovieTheaterFeatureContext()
        {
        }

        public static MyMovieTheaterFeatureContext Get()
        {
            MyMovieTheaterFeatureContext ctx;
            return ScenarioContext.Current.TryGetValue(out ctx) ? ctx : NewContext();
        }

        private static MyMovieTheaterFeatureContext NewContext()
        {
            var ctx = new MyMovieTheaterFeatureContext();
            ScenarioContext.Current.Set(ctx);
            return ctx;
        }
    }
}
