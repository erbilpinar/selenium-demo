using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace SeleniumDemo
{
    class Program
    {
        public static IWebDriver driver;
        public static WebDriverWait wait;
        [Test]
        public static void Main()
        {
            try{
                System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"/home/ella/PycharmProjects");
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl(@"https://hello.raklet.com/");
                driver.Navigate().Back();
                driver.Navigate().Forward();
                driver.Navigate().Refresh();
                Console.WriteLine(driver.Title);
                Console.WriteLine(driver.CurrentWindowHandle);

                //Store the ID of the original window
                string originalWindow = driver.CurrentWindowHandle;

                //Check we don't have other windows open already
                Assert.AreEqual(driver.WindowHandles.Count, 1);

                //Click the link which opens in a new window
                driver.FindElement(By.LinkText("Start Fundraising")).Click();
                
                driver.SwitchTo().NewWindow(WindowType.Tab);    
                driver.SwitchTo().NewWindow(WindowType.Window);
                driver.Close();
                driver.SwitchTo().Window(originalWindow);

                // Get all the elements available with tag name 'p'
                IList < IWebElement > elements = driver.FindElements(By.TagName("p"));
                foreach(IWebElement e in elements) {
                System.Console.WriteLine(e.Text);

                driver.FindElement(By.LinkText("Login")).Click();

                // Store 'SearchInput' element
                IWebElement searchInput = driver.FindElement(By.Name("Email"));
                searchInput.SendKeys("selenium");

                // Clears the entered text
                searchInput.Clear();
                
                //driver.FindElement(By.Name("Email")).SendKeys("email" + Keys.Enter);

                }
            } finally {
                driver.Quit();
            }
        }
    }
}
