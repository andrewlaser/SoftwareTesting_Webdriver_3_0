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
    public class Task7
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
        public void GoThroughLeftMenu()
        {
            //Login
            _driver.Url = _hostName + @"/litecart/admin/";
            _driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//button[@name='login']")).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@title='Logout']")));

            
            int itemsCount = _driver.FindElements(By.Id("app-")).Count;
            //go through all item
            for (int i = 0; i < itemsCount; i++)
            {
                var items = _driver.FindElements(By.Id("app-"));
                var item = items[i];
                item.Click();
                Assert.True(IsElementVisible(By.XPath("//h1")), "Header is visible");

                //go through all subitems
                var subItemsCount = _driver.FindElements(By.XPath("//ul[@class='docs']//a")).Count;
                for (int j = 0; j < subItemsCount; j++)
                {
                    var subItems = _driver.FindElements(By.XPath("//ul[@class='docs']//a"));
                    var subItem = subItems[j];
                    subItem.Click();
                    Assert.True(IsElementVisible(By.XPath($"//h1")), $"Page Header is visible");
                }

            }
        }

        private bool IsElementVisible(By locator)
        {
            var elements = _driver.FindElements(locator);
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
