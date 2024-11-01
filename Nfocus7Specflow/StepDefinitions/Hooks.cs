using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfocus7Specflow.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private IWebDriver _driver; //field to share driver between class methods

        public Hooks(FeatureContext featureContext,ScenarioContext scenarioContext) //Constructor will be run by Specflow when it instantiates this class to use the [Before] step. When it does that it will create a ScenarioCOntext object. Other step classes that need a scenario context in their constructor will use the same ScenarioContext instance.
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Before] //Similar to NUnit [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _scenarioContext.Add("webdriver", _driver); //Javaish
            //_scenarioContext["alsowebdriver"] = _driver; //C#ish
            _scenarioContext["notawebdriver"] = "not a webdriver";
            //A comment
        }
        [After] //Similar to NUnit [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
