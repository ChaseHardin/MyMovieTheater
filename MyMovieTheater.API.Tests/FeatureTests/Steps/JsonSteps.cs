using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMovieTheater.API.Tests.FeatureTests.Context;
using MyMovieTheater.API.Tests.FeatureTests.Infrastructure;
using TechTalk.SpecFlow;

namespace MyMovieTheater.API.Tests.FeatureTests.Steps
{
    [Binding]
    public sealed class JsonSteps
    {
        private static readonly JsonGetter JsonGetter = new JsonGetter(new Dictionary<string, string>
        {
            
        });

        [Then(@"the JSON at '(.*)' should be '(.*)'")]
        public void ThenTheJsonAtShouldBe(string jsonSelector, string expected)
        {
            var expectedWithSubstitutions = MyMovieTheaterFeatureContext.Get().SubstitueKeys(expected);
            var actual = JsonGetter.GetProperty(HttpContext.Get().Json, jsonSelector);
            Assert.AreEqual(
                expectedWithSubstitutions,
                actual.ToString(),
                string.Format("Does not equal expected value at {0}", jsonSelector));
        }
    }
}
