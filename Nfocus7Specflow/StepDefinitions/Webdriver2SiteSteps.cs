using Nfocus7Specflow.POMs;
using Nfocus7Specflow.Support;
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
        private readonly WDWrapper _wdWrapper;
        public Webdriver2SiteSteps(WDWrapper wdWrapper, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            //_driver = (IWebDriver)_scenarioContext["webdriver"];
            _driver = wdWrapper.Driver; //wdWrapper *cannot* contain anything other than an IWebDriver instance. It *can't* accidentally hold just a string. It might be null though...
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