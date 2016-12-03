using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace Homework3
{
    [TestFixture]
    [Parallelizable]
    public class StartIE11
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.RequireWindowFocus = true;
            _driver = new InternetExplorerDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(100));
        }

        [Test]
        public void StartIe11Test()
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
