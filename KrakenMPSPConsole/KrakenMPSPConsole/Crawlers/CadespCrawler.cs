using System;
using System.IO;
using System.Net;
using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace KrakenMPSPConsole.Crawlers
{
    public class CadespCrawler : Crawler
    {
        private readonly string _cnpj;

        public CadespCrawler(string cnpj)
        {
            _cnpj = cnpj;
        }

        public override CrawlerStatus Execute(out object result)
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/cadesp/login.html");

                    // page 1
                    driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_loginControl_UserName")).SendKeys("1");
                    driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_loginControl_Password")).SendKeys("1");
  
                    driver.FindElement(By.CssSelector("ctl00_conteudoPaginaPlaceHolder_loginControl_loginButton")).Click();


                    // page 2
                    Actions actionPage2 = new Actions(driver);
                    var menuDropDown = driver.FindElement(By.CssSelector("#ctl00_menuPlaceHolder_menuControl1_LoginView1_menuSuperiorn1 > table"));
                    actionPage2.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.CssSelector("#ctl00_menuPlaceHolder_menuControl1_LoginView1_menuSuperiorn1 > table > tbody > tr > td > a")).Click();

                    // page 3
                    driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_tcConsultaCompleta_TabPanel1_txtIdentificacao")).SendKeys(_cnpj);

                    driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_tcConsultaCompleta_TabPanel1_btnConsultarEstabelecimento")).Click();

                    // page 4 - Capturar dados
                    

                    var downloadFolderPath = $@"{AppDomain.CurrentDomain.BaseDirectory}temp\cadesp\";
                    if (!Directory.Exists(downloadFolderPath))
                        Directory.CreateDirectory(downloadFolderPath);

                    var data = DateTime.Now.ToString("yyyyMMddhhmm",
                        System.Globalization.CultureInfo.InvariantCulture);

                    var arquivo = $@"{downloadFolderPath}{_cnpj}_{data}.png";
                    try
                    {
                        Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                        screenshot.SaveAsFile(arquivo, ScreenshotImageFormat.Png);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[CADESP] Ocorreu um erro ao capturara a tela! \nMensagem de erro: " + e);
                        result = null;
                        return CrawlerStatus.Skipped;
                    }

                    var cadespResult = new CadespModel {Imagem = arquivo};

                    result = cadespResult;

                    driver.Close();
                    Console.WriteLine("CadespCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(CadespCrawler), e.Message);
                result = null;
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(CadespCrawler), e.Message);
                result = null;
                return CrawlerStatus.Error;
            }
        }
    }
}
