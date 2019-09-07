using System;

using OpenQA.Selenium;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium.Support.UI;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArpenspCrawler : Crawler
    {
        private readonly string NumeroProcesso;

        public ArpenspCrawler(string numeroProcesso)
        {
            NumeroProcesso = numeroProcesso;
        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/login.html");

                    // page 1
                    driver.FindElement(By.CssSelector("#main > div.container > div:nth-child(2) > div:nth-child(2) > div > a")).Click();

                    // page 2
                    //driver.FindElement(By.Id("arrumaMenu")).Click();
                    driver.FindElement(By.CssSelector("#wrapper > ul > li.item3 > a")).Click();
                    driver.FindElement(By.CssSelector("#wrapper > ul > li.item3 > ul > li:nth-child(1) > a")).Click();

                    // page 3
                    driver.FindElement(By.CssSelector("#principal > div > form > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[name='numero_processo']")).SendKeys(NumeroProcesso);

                    var campoVara = new SelectElement(driver.FindElement(By.Id("vara_juiz_id")));
                    campoVara.SelectByValue("297");

                    driver.FindElement(By.Id("btn_pesquisar")).Click();


                    driver.Close();
                    Console.WriteLine("ArpenspCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("ArpenspCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e.Message);
                SetErrorMessage("ArpenspCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
