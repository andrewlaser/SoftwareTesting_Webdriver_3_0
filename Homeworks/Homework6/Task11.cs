
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
    public class Task11
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


        [Test, Description("Задание 11. Сделайте сценарий регистрации пользователя")]
        public void RegisterUserInShop()
        {

            string timestampStr = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string emailStr = $"{timestampStr}mail@test.com";
            string passwordStr = "Password123";

            //Navigate to registration form
            _driver.Url = _hostName + "/litecart/en/";
            _driver.FindElement(By.XPath("//form[@name='login_form']//a[contains(text(),'New customers click here')]")).Click();

            //Fill registration form
            _driver.FindElement(By.Name("firstname")).SendKeys("Fname");
            _driver.FindElement(By.Name("lastname")).SendKeys("Lname");
            _driver.FindElement(By.Name("address1")).SendKeys("Address 1");
            _driver.FindElement(By.Name("postcode")).SendKeys("12345");
            _driver.FindElement(By.Name("city")).SendKeys("City");
            var countrySelect = new SelectElement(_driver.FindElement(By.Name("country_code")));
            countrySelect.SelectByValue("UA");
            _driver.FindElement(By.Name("email")).SendKeys(emailStr);
            _driver.FindElement(By.Name("phone")).SendKeys("+380671234567");
            _driver.FindElement(By.Name("password")).SendKeys(passwordStr);
            _driver.FindElement(By.Name("confirmed_password")).SendKeys(passwordStr);

            //submit registration form
            _driver.FindElement(By.Name("create_account")).Click();

            //logout
            _driver.FindElement(By.XPath("//div[@id='box-account']//a[contains(text(),'Logout')]")).Click();

            //login
            _driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(emailStr);
            _driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(passwordStr);
            _driver.FindElement(By.XPath("//button[@name='login']")).Click();

            //logout again
            _driver.FindElement(By.XPath("//div[@id='box-account']//a[contains(text(),'Logout')]")).Click();
        }


        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;

        }
    }
}
