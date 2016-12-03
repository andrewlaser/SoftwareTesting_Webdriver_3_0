using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Homework3
{
    [TestFixture]
    [Parallelizable]
    public class StartFirefox2
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new FirefoxDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(100));
        }

        [Test]
        public void StartFirefoxTest2()
        {
            _driver.Url = "http://edition.cnn.com/";

        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }

    }
}
