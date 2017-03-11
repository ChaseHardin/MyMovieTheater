using System;
using System.Configuration;
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

        [Then(@"the status should be (.*)")]
        public void ThenTheStatusShouldBe(int expected)
        {
            Assert.AreEqual(expected, HttpContext.Get().StatusCode, "Incorrect HTTP status code");
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