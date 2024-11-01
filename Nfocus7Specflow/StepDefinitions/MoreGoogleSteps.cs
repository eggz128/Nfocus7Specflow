using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfocus7Specflow.StepDefinitions
{
    [Binding]
    internal class MoreGoogleSteps
    {

        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;


        public MoreGoogleSteps(ScenarioContext scenarioContext) //Specflow will give this class the same ScenarioCOntext as was created in Hooks
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["webdriver"];
        }

        [Then(@"""(.*)"" is the number '(\d)' result")]
        public void ThenIsTheNumberResult(string resultText, int resultNumber)
        {
            IWebElement topResult = _driver.FindElement(By.CssSelector("div#rso div.MjjYud")); //div#rso > div:first-of-type > div.MjjYud or div.g
            topResult = _driver.FindElements(By.CssSelector("div#rso div.MjjYud"))[resultNumber - 1]; //Find element match based on index.


            string topResultText = topResult.Text;

            //Now assert
            Assert.That(topResultText, Does.Contain(resultText), "Edgewords was not the top result");
            //Fluent assertion alternative
            topResultText.Should().Contain(resultText);
        }

        [Then(@"I should see in the results")]
        public void ThenIShouldSeeInTheResults(Table table)
        {
            string results = _driver.FindElement(By.CssSelector("div#rso")).Text;

            foreach (TableRow tableRow in table.Rows)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(results, Does.Contain(tableRow["title"]));
                    Assert.That(results, Does.Contain(tableRow["url"]));
                });

            }
        }

        //Use ' ' to paramiterise with Specflow, or you get uglier regex capture groups
        [StepDefinition(@"""(.*)"" is the top result")] //Generic match Given/When/Then
        public void ThenEdgewordsIsTheTopResult(string resultText)
        {
            IWebElement topResult = _driver.FindElement(By.CssSelector("div#rso div.MjjYud")); //div#rso > div:first-of-type > div.MjjYud or div.g
            topResult = _driver.FindElements(By.CssSelector("div#rso div.MjjYud"))[0]; //Find element match based on index.


            string topResultText = topResult.Text;

            //Now assert
            Assert.That(topResultText, Does.Contain(resultText), "Edgewords was not the top result");
            //Fluent assertion alternative
            topResultText.Should().Contain(resultText);
        }
    }
}
