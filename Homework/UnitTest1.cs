using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Homework
{
    public class Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "http://automationpractice.com/index.php";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Manage().Window.Maximize();
        }

        public readonly string email = "siuksline321@gmail.com";
        public readonly string password = "123456789rr";
        public readonly string item = "shirt";

        [Test]
        public void TestLogin()
        {
            driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div.header_user_info > a")).Click();
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("passwd")).SendKeys(password);
            driver.FindElement(By.Id("SubmitLogin")).Click();

            IWebElement correctAccountInfo = driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div:nth-child(1) > a"));
            IWebElement signInInfo = driver.FindElement(By.CssSelector("#center_column > h1"));

            Assert.AreEqual("MY ACCOUNT", signInInfo.Text, "Sign in is incorrect");//1.	Sign in to the account
            Assert.AreEqual("Rytis S", correctAccountInfo.Text, "Account info is incorrect");//2.	Validate correct sign in
        }

        [Test]
        public void SearchItem()
        {
            driver.FindElement(By.Id("search_query_top")).SendKeys(item);
            driver.FindElement(By.CssSelector("#searchbox > button")).Click();

            IWebElement itemFound = driver.FindElement(By.CssSelector("#center_column > h1 > span.heading-counter"));
            IWebElement correctItemFound = driver.FindElement(By.CssSelector("#center_column > ul > li > div > div.right-block > h5 > a"));

            Assert.AreNotEqual("0 result has been found.", itemFound.Text, "Item(s) was not found");//3.	Search for an item in the shop
            Assert.IsTrue(correctItemFound.Text.Contains("shirt"), "Item(s) was found with the search key word(s) - shirt ");//4.	Validate that it finds the item

        }
        [Test]
        public void BuyItem()
        {
            driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div.header_user_info > a")).Click();
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("passwd")).SendKeys(password);
            driver.FindElement(By.Id("SubmitLogin")).Click();
            driver.FindElement(By.Id("search_query_top")).SendKeys(item);
            driver.FindElement(By.CssSelector("#searchbox > button")).Click();
            driver.FindElement(By.CssSelector("#center_column > ul > li > div > div.right-block > h5 > a")).Click();
            driver.FindElement(By.CssSelector("#add_to_cart > button")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);//need wait antill Pop Up loads
            driver.FindElement(By.CssSelector("#layer_cart > div.clearfix > div.layer_cart_cart.col-xs-12.col-md-6 > div.button-container > a")).Click();
            driver.FindElement(By.CssSelector("#center_column > p.cart_navigation.clearfix > a.button.btn.btn-default.standard-checkout.button-medium")).Click();
            driver.FindElement(By.CssSelector("#center_column > form > p > button")).Click();
            driver.FindElement(By.Id("cgv")).Click();
            driver.FindElement(By.CssSelector("#form > p > button")).Click();
            driver.FindElement(By.CssSelector("#HOOK_PAYMENT > div:nth-child(2) > div > p > a")).Click();
            driver.FindElement(By.CssSelector("#cart_navigation > button")).Click();

            IWebElement orderConfirmation = driver.FindElement(By.CssSelector("#center_column > h1"));
            IWebElement itemBought = driver.FindElement(By.CssSelector("#center_column > p.alert.alert-success"));

            Assert.AreEqual("ORDER CONFIRMATION", orderConfirmation.Text, "Order Confirmed");//5.	Finish buying the item
            Assert.AreEqual("Your order on My Store is complete.", itemBought.Text, "Item bought confirmed");//6.	Validate that the order is completed






        }
    }
}
