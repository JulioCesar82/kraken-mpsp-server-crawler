using System;
using System.IO;
using System.Net;
using System.Linq;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Services;

namespace KrakenMPSPConsole.Crawlers
{
    public class DetranCrawler : Crawler
    {
        private readonly WebClient client;
        private readonly string _pathTemp;

        private readonly string _usuario;
        private readonly string _senha;

        private readonly string _cpf;
        private readonly string _cnpj;

        public DetranCrawler(string usuario, string senha, KindPerson kind, string identificador)
        {
            client = new WebClient();

            _usuario = usuario;
            _senha = senha;

            if (kind.Equals(KindPerson.LegalPerson))
                _cnpj = identificador;
            else if (kind.Equals(KindPerson.PhysicalPerson))
                _cpf = identificador;

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
                    var tabs = driver.WindowHandles;
                    var rndPdf = new Random();
                    var lastTab = driver.WindowHandles.Last();
                    var firstTab = driver.WindowHandles.First();

                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/login.html");

                    // page 1
                    driver.FindElement(By.Id("form:j_id563205015_44efc1ab")).SendKeys(_cpf);
                    driver.FindElement(By.Id("form:j_id563205015_44efc191")).SendKeys(_senha);
                    driver.FindElement(By.Id("form:j_id563205015_44efc15b")).Click();


                    // page 2        
                    Actions builder1 = new Actions(driver);
                    var menuDropDown1 = driver.FindElement(By.Id("navigation_a_M_16"));
                    builder1.MoveToElement(menuDropDown1).Build().Perform();
                    driver.FindElement(By.Id("navigation_a_F_16")).Click();


                    // page 3
                    // TODO: INSERIR MAIS DADOS PESSOAIS DE BUSCA
                    driver.FindElement(By.Id("form:cpf")).SendKeys(_cpf);
                    driver.FindElement(By.CssSelector("#form\\:j_id2049423534_c43228e_content > table:nth-child(3) > tbody > tr > td > a")).Click();


                    // page 4 - Capturar dados 1
                    // indo para a janela aberta
                    lastTab = driver.WindowHandles.Last();
                    driver.SwitchTo().Window(lastTab);
                    var nameFileLinhaVida = $"{_pathTemp}/linhadeVida-{rndPdf.Next(1000, 10001)}.pdf";
                    client.DownloadFile((string)driver.Url, nameFileLinhaVida);

                    // fechando a janela aberta
                    driver.Close();


                    // page 3
                    // voltando para a janela anterior
                    lastTab = driver.WindowHandles.Last();
                    driver.SwitchTo().Window(firstTab);

                    Actions builder2 = new Actions(driver);
                    var menuDropDown2 = driver.FindElement(By.Id("navigation_a_M_16"));
                    builder2.MoveToElement(menuDropDown2).Build().Perform();
                    driver.FindElement(By.CssSelector("#navigation_ul_M_16 > li:nth-child(2) > a:nth-child(1)")).Click();


                    //driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/pagina4-pesquisa-imagem-cnh.html");

                    // page 4
                    driver.FindElement(By.Id("form:cpf")).SendKeys(_cpf);
                    driver.FindElement(By.CssSelector("a.ui-button > span:nth-child(1)")).Click();

                    // page 5 - Capturar dados 2
                    lastTab = driver.WindowHandles.Last();
                    driver.SwitchTo().Window(lastTab);

                    var resultadoRenach = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > span:nth-child(2)")).Text.Trim();
                    var resultadoCategoria = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > span:nth-child(2)")).Text.Trim();
                    var resultadoEmissão = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(3) > span:nth-child(2)")).Text.Trim();
                    var resultadoNascimento = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(4) > span:nth-child(2)")).Text.Trim();
                    var resultadoNomeCondutor = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > span:nth-child(1)")).Text.Trim();
                    var resultadoNomePai = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(3) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > span:nth-child(1)")).Text.Trim();
                    var resultadoNomeMae = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(4) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > span:nth-child(1)")).Text.Trim();
                    var resultadoRegistro = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(5) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > span:nth-child(2)")).Text.Trim();
                    var resultadoTipografico = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(5) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > span:nth-child(2)")).Text.Trim();
                    var resultadoIdentidade = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(5) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(3) > span:nth-child(2)")).Text.Trim();
                    var resultadoCpf = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(5) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(4) > span:nth-child(2)")).Text.Trim();
                    var fotoSrc = driver.FindElement(By.Id("form:imgFoto")).GetAttribute("src");
                    var assinaturaSrc = driver.FindElement(By.Id("form:imgAssinatura")).GetAttribute("src");

                    var rndPicture = new Random();
                    var nextRndPicture = rndPicture.Next(1000, 10001);
                    client.DownloadFile((string)fotoSrc, $"{_pathTemp}/foto-{nextRndPicture}.png");
                    client.DownloadFile((string)assinaturaSrc, $"{_pathTemp}/assinatura-{nextRndPicture}.png");

                    driver.Close();


                    // page 6
                    // voltando para a janela anterior
                    firstTab = driver.WindowHandles.First();
                    driver.SwitchTo().Window(firstTab);

                    Actions builder3 = new Actions(driver);
                    var menuDropDown3 = driver.FindElement(By.Id("navigation_a_M_18"));
                    builder3.MoveToElement(menuDropDown3).Build().Perform();
                    driver.FindElement(By.CssSelector("#navigation_a_F_18")).Click();


                    // page 7
                    if (_cpf != null)
                        driver.FindElement(By.Id("form:j_id2124610415_1b3be1e3")).SendKeys(_cpf);

                    if (_cnpj != null)
                        driver.FindElement(By.Id("form:j_id2124610415_1b3be1e3")).SendKeys(_cpf);

                    driver.FindElement(By.Id("form:j_id2124610415_1b3be1bd")).SendKeys(_cpf);
                    driver.FindElement(By.CssSelector("a.ui-button > span:nth-child(1)")).Click();


                    // page 8 - Capturar dados 3
                    lastTab = driver.WindowHandles.Last();
                    driver.SwitchTo().Window(lastTab);
                    var nameFileVeiculo = $"{_pathTemp}/relatorioveiculo-{rndPdf.Next(1000, 10001)}.pdf";
                    client.DownloadFile((string)driver.Url, nameFileVeiculo);
                    driver.Close();

                    //Rsultado = objeto
                    result = null;

                    firstTab = driver.WindowHandles.First();
                    driver.SwitchTo().Window(firstTab);


                    driver.Close();
                    Console.WriteLine("DetranCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(DetranCrawler), e.Message);
                result = null;
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(DetranCrawler), e.Message);
                result = null;
                return CrawlerStatus.Error;
            }
        }
    }
}