using System;
using System.IO;
using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

using KrakenMPSPCrawler.Enum;
using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Services;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArispCrawler : Crawler
    {
        private readonly KindPerson _type;
        private readonly string _identification;

        public ArispCrawler(KindPerson type, string identification)
        {
            _type = type;
            _identification = identification;
        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
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
                    if (_type.Equals(KindPerson.LegalPerson))
                    {
                        var campoFilter = new SelectElement(driver.FindElement(By.Id("filterTipo")));
                        campoFilter.SelectByValue("2");
                    }
                    IWebElement campoBusca = driver.FindElement(By.Id("filterDocumento"));
                    campoBusca.SendKeys(_identification);
                    driver.FindElement(By.Id("btnPesquisar")).Click();


                    // page 6
                    var buttonSelectAll = driver.FindElement(By.Id("btnSelecionarTudo"));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", buttonSelectAll);
                    buttonSelectAll.Click();
                    driver.FindElement(By.Id("btnProsseguir")).Click();


                    // page 7
                    var pathTemp = $@"{AppDomain.CurrentDomain.BaseDirectory}/temp/arisp";
                    var rnd = new Random();
                    if (!Directory.Exists(pathTemp)) {
                        Directory.CreateDirectory(pathTemp);
                    }

                    var contador = 0;
                    var processos = driver.FindElements(By.CssSelector("#panelMatriculas > tr > td:nth-child(4) a.list.listDetails"));
                    List<Processo> resultados = new List<Processo>();
                    foreach (IWebElement processo in processos)
                    {
                        contador++;
                        var cidade = driver.FindElement(By.CssSelector($"#panelMatriculas > tr:nth-child({contador}) > td:nth-child(1)")).Text.Trim();
                        var cartorio = driver.FindElement(By.CssSelector($"#panelMatriculas > tr:nth-child({contador}) > td:nth-child(2)")).Text.Trim();
                        var matricula = driver.FindElement(By.CssSelector($"#panelMatriculas > tr:nth-child({contador}) > td:nth-child(3)")).Text.Trim();
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", processo);
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].removeAttribute('href');", processo);
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", processo);


                        // page 8 - Capturar dados
                        var tabs = driver.WindowHandles;
                        // indo para a janela aberta
                        driver.SwitchTo().Window(tabs[tabs.Count - 1]);

                        var nameFile = $"{pathTemp}/matricula-{rnd.Next(1000, 10001)}.png";
                        Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                        screenshot.SaveAsFile(nameFile, ScreenshotImageFormat.Png);

                        #region Objeto com os dados capturados
                        resultados.Add(new Processo
                        {
                            Cidade = cidade,
                            Cartorio = cartorio,
                            Matricula = matricula,
                            Arquivo = nameFile
                        });
                        #endregion

                        // fechando a janela aberta
                        driver.Close();

                        // voltando para a janela anterior
                        driver.SwitchTo().Window(tabs[tabs.Count - 2]);
                    }

                    SetInformationFound(new ArispCrawlerModel
                    {
                        Processos = resultados
                    }
                    );

                    driver.Close();
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(ArispCrawler), e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(ArispCrawler), e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
                 