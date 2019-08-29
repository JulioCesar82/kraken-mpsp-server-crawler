using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using KrakenMPSPCrawler.Enum;
using System.Threading;

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
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Chrome))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arisp/login.html");

                    // page 1
                    driver.FindElement(By.Id("btnCallLogin")).Click();
                    driver.FindElement(By.Id("btnAutenticar")).Click();

                    // page 2
                    Actions ActionPage2 = new Actions(driver);
                    var menuDropDown = driver.FindElement(By.Id("liInstituicoes"));
                    ActionPage2.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.CssSelector("#liInstituicoes > div > ul > li:nth-child(3) > a")).Click();

                    // page 3
                    driver.FindElement(By.Id("Prosseguir")).Click();

                    // page 4
                    driver.FindElement(By.CssSelector("div.selectorAll div.checkbox input")).Click();
                    driver.FindElement(By.Id("chkHabilitar")).Click();
                    driver.FindElement(By.Id("Prosseguir")).Click();

                    // page 5
                    if (this.type.Equals(KindPerson.LegalPerson))
                    {
                        var campoFilter = new SelectElement(driver.FindElement(By.Id("filterTipo")));
                        campoFilter.SelectByValue("2");
                    }
                    IWebElement campoBusca = driver.FindElement(By.Id("filterDocumento"));
                    campoBusca.SendKeys(this.identification);
                    driver.FindElement(By.Id("btnPesquisar")).Click();

                    // page 6
                    // TODO: implementar WAITING
                    //
                    Actions ActionPage6 = new Actions(driver);
                    var buttonSelectAll = driver.FindElement(By.Id("btnSelecionarTudo"));
                    ActionPage6.MoveToElement(buttonSelectAll).Build().Perform();

                    new WebDriverWait(driver, TimeSpan.FromSeconds(60))
                        .Until(d => d.FindElement(By.Id("btnSelecionarTudo")));
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    //buttonSelectAll.Click();

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
