using System;
using System.Configuration;
using MyMovieTheater.API.Tests.FeatureTests.Context;
using RestSharp;
using TechTalk.SpecFlow;

namespace MyMovieTheater.API.Tests.FeatureTests.Steps
{
    [Binding]
    public sealed class HttpSteps
    {
        public static readonly string MyMovieTheaterServerUrl = ConfigurationManager.AppSettings["MyMovieTheaterServerUrl"];

        [When(@"I GET '(.*)'")]
        public void WhenIGET(string url)
        {
            ExecuteHttp(Method.GET, url, "");
        }

        private void ExecuteHttp(Method method, string url, string bodyString)
        {
            var request = new RestRequest(PrepareUrl(url), method);
            request.UseDefaultCredentials = true;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("ContentType", "applicationJson");
            request.AddParameter("application/json", MyMovieTheaterFeatureContext.Get().SubstituteKeys(bodyString), ParameterType.RequestBody);

            var response = new RestClient(MyMovieTheaterServerUrl).Execute(request);
            ExtractResponse(response);
        }

        private void ExtractResponse(IRestResponse response)
        {
            HttpContext.Get().Response = response;
        }

        private string PrepareUrl(string url)
        {
            var resource = MyMovieTheaterFeatureContext.Get().SubstituteKeys(url);
            Console.WriteLine("URL: " + resource);
            return resource;
        }
    }
}