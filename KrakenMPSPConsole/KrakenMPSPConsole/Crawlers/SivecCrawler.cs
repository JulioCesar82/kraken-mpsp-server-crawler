using System;

using OpenQA.Selenium;

using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Models;
using KrakenMPSPConsole.Services;

namespace KrakenMPSPConsole.Crawlers
{
    public class SivecCrawler : Crawler
    {
        private readonly string _usuario;
        private readonly string _senha;

        private readonly string _matriculaSap;
        private readonly string _nomeCompleto;
        private readonly string _rg;

        public SivecCrawler(string usuario, string senha, string matriculaSap, string nomeCompleto, string rg)
        {
            _usuario = usuario;
            _senha = senha;

            _matriculaSap = matriculaSap;
            _nomeCompleto = nomeCompleto;
            _rg = rg;
        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/sivec/login.html");

                    // page 1
                    driver.FindElement(By.Id("nomeusuario")).SendKeys(_usuario);
                    driver.FindElement(By.Id("senhausuario")).SendKeys(_senha);

                    driver.FindElement(By.Id("Acessar")).Click();


                    // page 2
                    driver.FindElement(By.CssSelector("#navbar-collapse-1 > ul > li:nth-child(4) > a")).Click();
                    driver.FindElement(By.Id("1")).Click();

                    if (!string.IsNullOrEmpty(_rg))
                    {
                        driver.FindElement(By.CssSelector("li.open:nth-child(2) > ul:nth-child(2) > li:nth-child(1) > a:nth-child(1)")).Click();
                    }
                    else if (!string.IsNullOrEmpty(_nomeCompleto))
                    {
                        driver.FindElement(By.CssSelector("li.open:nth-child(2) > ul:nth-child(2) > li:nth-child(1) > a:nth-child(2)")).Click();
                    }
                    else if (!string.IsNullOrEmpty(_matriculaSap))
                    {
                        driver.FindElement(By.CssSelector("li.open:nth-child(2) > ul:nth-child(2) > li:nth-child(1) > a:nth-child(3)")).Click();
                    }


                    // page 3
                    if (!string.IsNullOrEmpty(_rg))
                    {
                        driver.FindElement(By.Id("idValorPesq")).SendKeys(_rg);
                        driver.FindElement(By.Id("procurar")).Click();
                    }
                    else if (!string.IsNullOrEmpty(_nomeCompleto))
                    {
                        driver.FindElement(By.Id("idNomePesq")).SendKeys(_nomeCompleto);
                        driver.FindElement(By.Id("procura")).Click();
                    }
                    else if (!string.IsNullOrEmpty(_matriculaSap))
                    {
                        driver.FindElement(By.Id("idValorPesq")).SendKeys(_matriculaSap);
                        driver.FindElement(By.Id("procurar")).Click();
                    }


                    // page 4
                    var personFind = driver.FindElement(By.CssSelector("#tabelaPesquisa > tbody > tr:nth-child(1) > td.textotab1.text-center.sorting_1 > a"));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", personFind);
                    personFind.Click();


                    // page 5 - Capturar dados
                    const string caminhoTabela = "body > form:nth-child(13) > div > ";

                    #region Objeto com os dados capturados
                    var outrasInfo = new Outros
                    {
                        Nome = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(10) > div.col-md-9")).Text.Trim(),
                        RG = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(11) > div:nth-child(2)")).Text.Trim(),
                        DataNascimento = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(12) > div.col-md-5")).Text.Trim(),
                        Naturalidade = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(13) > div.col-md-7")).Text.Trim(),
                        NomePai = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(14) > div.col-md-9")).Text.Trim(),
                        NomeMae = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(15) > div.col-md-9")).Text.Trim()
                    };

                    var resultado = new SivecCrawlerModel
                    {
                        Nome = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-11 > table > tbody > tr:nth-child(1) > td:nth-child(2)")).Text.Trim(),
                        Sexo = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-11 > table > tbody > tr:nth-child(1) > td:nth-child(5)")).Text.Trim(),
                        DataNascimento = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-11 > table > tbody > tr:nth-child(2) > td:nth-child(2)")).Text.Trim(),
                        RG = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-11 > table > tbody > tr:nth-child(2) > td:nth-child(5)")).Text.Trim(),
                        NumControle = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-11 > table > tbody > tr:nth-child(3) > td:nth-child(2)")).Text.Trim(),
                        TipoRG = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-11 > table > tbody > tr:nth-child(3) > td:nth-child(5)")).Text.Trim(),
                        DataEmissaoRG = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(1) > td:nth-child(2)")).Text.Trim(),
                        Alcunha = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(1) > td:nth-child(5)")).Text.Trim(),
                        EstadoCivil = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(2) > td:nth-child(2)")).Text.Trim(),
                        Naturalidade = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(2) > td:nth-child(5)")).Text.Trim(),
                        Naturalizado = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(3) > td:nth-child(2)")).Text.Trim(),
                        PostoIdentificacao = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(3) > td:nth-child(5)")).Text.Trim(),
                        GrauInstrucao = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(4) > td:nth-child(2)")).Text.Trim(),
                        FormulaFundamental = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(4) > td:nth-child(5)")).Text.Trim(),
                        NomePai = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(5) > td:nth-child(2)")).Text.Trim(),
                        CorOlhos = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(5) > td:nth-child(5)")).Text.Trim(),
                        NomeMae = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(6) > td:nth-child(2)")).Text.Trim(),
                        Cabelo = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(6) > td:nth-child(5)")).Text.Trim(),
                        CorPele = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(7) > td:nth-child(2)")).Text.Trim(),
                        Profissao = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(5) > div.col-md-12.top-buffer25 > table > tbody > tr:nth-child(7) > td:nth-child(5)")).Text.Trim(),
                        EnderecoResidencial = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(7) > div.col-md-7")).Text.Trim(),
                        EnderecoTrabalho = driver
                            .FindElement(By.CssSelector($"{caminhoTabela}div:nth-child(8) > div.col-md-7")).Text.Trim(),
                        outros = outrasInfo
                    };
                    #endregion

                    SetInformationFound(resultado);

                    driver.Close();
                    Console.WriteLine("SivecCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(SivecCrawler), e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(SivecCrawler), e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
