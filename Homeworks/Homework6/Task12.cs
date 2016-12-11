
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Homework6
{
    [TestFixture]
    public class Task12
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private const string _hostName = "http://192.168.1.151";
        //private const string _hostName = "http://localhost";

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }


        [Test, Description("Задание 12. Сделайте сценарий добавления товара")]
        public void AddProductInAdmin()
        {
            //constants
            string timestampStr = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string productNameStr = "product " + timestampStr;
            string selectCategoryStr = "Rubber Ducks";

            //Login and Navigate to http://localhost/litecart/admin/?app=countries&doc=countries
            _driver.Url = _hostName + @"/litecart/admin/";
            _driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//button[@name='login']")).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@title='Logout']")));

            //Navigate to Add new product form
            _driver.FindElement(By.XPath("//a[./span[contains(text(),'Catalog')]]")).Click();
            _driver.FindElement(By.XPath("//a[contains(text(),'Add New Product')]")).Click();

            //Fill in Add New Product form
            _driver.FindElement(By.XPath("//input[@name='status' and @value='1']")).Click();
            _driver.FindElement(By.XPath("//input[contains(@name, 'name[')]")).SendKeys(timestampStr);
            SelectCategories(new List<string>() {selectCategoryStr});
            SelectElement defaultCategoryElement = new SelectElement(_driver.FindElement(By.Name("default_category_id")));
            defaultCategoryElement.SelectByText(selectCategoryStr);
            //TODO finish





        }

        /// <summary>
        /// Select categories from the list
        /// </summary>
        /// <param name="categoryNames"></param>
        /// <param name="isChecked"></param>
        public void SelectCategories(List<string> categoryNames)
        {
            foreach (var categoryName in categoryNames)
            {
                var checkbox =
                    _driver.FindElement(By.XPath($"//input[@name='categories[]' and @data-name='{categoryName}']"));
                SetCheckboxValue(checkbox, true);
            }
        }

        public void SetCheckboxValue(IWebElement element, bool isChecked)
        {
            if(element.Selected != isChecked)
                element.Click();
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;

        }
    }
}
