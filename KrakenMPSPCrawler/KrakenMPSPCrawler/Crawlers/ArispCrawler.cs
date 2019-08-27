using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using KrakenMPSPCrawler.Enum;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArispCrawler : Crawler
    {
        private KindPerson type;
        private String identification;

        public ArispCrawler(KindPerson type, String identification)
        {
            this.type = type;
            this.identification = identification;
        }

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
                    Actions builder = new Actions(driver);
                    var menuDropDown = driver.FindElement(By.Id("liInstituicoes"));
                    builder.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.CssSelector("#liInstituicoes > div > ul > li:nth-child(3) > a")).Click();

                    // page 3
                    driver.FindElement(By.Id("Prosseguir")).Click();

                    // page 4
                    driver.FindElement(By.CssSelector("div.selectorAll div.checkbox input")).Click();
                    var botaoTermos = driver.FindElement(By.Id("chkHabilitar"));
                    builder.MoveToElement(botaoTermos).Build().Perform();
                    botaoTermos.Click();
                    driver.FindElement(By.Id("Prosseguir")).Click();

                    // page 5
                    driver.FindElement(By.Id("btnPesquisar")).Click();

                    if (this.type.Equals(KindPerson.LegalPerson))
                    {
                        var campoFilter = new SelectElement(driver.FindElement(By.Id("filterTipo")));
                        campoFilter.SelectByValue("Pessoa Jurídica");
                    }
                    IWebElement campoBusca = driver.FindElement(By.Id("filterDocumento"));
                    campoBusca.SendKeys(this.identification);
                    driver.FindElement(By.Id("btnPesquisar")).Click();
                    

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
