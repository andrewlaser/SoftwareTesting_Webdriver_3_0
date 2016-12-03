using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace Homework3
{
    [TestFixture]
    [Parallelizable]
    public class StartEdge
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            EdgeOptions options = new EdgeOptions();
            _driver = new EdgeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(100));
        }

        [Test]
        public void StartEdgeTest()
        {
            _driver.Url = "http://www.bbc.com";
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;

        }

    }
}
