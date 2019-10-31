using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;
using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace KrakenMPSPConsole.Crawlers
{
    class InfocrimCrawler : Crawler
    {
        private readonly WebClient client;
        private readonly string _pathTemp;
        private readonly string _usuario;
        private readonly string _senha;

        public InfocrimCrawler(string usuario, string senha)
        {
            _usuario = usuario;
            _senha = senha;
        }

        public InfocrimCrawler()
        {
            throw new NotImplementedException();
        }

        public override CrawlerStatus Execute(out object result)
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));

                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/login");

                    // page 1
                    driver.FindElement(By.Id("username")).SendKeys("fiap");
                    driver.FindElement(By.Id("password")).SendKeys("mpsp");
                    driver.FindElement(By.CssSelector(".btn")).Click();


                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/infocrim/login.html");

                    //page 1

                    wait.Until(driver1 =>
                        ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState")
                        .Equals("complete"));


                    driver.FindElement(By.CssSelector("#wrapper > tbody:nth-child(1) > tr:nth-child(4) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(3) > input:nth-child(1)"))
                        .SendKeys(_usuario);
                    driver.FindElement(By.CssSelector("#wrapper > tbody:nth-child(1) > tr:nth-child(4) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(3) > input:nth-child(1)"))
                        .SendKeys(_senha);
                    driver.FindElement(By.CssSelector("#wrapper > tbody:nth-child(1) > tr:nth-child(4) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(4) > a:nth-child(1)"))
                        .Click();

                    //page 2
                    var agora = DateTime.Now.ToString("ddmmyyyy");
                    driver.FindElement(By.Id("dtIni")).SendKeys(agora);
                    driver.FindElement(By.Id("dtFim")).SendKeys(agora);
                    driver.FindElement(By.Id("enviar")).Click();

                    //page 3
                    var nome = driver.FindElement(By.CssSelector("body > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(3) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(5) > font:nth-child(2)")).Text;
                    driver.FindElement(By.CssSelector("body > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(3) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(2) > a:nth-child(2)")).Click();
                    nome = nome.Replace(" ", "");
                    //page 4

                    var downloadFolderPath = $@"{AppDomain.CurrentDomain.BaseDirectory}temp\infocrim\";
                    if (!Directory.Exists(downloadFolderPath))
                        Directory.CreateDirectory(downloadFolderPath);

                    var datahora = DateTime.Now.ToString("yyyyMMddhhmm");


                    var arquivoBO = $"{downloadFolderPath}/BO_{nome}_{datahora}.png";
                    ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
                    Screenshot screenshot = screenshotDriver.GetScreenshot();
                    screenshot.SaveAsFile(arquivoBO, ScreenshotImageFormat.Png);

                    var resultados = new InfocrimModel
                    {
                        BO = arquivoBO
                    };

                    result = resultados;

                    Console.WriteLine("InfocrimCrawler OK");

                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(InfocrimCrawler), e.Message);
                result = null;
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(InfocrimCrawler), e.Message);
                result = null;
                return CrawlerStatus.Error;
            }
        }
    }
}