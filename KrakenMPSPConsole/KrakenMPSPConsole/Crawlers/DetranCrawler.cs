using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Management.Instrumentation;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

using KrakenMPSPBusiness.Models;
using KrakenMPSPBusiness.Enums;

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
        private readonly string _rg;
        private readonly string _cnpj;
        private readonly string _tipo;

        public DetranCrawler(string usuario, string senha, KindPerson kind, string identificador)
        {
            client = new WebClient();
            _usuario = usuario;
            _senha = senha;

            if (kind.Equals(KindPerson.LegalPerson))
            {
                _cnpj = identificador;
                _tipo = "Legal";
            }
            else if (kind.Equals(KindPerson.PhysicalPerson))
            {            
                _cpf = identificador;
                _tipo = "Physical";
            }
            _pathTemp = $@"{AppDomain.CurrentDomain.BaseDirectory}/temp/detran";
            if (!Directory.Exists(_pathTemp))
            {
                Directory.CreateDirectory(_pathTemp);
            }
        }

        public DetranCrawler(string usuario, string senha, string rg, KindPerson kind, string identificador)
        {
            client = new WebClient();
            _usuario = usuario;
            _senha = senha;

            if (kind.Equals(KindPerson.LegalPerson))
            {
                _cnpj = identificador;
                _tipo = "Legal";
            }
            else if (kind.Equals(KindPerson.PhysicalPerson))
            {
                _cpf = identificador;
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
                    var lastTab = driver.WindowHandles.Last();
                    var firstTab = driver.WindowHandles.First();
                    var data = DateTime.Now.ToString("yyyyMMddhhmm",System.Globalization.CultureInfo.InvariantCulture);

                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/login.html");

                    // page 1
                    driver.FindElement(By.Id("form:j_id563205015_44efc1ab")).SendKeys(_usuario);
                    driver.FindElement(By.Id("form:j_id563205015_44efc191")).SendKeys(_senha);
                    driver.FindElement(By.Id("form:j_id563205015_44efc15b")).Click();

                    var resultado = new DetranModel();

                    if (_tipo.Equals("Physical"))
                    {
                        // page 2        
                        
                        Actions builder1 = new Actions(driver);
                        var menuDropDown1 = driver.FindElement(By.Id("navigation_a_M_16"));
                        builder1.MoveToElement(menuDropDown1).Build().Perform();
                        //Thread.Sleep(5000);
                        driver.FindElement(By.Id("navigation_a_F_16")).Click();
                        
                        // page 3
                        // TODO: INSERIR MAIS DADOS PESSOAIS DE BUSCA
                        driver.FindElement(By.Id("form:cpf")).SendKeys(_cpf);
                        driver.FindElement(By.Id("form:rg")).SendKeys(_rg);
                        driver.FindElement(By.CssSelector("#form\\:j_id2049423534_c43228e_content > table:nth-child(3) > tbody > tr > td > a")).Click();

                        // page 4 - Capturar dados 1
                        // indo para a janela aberta
                        lastTab = driver.WindowHandles.Last();
                        driver.SwitchTo().Window(lastTab);
                        var nameFileLinhaVida = $"{_pathTemp}/linhadeVida-{data}.pdf";
                        client.DownloadFileAsync(new Uri(driver.Url), nameFileLinhaVida);

                        // fechando a janela aberta
                        driver.Close();

                        // page 3
                        // voltando para a janela anterior
                        driver.SwitchTo().Window(firstTab);

                        Actions builder2 = new Actions(driver);
                        var menuDropDown2 = driver.FindElement(By.Id("navigation_a_M_16"));
                        builder2.MoveToElement(menuDropDown2).Build().Perform();
                        driver.FindElement(By.CssSelector("#navigation_ul_M_16 > li:nth-child(2) > a:nth-child(1)")).Click();
                           
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
                        var resultadoRg = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(5) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(3) > span:nth-child(2)")).Text.Trim();
                        var resultadoCpf = driver.FindElement(By.CssSelector("#form\\:pnCNH > tbody:nth-child(1) > tr:nth-child(2) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(5) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(4) > span:nth-child(2)")).Text.Trim();
                        try
                        {                            
                            var fotoSrc = driver.FindElement(By.Id("form:imgFoto")).GetAttribute("src");
                            var assinaturaSrc = driver.FindElement(By.Id("form:imgAssinatura")).GetAttribute("src");

                            //var rndPicture = new Random();
                            //var nextRndPicture = rndPicture.Next(1000, 10001);

                            var nameFileFoto = $@"{_pathTemp}/foto_{data}.png";
                            client.DownloadFile(new Uri(fotoSrc), nameFileFoto);
                            var nameFileAssinatura = $@"{_pathTemp}/assinatura_{data}.png";
                            client.DownloadFileAsync(new Uri(assinaturaSrc), nameFileAssinatura);

                            #region Objeto com os dados capturados
                            resultado = new DetranModel
                            {
                                Renach = resultadoRenach,
                                Categoria = resultadoCategoria,
                                Emissao = resultadoEmissão,
                                Nascimento = resultadoNascimento,
                                Nome = resultadoNomeCondutor,
                                NomePai = resultadoNomePai,
                                NomeMae = resultadoNomeMae,
                                Registro = resultadoRegistro,
                                Tipografo = resultadoTipografico,
                                RG = resultadoRg,
                                CPF = resultadoCpf,
                                Arquivo1 = nameFileLinhaVida,
                                Imagem1 = nameFileFoto,
                                Imagem2 = nameFileAssinatura,
                            };
                            #endregion
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("[DETRAN] Ocorreu um erro ao tentar baixar as Imagens! \nMensagem de erro: " + e);
                            result = null;
                            return CrawlerStatus.Skipped;                            
                        }

                        // page 6
                        // voltando para a janela anterior
                        driver.Close();
                        firstTab = driver.WindowHandles.First();
                        driver.SwitchTo().Window(firstTab);
                        
                    }
                    
                    Actions builder3 = new Actions(driver);
                    var menuDropDown3 = driver.FindElement(By.Id("navigation_a_M_18"));
                    builder3.MoveToElement(menuDropDown3).Build().Perform();
                    driver.FindElement(By.CssSelector("#navigation_a_F_18")).Click();
                    
                    // page 7
                    if (_cpf != null)
                        driver.FindElement(By.Id("form:j_id2124610415_1b3be1e3")).SendKeys(_cpf);

                    if (_cnpj != null)
                        driver.FindElement(By.Id("form:j_id2124610415_1b3be1e3")).SendKeys(_cnpj);

                    driver.FindElement(By.CssSelector("a.ui-button > span:nth-child(1)")).Click();


                    // page 8 - Capturar dados 3
                    lastTab = driver.WindowHandles.Last();
                    driver.SwitchTo().Window(lastTab);
                    var nameFileVeiculo = $@"{_pathTemp}/relatorioveiculo_{data}.pdf";
                    client.DownloadFileAsync(new Uri(driver.Url), nameFileVeiculo);
                    
                    driver.Close();                                      

                    firstTab = driver.WindowHandles.First();
                    driver.SwitchTo().Window(firstTab);
                    driver.Close();

                    if (_tipo.Equals("Physical"))
                    {
                        resultado.Arquivo2 = nameFileVeiculo;
                    }
                    else
                    {
                        #region Objeto com os dados capturados
                        resultado = new DetranModel
                        {
                            Arquivo2 = nameFileVeiculo
                        };
                        #endregion
                    }

                    Console.WriteLine("DetranCrawler OK");
                    result = resultado;
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
