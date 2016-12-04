using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Homework4
{
    [TestFixture]
    public class Task8
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        //private const string _hostName = "http://192.168.1.151";
        private const string _hostName = "http://localhost";

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }


        [Test, Description("")]
        public void VerifyStickers()
        {
            _driver.Url = _hostName + "/litecart/en/";
            var goodsList = _driver.FindElements(By.CssSelector(".image-wrapper"));
            foreach (var item in goodsList)
            {
                int stickersCount = item.FindElements(By.CssSelector(".sticker")).Count;
                Assert.AreEqual(1, stickersCount);
            }
        }

        private bool IsElementVisible(By locator)
        {
            var elements = _driver.FindElements(locator);
            return (elements.Count > 0 && elements[0].Displayed);
        }

        private bool IsElementVisible(By locator, IWebElement parent)
        {
            var elements = parent.FindElements(locator);
            return (elements.Count > 0 && elements[0].Displayed);
        }


        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;

        }
    }
}
