using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

using KrakenMPSPCrawler.Enum;
using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArispCrawler : Crawler
    {
        private readonly KindPerson Type;
        private readonly string Identification;

        public ArispCrawler(KindPerson type, string identification)
        {
            Type = type;
            Identification = identification;
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
                    Actions actionPage2 = new Actions(driver);
                    var menuDropDown = driver.FindElement(By.Id("liInstituicoes"));
                    actionPage2.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.CssSelector("#liInstituicoes > div > ul > li:nth-child(3) > a")).Click();

                    // page 3
                    driver.FindElement(By.Id("Prosseguir")).Click();

                    // page 4
                    driver.FindElement(By.CssSelector("div.selectorAll div.checkbox input")).Click();
                    driver.FindElement(By.Id("chkHabilitar")).Click();
                    driver.FindElement(By.Id("Prosseguir")).Click();

                    // page 5
                    if (Type.Equals(KindPerson.LegalPerson))
                    {
                        var campoFilter = new SelectElement(driver.FindElement(By.Id("filterTipo")));
                        campoFilter.SelectByValue("2");
                    }
                    IWebElement campoBusca = driver.FindElement(By.Id("filterDocumento"));
                    campoBusca.SendKeys(Identification);
                    driver.FindElement(By.Id("btnPesquisar")).Click();

                    // page 6
                    var buttonSelectAll = driver.FindElement(By.Id("btnSelecionarTudo"));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", buttonSelectAll);
                    buttonSelectAll.Click();
                    driver.FindElement(By.Id("btnProsseguir")).Click();

                    // page 7 - Capturar dados
                    var resultados = driver.FindElements(By.CssSelector("#panelMatriculas > tr > td:nth-child(4) a.list.listDetails"));
                    foreach (IWebElement resultado in resultados)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", resultado);
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].removeAttribute('href');", resultado);
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", resultado);
                        Console.Write("ArispCrawler resultado");

                        // fechando a janela aberta
                        var tabs = driver.WindowHandles;
                        driver.SwitchTo().Window(tabs[tabs.Count - 1]);
                        driver.Close();
                        driver.SwitchTo().Window(tabs[tabs.Count - 2]);
                    }


                    driver.Close();
                    Console.Write("ArispCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("ArispCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage("ArispCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
                 