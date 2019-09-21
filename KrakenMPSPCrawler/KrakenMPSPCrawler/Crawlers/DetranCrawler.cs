using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

using KrakenMPSPCrawler.Services;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium.Support.UI;

namespace KrakenMPSPCrawler.Crawlers
{
    public class DetranCrawler : Crawler
    {
        private readonly WebClient client;
        private readonly string _pathTemp;

        private readonly string _usuario;
        private readonly string _senha;

        private readonly string _cpf;

        public DetranCrawler(string usuario, string senha, string cpf)
        {
            client = new WebClient();

            _usuario = usuario;
            _senha = senha;

            _cpf = cpf;

            _pathTemp = $@"{AppDomain.CurrentDomain.BaseDirectory}/temp/detran";
            if (!Directory.Exists(_pathTemp))
            {
                Directory.CreateDirectory(_pathTemp);
            }

        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/login.html");

                    // page 1
                    driver.FindElement(By.Id("form:j_id563205015_44efc1ab")).SendKeys(_usuario);
                    driver.FindElement(By.Id("form:j_id563205015_44efc191")).SendKeys(_senha);
                    driver.FindElement(By.Id("form:j_id563205015_44efc15b")).Click();


                    // page 2        
                    WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    var menuDropDown1 = wait1.Until(ExpectedConditions.ElementIsVisible(By.Id("navigation_li_M_16")));
                    Actions builder1 = new Actions(driver);
                    builder1.MoveToElement(menuDropDown1).Perform();
                    driver.FindElement(By.Id("navigation_a_F_16")).Click();


                    // page 3
                    // TODO: INSERIR MAIS DADOS PESSOAIS DE BUSCA
                    driver.FindElement(By.Id("form:cpf")).SendKeys(_cpf);
                    driver.FindElement(By.CssSelector("#form\\:j_id2049423534_c43228e_content > table:nth-child(3) > tbody > tr > td > a")).Click();
                    var tabs = driver.WindowHandles;


                    // page 4 - Capturar dados 1
                    // indo para a janela aberta
                    driver.SwitchTo().Window(tabs[tabs.Count - 1]);
                    var rndPdf = new Random();
                    var nameFile = $"{_pathTemp}/linhadeVida-{rndPdf.Next(1000, 10001)}.pdf";
                    client.DownloadFile(driver.Url, nameFile);

                    // fechando a janela aberta
                    driver.Close();


                    // page 3
                    // voltando para a janela anterior
                    driver.SwitchTo().Window(tabs[tabs.Count - 2]);

                    WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    var menuDropDown2 = wait2.Until(ExpectedConditions.ElementIsVisible(By.Id("navigation_li_M_16")));
                    Actions builder2 = new Actions(driver);
                    builder2.MoveToElement(menuDropDown2).Perform();
                    driver.FindElement(By.CssSelector("#navigation_ul_M_16 > li:nth-child(2) > a:nth-child(1)")).Click();


                    // page 4
                    driver.FindElement(By.Id("form:cpf")).SendKeys(_cpf);
                    driver.FindElement(By.CssSelector("a.ui-button > span:nth-child(1)")).Click();


                    // page 5 - Capturar dados 2
                    var resultadoRenach = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/table/tbody/tr/td[1]/span")).Text.Trim();
                    var resultadoCategoria = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/table/tbody/tr/td[2]/span")).Text.Trim();
                    var resultadoEmissão = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/table/tbody/tr/td[3]/span")).Text.Trim();
                    var resultadoNascimento = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[1]/td/table/tbody/tr/td[4]/span")).Text.Trim();
                    var resultadoNomeCondutor = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr[2]/td/span")).Text.Trim();
                    var resultadoNomePai = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[3]/td/table/tbody/tr[2]/td/span")).Text.Trim();
                    var resultadoNomeMae = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr[2]/td/span")).Text.Trim();
                    var resultadoRegistro = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/table/tbody/tr/td[1]/span")).Text.Trim();
                    var resultadoTipografico = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/table/tbody/tr/td[2]/span")).Text.Trim();
                    var resultadoIdentidade = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/table/tbody/tr/td[3]/span")).Text.Trim();
                    var resultadoCpf = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[5]/td/table/tbody/tr/td[4]/span")).Text.Trim();
                    var fotoSrc = driver.FindElement(By.Id("form:imgFoto")).GetAttribute("src");
                    var assinaturaSrc = driver.FindElement(By.Id("form:imgAssinatura")).GetAttribute("src");

                    var rndPicture = new Random();
                    var nextRndPicture = rndPicture.Next(1000, 10001);
                    client.DownloadFile(fotoSrc, $"{_pathTemp}/foto-{nextRndPicture}.png");
                    client.DownloadFile(assinaturaSrc, $"{_pathTemp}/assinatura-{nextRndPicture}.png");


                    // page 3
                    Actions builder3 = new Actions(driver);
                    var menuDropDown3 = driver.FindElement(By.Id("navigation_a_M_18"));
                    builder3.MoveToElement(menuDropDown3).Build().Perform();
                    driver.FindElement(By.CssSelector("#navigation_a_F_18")).Click();


                    // page 4
                    driver.FindElement(By.Id("form:j_id2124610415_1b3be1bd")).SendKeys(_cpf);
                    driver.FindElement(By.Id("form:j_id2124610415_1b3be1e3")).SendKeys(_cpf);
                    driver.FindElement(By.CssSelector("a.ui-button > span:nth-child(1)")).Click();


                    // page 5 - Capturar dados 3
                    client.DownloadFile(driver.Url, "relatorioveiculo.pdf");
                    driver.Close();


                    driver.Close();
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(DetranCrawler), e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(DetranCrawler), e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}