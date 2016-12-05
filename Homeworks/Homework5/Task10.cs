using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Homework5
{
    [TestFixture]
    public class Task10
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


        [Test, Description("Сделайте сценарий, который проверяет, что при клике на товар открывается правильная страница товара в учебном приложении litecart.")]
        public void VerifyGoodsPage()
        {
            _driver.Url = _hostName + "/litecart/en/";
            var firstCampaignGood = _driver.FindElement(By.XPath("//div[@id='box-campaigns']//a[@class='link']"));
            string nameExpected = firstCampaignGood.FindElement(By.XPath(".//div[@class='name']")).Text;
            // Will throw "Not found" exception if the Style i.e. tag+class is wrong
            string regularPriceExpected = firstCampaignGood.FindElement(By.XPath(".//s[@class='regular-price']")).Text;
            string campaignPriceExpected = firstCampaignGood.FindElement(By.XPath(".//strong[@class='campaign-price']")).Text;
            firstCampaignGood.Click();

            string nameActual = _driver.FindElement(By.XPath("//h1[@class='title']")).Text;
            // Will throw "Not found" exception if the Style i.e. tag+class is wrong
            string regularPriceActual = _driver.FindElement(By.XPath("//s[@class='regular-price']")).Text;
            string campaignPriceActual = _driver.FindElement(By.XPath(".//strong[@class='campaign-price']")).Text;

            Assert.AreEqual(nameExpected, nameActual, $"Name on the first page {nameExpected}, doesn't match name on the product page {nameActual}");
            Assert.AreEqual(regularPriceExpected, regularPriceActual, $"regularPrice on the first page {regularPriceExpected}, doesn't match regularPrice on the product page {regularPriceActual}");
            Assert.AreEqual(campaignPriceExpected, campaignPriceActual, $"campaignPrice on the first page {campaignPriceExpected}, doesn't match campaignPrice on the product page {campaignPriceActual}");
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;

        }
    }
}
