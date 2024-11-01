using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Nfocus7Specflow.Support;
[assembly: Parallelizable(ParallelScope.Fixtures)] //Can only parallelise Features
[assembly: LevelOfParallelism(8)] //Worker thread i.e. max amount of Features to run in Parallel

namespace Nfocus7Specflow.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private IWebDriver _driver; //field to share driver between class methods
        private readonly WDWrapper _wdWrapper;
        public Hooks(WDWrapper wdWrapper, FeatureContext featureContext,ScenarioContext scenarioContext) //Constructor will be run by Specflow when it instantiates this class to use the [Before] step. When it does that it will create a ScenarioCOntext object. Other step classes that need a scenario context in their constructor will use the same ScenarioContext instance.
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _wdWrapper = wdWrapper; //WDWrapper will be instanticated and passed in by Specflow
        }

        [Before] //Similar to NUnit [SetUp]
        public void SetUp()
        {
           

            string browser = Environment.GetEnvironmentVariable("browser") ?? "chrome";

            switch (browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "chrome":
                    _driver = new ChromeDriver();
                    break;

                default:
                    Assert.Fail("No browser set");
                    break;
            }


            //_scenarioContext.Add("webdriver", _driver); //Javaish
            //_scenarioContext["alsowebdriver"] = _driver; //C#ish
            //_scenarioContext["notawebdriver"] = "not a webdriver"; //You can put *anything* in ScenarioContext
            //A comment
            _wdWrapper.Driver = _driver; //Typesafe storage of WebDriver
            //_wdWrapper.Driver = "not a webdriver"; //Wont compile. A string is not a IWebDriver.
        }
        [After] //Similar to NUnit [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
