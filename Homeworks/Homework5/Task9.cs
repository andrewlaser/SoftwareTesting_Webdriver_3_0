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
    public class Task9
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


        [Test, Description("1) на странице http://localhost/litecart/admin/?app=countries&doc=countries а) проверить, что страны расположены в алфавитном порядке б) для тех стран, у которых количество зон отлично от нуля -- открыть страницу этой страны и там проверить, что зоны расположены в алфавитном порядке")]
        public void VerifySortedCountries()
        {
            //Login and Navigate to http://localhost/litecart/admin/?app=countries&doc=countries
            _driver.Url = _hostName + @"/litecart/admin/";
            _driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//button[@name='login']")).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@title='Logout']")));
            _driver.Url = _hostName + @"/litecart/admin/?app=countries&doc=countries";



            /* a) Verify countries order */
            var countryRowsList = _driver.FindElements(By.XPath("//tr[@class='row']"));
            List<string> countriesActuaList = new List<string>();
            List<string> countriesWithGeoZonesList = new List<string>();

            foreach (var countryObject in countryRowsList)
            {
                string countryName = countryObject.FindElement(By.XPath(".//td[5]/a")).Text;
                countriesActuaList.Add(countryName);

                //countries with multiple geo zones
                string geoZones = countryObject.FindElement(By.XPath(".//td[6]")).Text;
                if (geoZones.Trim() != "0")
                {
                    countriesWithGeoZonesList.Add(countryName);
                }
            }

            var countriesExpectedList = new List<string>(countriesActuaList);
            countriesExpectedList.Sort();
            Assert.True(countriesExpectedList.SequenceEqual(countriesActuaList), "Countries are sorted alphabetically");

            /* b) Go through countries with multiple geo zones */
            foreach (var country in countriesWithGeoZonesList)
            {
                var countryLink = _driver.FindElement(By.XPath($"//a[contains(text(),'{country}')]"));
                countryLink.Click();

                //get list of zones
                var zoneObjectsList = _driver.FindElements(By.XPath("//table[@id='table-zones']//td[./input[contains(@name,'][name]')]]"));
                var zonesActualList = zoneObjectsList.Select(zoneObject => zoneObject.Text).ToList();

                //sort
                var zonesExpectedList = new List<string>(zonesActualList);
                zonesExpectedList.Sort();

                Assert.True(zonesExpectedList.SequenceEqual(zonesActualList), "Time zones are sorted alphabetically");
                _driver.Navigate().Back();
            }

        }


        [Test, Description("2) на странице http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones зайти в каждую из стран и проверить, что зоны расположены в алфавитном порядке")]
        public void VerifySortedGeoZones()
        {
            //Login and Navigate to http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones
            _driver.Url = _hostName + @"/litecart/admin/";
            _driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");
            _driver.FindElement(By.XPath("//button[@name='login']")).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@title='Logout']")));
            _driver.Url = _hostName + @"/litecart/admin/?app=geo_zones&doc=geo_zones";

            
            int countryRowsCount = _driver.FindElements(By.XPath("//table[@class='dataTable']//a[not(@title)]")).Count;
            /* Go through all countries */
            for(int i = 0; i<countryRowsCount; i++)
            {
                var countryLink = _driver.FindElements(By.XPath("//table[@class='dataTable']//a[not(@title)]"))[i];
                countryLink.Click();

                //get list of zones
                var zoneObjectsList = _driver.FindElements(By.XPath("//table[@id='table-zones']//select[contains(@name,'[zone_code]')]"));
                List<string> zonesActualList = new List<string>();
                //add all timezones to the list
                foreach (var zoneObject in zoneObjectsList)
                {
                    SelectElement dropDown = new SelectElement(zoneObject);
                    zonesActualList.Add(dropDown.SelectedOption.Text);
                }

                //sort
                var zonesExpectedList = new List<string>(zonesActualList);
                zonesExpectedList.Sort();
                //assert
                Assert.True(zonesExpectedList.SequenceEqual(zonesActualList), "Time zones are sorted alphabetically");
                _driver.Navigate().Back();
            }

        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;

        }
    }
}
