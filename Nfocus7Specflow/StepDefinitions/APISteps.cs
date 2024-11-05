using System;
using TechTalk.SpecFlow;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework;

namespace Nfocus7Specflow.StepDefinitions
{
    [Binding]
    public class APISteps
    {
        private readonly ScenarioContext _scenarioContext;
        private RestClient _client;
        private RestResponse _response;

        public APISteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [When(@"I request user number '(.*)'")]
        public void WhenIRequestUserNumber(string userNumber)
        {
            //Common options for the requests can be defined on a RestClientOptions object e.g. baseUrl
            RestClientOptions clientOptions = new RestClientOptions("http://localhost:2002/api/");
            clientOptions.Authenticator = new HttpBasicAuthenticator("edge", "edgewords");
            //Create the client
            _client = new RestClient(clientOptions);


            RestRequest request = new RestRequest("users/" + userNumber, Method.Get);
          

            _response = _client.Execute(request);
            

        }

        [Then(@"I get a '(.*)' response code")]
        public void ThenIGetAResponseCode(int expectedResponseCode)
        {
            int status = (int)_response.StatusCode;
            //Assert.That(status == expectedResponseCode);
            Assert.That(status, Is.EqualTo(200), "Not OK");
            status.Should().Be(200);

        }

        [Then(@"the user is '(.*)'")]
        public void ThenTheUserIs(string expectedUsernmae)
        {
            Assert.That(_response.Content, Does.Contain(expectedUsernmae), "Wrong user");
        }
    }
}