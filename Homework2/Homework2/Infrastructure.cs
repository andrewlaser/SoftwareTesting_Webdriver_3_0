using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Homework2
{
    [TestFixture]
    public class Infrastructure
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(100));
        }


        [Test]
        public void TestMethod1()
        {
            _driver.Url = @"http://www.bbc.com/";
            _driver.FindElement(By.Id("orb-search-q")).SendKeys("Weather");
            _driver.FindElement(By.XPath("//button[@class='se-searchbox__submit']")).Click();
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//a[contains(text(), 'BBC Weather')]")));
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;

        }
    }
}
