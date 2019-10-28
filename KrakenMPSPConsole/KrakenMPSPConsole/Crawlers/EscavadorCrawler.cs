using System;
using System.IO;
using System.Net;
using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;
using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Services;
using OpenQA.Selenium;

namespace KrakenMPSPConsole.Crawlers
{
    public class EscavadorCrawler : Crawler
    {
        private readonly WebClient client;
        private readonly string _busca;
        private readonly string _tipo;
        private readonly string _pathTemp;

        public EscavadorCrawler(string busca, KindPerson kind)
        {
            client = new WebClient();
            _busca = busca;
            if (kind.Equals(KindPerson.LegalPerson))
            {                
                _tipo = "Legal";
            }
            else if (kind.Equals(KindPerson.PhysicalPerson))
            {
                _tipo = "Physical";
            }
            _pathTemp = $@"{AppDomain.CurrentDomain.BaseDirectory}/temp/detran";
            if (!Directory.Exists(_pathTemp))
            {
                Directory.CreateDirectory(_pathTemp);
            }
        }

        public override CrawlerStatus Execute(out object result)
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"https://www.escavador.com");

                    var resultado = new EscavadorModel();

                    if (_tipo.Equals("Physical"))
                    {
                        driver.FindElement(By.CssSelector(".c-search-box_input")).SendKeys(_busca);
                        driver.FindElement(By.CssSelector(".c-search-box_buttonLabel")).Click();
                    }                                        
                    if (_tipo.Equals("Legal")) {
                    driver.FindElement(By.CssSelector(".c-search-box_input")).Click();
                    driver.FindElement(By.CssSelector("li.c-search-box_filtersOption:nth-child(2) > button:nth-child(1)")).Click();
                    driver.FindElement(By.CssSelector(".c-search-box_input")).SendKeys(_busca);
                    driver.FindElement(By.CssSelector(".c-search-box_buttonLabel")).Click();
                    }

                    Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                    var nameEscavador = $"{_pathTemp}/escavador-{ss}.png";
                    ss.SaveAsFile(nameEscavador);

                    #region Objeto com os dados capturados
                    resultado = new EscavadorModel
                    {                        
                        Imagem1 = nameEscavador
                    };
                    #endregion

                    driver.Close();
                    Console.WriteLine("EscavadorCrawler OK");
                    result = resultado;
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(EscavadorCrawler), e.Message);
                result = null;
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(EscavadorCrawler), e.Message);
                result = null;
                return CrawlerStatus.Error;
            }
        }
    }
}
