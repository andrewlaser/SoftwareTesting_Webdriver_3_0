using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Homework2
{
    [TestFixture]
    public class SimpleLogin
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        //private const string _hostName = "http://192.168.1.151";
        private const string _hostName = "http://localhost";

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(100));
        }


        [Test]
        public void Login()
        {
            _driver.Url = _hostName + @"/litecart/admin/";
            _driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//button[@name='login']")).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@title='Logout']")));

        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;

        }
    }
}
