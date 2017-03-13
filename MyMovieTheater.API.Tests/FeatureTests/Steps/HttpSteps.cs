using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMovieTheater.API.Tests.FeatureTests.Context;
using RestSharp;
using TechTalk.SpecFlow;

namespace MyMovieTheater.API.Tests.FeatureTests.Steps
{
    [Binding]
    class HttpSteps
    {
        public static readonly string MyMovieTheaterServerUrl = ConfigurationManager.AppSettings["MyMovieTheaterServerUrl"];

        [When(@"I GET '(.*)'")]
        public void WhenWeGET(string url)
        {
            ExecuteHttp(Method.GET, url, "");
        }

        [When(@"I POST '(.*)' with the following:")]
        public void WhenIPOSTWithTheFollowing(string url, string bodyString)
        {
            ExecuteHttp(Method.POST, url, MyMovieTheaterFeatureContext.Get().SubstitueKeys(bodyString));
        }

        [Then(@"the status should be (.*)")]
        public void ThenTheStatusShouldBe(int expected)
        {
            Assert.AreEqual(expected, HttpContext.Get().StatusCode, "Incorrect HTTP status code");
        }

        [Then(@"the '(.*)' header should match regex '(.*)'")]
        public void ThenTheHeaderShouldMatchRegex(string key, string expectedRegex)
        {
            var actual = HeaderForKey(key);

            var r = new Regex(expectedRegex, RegexOptions.IgnoreCase);
            var match = r.Match(actual);

            Assert.IsTrue(match.Success, string.Format("Expected header key '{0}' with value '{1}' to match the regex '{2}'", key, actual, expectedRegex));
        }

        private static string HeaderForKey(string key)
        {
            var header = HttpContext.Get().Headers.ToList().Find(x => x.Name == key);
            if (header == null)
            {
                Assert.Fail("Expected the response headers to include '{0}' and it did not", key);
            }

            return header.Value.ToString();
        }

        private static void ExecuteHttp(Method method, string url, string bodyString)
        {
            var request = new RestRequest(PrepareUrl(url), method);
            request.UseDefaultCredentials = true;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("ContentType", "application/json");
            request.AddParameter("application/json", MyMovieTheaterFeatureContext.Get().SubstitueKeys(bodyString), ParameterType.RequestBody);
           
            var response = new RestClient(MyMovieTheaterServerUrl).Execute(request);

            ExtractResponse(response);
        }

        private static void ExtractResponse(IRestResponse response)
        {
            HttpContext.Get().Response = response;
        }

        private static string PrepareUrl(string url)
        {
            var resource = MyMovieTheaterFeatureContext.Get().SubstitueKeys(url);
            Console.WriteLine("URL: " + resource);
            return resource;
        }
    }
}