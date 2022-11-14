using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriverTask.TestCases
{
    internal class TestCase1
    {
        IWebDriver driver;
        string url = "https://demo.guru99.com/test/guru99home/";

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\UISANLE\\Downloads\\.Net Selenium Training\\chromedriver_win32");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
        }

        [Test, Order(1)]
        public void VerifyPage()
        {
            driver.Navigate().GoToUrl(url);
            
            Assert.AreEqual("Demo Guru99 Page", driver.Title);
        }

        [Test, Order(2)]
        public void SubmitEmail()
        {
            driver.Navigate().GoToUrl(url);

            IWebElement Email = driver.FindElement(By.XPath("//*[@id='philadelphia-field-email']"));
            IWebElement SubmitBtn = driver.FindElement(By.XPath("//*[@id='philadelphia-field-submit']"));

            Email.Clear();
            Email.SendKeys("abc123@gmail.com");
            SubmitBtn.Click();

            IAlert AlertBox = driver.SwitchTo().Alert();
            string AlertText = AlertBox.Text;
            Console.WriteLine(AlertText);
            Assert.AreEqual("Form Submitted Successfully...", AlertText);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
            AlertBox.Accept();
        }

        [Test, Order(3)]
        public void SelectProject()
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            IWebElement InsProjectTab = driver.FindElement(By.XPath("//*[@id='navbar-brand-centered']/ul/li[2]/a"));
            InsProjectTab.Click();

            Assert.AreEqual("Insurance Broker System - Login", driver.Title);
        }

        [Test, Order(4)]
        public void SelectCourse()
        {
            driver.Navigate().GoToUrl(url);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            IWebElement Category = driver.FindElement(By.XPath("//li[@class='item118 parent']"));
            IWebElement Course = driver.FindElement(By.XPath("//li[@class='item121']"));

            Actions action = new Actions(driver);
            action.MoveToElement(Category).Perform();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//li[@class='item121']")));

            action.MoveToElement(Course).Click().Perform();

            Assert.AreEqual("Selenium Tutorial", driver.Title);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
