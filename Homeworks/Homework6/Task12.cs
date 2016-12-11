
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
            CheckProductGroups(new List<string>() {"Male"});
            _driver.FindElement(By.Name("quantity")).SendKeys("5");
            new SelectElement(_driver.FindElement(By.Name("quantity_unit_id"))).SelectByText("pcs");
            new SelectElement(_driver.FindElement(By.Name("delivery_status_id"))).SelectByText("3-5 days");
            new SelectElement(_driver.FindElement(By.Name("sold_out_status_id"))).SelectByText("Sold out");
            AttachFile(_driver.FindElement(By.Name("new_images[]")), @"D:\Trainings\WebDriver3_0\GitHub\SoftwareTesting_Webdriver_3_0\Homeworks\Homework5\Task9.cs");
            _driver.FindElement(By.Name("date_valid_from")).SendKeys("2016-12-10");
            _driver.FindElement(By.Name("date_valid_to")).SendKeys("2026-12-10");

            _driver.FindElement(By.XPath("//button[@value='Save']")).Click();

            //TODO finish





        }

        public void AttachFile(IWebElement element, string path)
        {
            Unhide(element);
            element.SendKeys(path);
        }
        
        /// <summary>
        /// Unhide invisible element
        /// e.g. to attach a file to invisible field
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public void Unhide(IWebElement element)
        {
            String script = @"arguments[0].style.opacity=1;"
              + "arguments[0].style['transform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['MozTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['WebkitTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['msTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['OTransform']='translate(0px, 0px) scale(1)';"
              + "return true;";
            (_driver as IJavaScriptExecutor).ExecuteScript(script, element);
        }

        public void CheckProductGroups(List<string> productGroups)
        {
            foreach (var productgroup in productGroups)
            {
                IWebElement element =_driver.FindElement(By.XPath($"//tr[./td[contains(text(),'{productgroup}')]]//input[@type='checkbox']"));
                SetCheckboxValue(element, true);
            }
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
