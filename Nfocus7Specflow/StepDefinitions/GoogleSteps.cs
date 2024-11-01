using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using Nfocus7Specflow.Support;

namespace Nfocus7Specflow.StepDefinitions
{
    [Binding] //This class contains step definitions
    public class GoogleSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private string someCapturedValue;
        private IWebDriver _driver;
        private readonly WDWrapper _wdWrapper;


        public GoogleSteps(WDWrapper wdWrapper, ScenarioContext scenarioContext) //Specflow will give this class the same ScenarioCOntext as was created in Hooks
        {
            _scenarioContext = scenarioContext;
            //_driver = (IWebDriver)_scenarioContext["webdriver"]; //Casts object from ScenarioContext back in to IWebDriver
            //_driver = (IWebDriver)scenarioContext["notawebdriver"]; //Casts object from ScenarioContext back in to IWebDriver --BUT that object was a String. Will "go bang" at run time. Compiler can't pre-warn us of that.
            _driver = wdWrapper.Driver; //wdWrapper *cannot* contain anything other than an IWebDriver instance. It *can't* accidentally hold just a string. It might be null though...
        }

        [Given(@"That I have navigated to Google")] //Allow alternative phrasing for the same actions with multiple annotations
        [Given(@"(?:I|i) am on the (?i)Google(?-i) Homepage")] //Allow for uppercase/lowercase variations
        public void SOmeOtherName()//GivenIAmOnTheGoogleHomepage()
        {
            _driver.Url = "https://www.google.co.uk/";
            //Accept cookie prompt
            _driver.FindElement(By.CssSelector("#L2AGLb > div")).Click();
            
            someCapturedValue = "something";
        }

        [When(@"I search for '(.*)'")] //Annotation will only match if the step is a When
        public void WhenISearchForEdgewords(string searchText)
        {
            Console.WriteLine(someCapturedValue);
            _driver.FindElement(By.CssSelector("#APjFqb")).SendKeys(searchText + Keys.Enter);
        }




        


    }
}