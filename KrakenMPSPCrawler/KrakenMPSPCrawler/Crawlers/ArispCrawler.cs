using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArispCrawler : Crawler
    {
        public override CrawlerStatus Execute()
        {           
            searchPortal();
            return CrawlerStatus.Success;
        }

        private void searchPortal()
        {
            using (var driver = new FirefoxDriver())
            {
                driver.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
                var link = driver.FindElement(By.PartialLinkText("TFS Test API"));
                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
                clickableElement.Click();
            }

            /*_driver = new PhantomJSDriver();
            phatom.Run()
            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            /*using (var driver = new PhantomJSDriver())
            {
                const string phantomScript = "var page=this;page.onResourceRequested = function(requestData, request) { var reg =  /\\.png/gi; var isPng = reg.test(requestData['url']); console.log(isPng,requestData['url']); if (isPng){console.log('Aborting: ' + requestData['url']);request.abort();}}";
                var script = driver.ExecutePhantomJS(phantomScript);
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arisp/login.html");
                driver.GetScreenshot().SaveAsFile("googlewithoutimage.png", ImageFormat.Png);
            }

            _driver.Navigate().GoToUrl("");

            // passo 1
            IWebElement loginButton = _driver.FindElement(By.Name("btnCallLogin"));
            loginButton.Click();

            _driver.GetScreenshot().SaveAsFile("Arisp.png");

            using (var driver = new ChromeDriver(outPutDirectory))
            {
                // Go to the home page
                driver.Navigate().GoToUrl("");

                // passo 1
                var loginButton = driver.FindElementById("btnCallLogin");
                loginButton.Click();

                // passo 2
                var autenticationButton = driver.FindElementById("btnAutenticar");
                autenticationButton.Click();
            }*/
        }

    }
}
