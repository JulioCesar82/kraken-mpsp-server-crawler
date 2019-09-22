using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using KrakenMPSPBusiness.Models;

using KrakenMPSPCrawler.Enum;
using KrakenMPSPCrawler.Model;
using KrakenMPSPCrawler.Services;

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
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
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


                    // page 4 - Capturar dados
                    #region Objeto com os dados capturados
                    var resultado = new ArpenspCrawlerModel
                    {
                        CartorioRegistro =
                            driver.FindElement(By.CssSelector(
                                    "#principal > div > form > table:nth-child(3) > tbody > tr:nth-child(1) > td:nth-child(2)"))
                                .Text.Trim(),
                        NumeroCNS = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(3) > tbody > tr:nth-child(2) > td:nth-child(2)"))
                            .Text.Trim(),
                        UF = driver.FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(3) > tbody > tr:nth-child(3) > td:nth-child(2)"))
                            .Text.Trim(),
                        NomeConjugeA1 = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(2) > td:nth-child(2)"))
                            .Text.Trim(),
                        NovoNomeConjugeA2 =
                            driver.FindElement(By.CssSelector(
                                    "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(3) > td:nth-child(2)"))
                                .Text.Trim(),
                        NomeConjugeB1 = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(4) > td:nth-child(2)"))
                            .Text.Trim(),
                        NovoNomeConjugeB2 =
                            driver.FindElement(By.CssSelector(
                                    "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(5) > td:nth-child(2)"))
                                .Text.Trim(),
                        DataCasamento = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(6) > td:nth-child(2)"))
                            .Text.Trim(),
                        Matricula = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(8) > td:nth-child(2)"))
                            .Text.Trim(),
                        DataEntrada = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(9) > td:nth-child(2)"))
                            .Text.Trim(),
                        DataRegistro = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(10) > td:nth-child(2)"))
                            .Text.Trim(),
                        Acervo = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(11) > td:nth-child(2)"))
                            .Text.Trim(),
                        NumeroLivro = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(12) > td:nth-child(2)"))
                            .Text.Trim(),
                        NumeroFolha = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(13) > td:nth-child(2)"))
                            .Text.Trim(),
                        NumeroRegistro =
                            driver.FindElement(By.CssSelector(
                                    "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(14) > td:nth-child(2)"))
                                .Text.Trim(),
                        TipoLivro = driver
                            .FindElement(By.CssSelector(
                                "#principal > div > form > table:nth-child(15) > tbody > tr:nth-child(15) > td:nth-child(2)"))
                            .Text.Trim()
                    };
                    #endregion

                    SetInformationFound(resultado);

                    driver.Close();
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(ArpenspCrawler), e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(ArpenspCrawler), e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
