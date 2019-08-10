using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArispCrawler : Crawler
    {
        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arisp/login.html");

                    // page 1
                    driver.FindElement(By.Id("btnCallLogin")).Click();
                    driver.FindElement(By.Id("btnAutenticar")).Click();

                    // page 2
                    // TODO: navegar pelo drop down
                    //driver.FindElement(By.CssSelector("#liInstituicoes > div > ul > li:nth-child(3) > a")).Click();

                    // page 3
                    //driver.FindElement(By.Id("Prosseguir")).Click();
                    //driver.FindElement(By.Id("td.check > input[type=checkbox]")).Click();

                    // Take a screenshot and save it into screen.png
                    //driver.GetScreenshot().SaveAsFile(@"screen.png", ImageFormat.Png);

                    Console.WriteLine("ArispCrawler OK");
                    Console.ReadKey(intercept: true);
                    return CrawlerStatus.Success;              
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("ArispCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e.Message);
                SetErrorMessage("ArispCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
