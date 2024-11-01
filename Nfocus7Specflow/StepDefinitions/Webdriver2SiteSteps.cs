using Nfocus7Specflow.POMs;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using static System.Net.WebRequestMethods;

namespace Nfocus7Specflow.StepDefinitions
{
    [Binding]
    public class Webdriver2SiteSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public Webdriver2SiteSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["webdriver"];
        }
        [Given(@"That I am on the login page")]
        public void GivenThatIAmOnTheLoginPage()
        {
            _driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";
            HomePagePOM home = new(_driver);
            home.GoLogin();

        }

        [When(@"I use the username ""(.*)"" and password ""(.*)"" to log in")]
        public void WhenIUseTheUsernameAndPasswordToLogIn(string username, string password)
        {
            AuthPagePOM auth = new(_driver);
            auth.LoginExpectSuccess(username, password);
        }

        [Then(@"I am logged in")]
        public void ThenIAmLoggedIn()
        {
            AddRecordPOM addRecordPOM = new(_driver);
            string bodyText = addRecordPOM.bodyText;

            Assert.That(bodyText, Does.Contain("User is Logged in"));

        }
    }
}