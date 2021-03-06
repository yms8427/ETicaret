using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Yms.UI.Tests
{
    [TestFixture]
    public class MainAppTestFixture
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions { PageLoadStrategy = PageLoadStrategy.Eager };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://localhost:6001");
            driver.Manage().Window.Maximize();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        [Test]
        public void OpenHomePage()
        {
            Assert.AreEqual("YM'S Alışveriş Dünyası - Alışveriş Dünyası", driver.Title);
        }

        [Test]
        public void AddToCart()
        {
            driver.Find(By.Id("lnkLogin")).Click();
            //Login Page
            driver.Find(By.Id("UserName")).SendKeys("can.perk");
            driver.Find(By.Id("Password")).SendKeys("123");
            driver.Find(By.Id("login")).Click();
            //Home Page
            //Click "Elektronik"
            driver.Find(By.XPath("//*[@id='leftMenu']/div[2]/a")).Click();
            //il ürünü ekle
            driver.Find(By.XPath("//*[@id='productlist']/div[3]/div[1]/div/div/div[2]/ul/li[2]/a")).Click();
            //sepete git
            driver.Find(By.Id("lnkSepet")).Click();
            driver.Navigate().Refresh();
            var span = driver.Find(By.XPath("//*[@id='navbarSupportedContent']/ul[2]/li[1]/a/small"));
            Assert.AreEqual("(1)", span.Text);
        }

        [Test]
        public void InvalidLogin()
        {
            driver.Find(By.Id("lnkLogin")).Click();
            driver.Find(By.Id("UserName")).SendKeys("unknowuser");
            driver.Find(By.Id("Password")).SendKeys("unknowuserpassword");
            driver.Find(By.Id("login")).Click();
            var alert = driver.Find(By.ClassName("alert-danger"));
            Assert.AreEqual("Kullanıcı adı veya şifre yanlış", alert.Text);
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }

    public static class WebDriverExtensions
    {
        public static IWebElement Find(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(by));
        }
    }
}