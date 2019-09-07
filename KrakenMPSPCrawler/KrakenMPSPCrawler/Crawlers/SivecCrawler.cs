using System;

using OpenQA.Selenium;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class SivecCrawler : Crawler
    {
        private readonly string Usuario;
        private readonly string Senha;

        private readonly string MatriculaSap;
        private readonly string NomeCompleto;
        private readonly string RG;

        public SivecCrawler(string usuario, string senha, string matriculaSap, string nomeCompleto, string rg)
        {
            Usuario = usuario;
            Senha = senha;

            MatriculaSap = matriculaSap;
            NomeCompleto = nomeCompleto;
            RG = rg;
        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/sivec/login.html");

                    // page 1
                    driver.FindElement(By.Id("nomeusuario")).SendKeys(Usuario);
                    driver.FindElement(By.Id("senhausuario")).SendKeys(Senha);

                    driver.FindElement(By.Id("Acessar")).Click();


                    // page 2
                    driver.FindElement(By.CssSelector("#navbar-collapse-1 > ul > li:nth-child(4) > a")).Click();
                    driver.FindElement(By.Id("1")).Click();

                    if (!string.IsNullOrEmpty(RG))
                    {
                        driver.FindElement(By.CssSelector("li.open:nth-child(2) > ul:nth-child(2) > li:nth-child(1) > a:nth-child(1)")).Click();
                    }
                    else if (!string.IsNullOrEmpty(NomeCompleto))
                    {
                        driver.FindElement(By.CssSelector("li.open:nth-child(2) > ul:nth-child(2) > li:nth-child(1) > a:nth-child(2)")).Click();
                    }
                    else if (!string.IsNullOrEmpty(MatriculaSap))
                    {
                        driver.FindElement(By.CssSelector("li.open:nth-child(2) > ul:nth-child(2) > li:nth-child(1) > a:nth-child(3)")).Click();
                    }


                    // page 3
                    if (!string.IsNullOrEmpty(RG))
                    {
                        driver.FindElement(By.Id("idValorPesq")).SendKeys(RG);
                        driver.FindElement(By.Id("procurar")).Click();
                    }
                    else if (!string.IsNullOrEmpty(NomeCompleto))
                    {
                        driver.FindElement(By.Id("idNomePesq")).SendKeys(NomeCompleto);
                        driver.FindElement(By.Id("procura")).Click();
                    }
                    else if (!string.IsNullOrEmpty(MatriculaSap))
                    {
                        driver.FindElement(By.Id("idValorPesq")).SendKeys(MatriculaSap);
                        driver.FindElement(By.Id("procurar")).Click();
                    }


                    // page 4
                    var personFind = driver.FindElement(By.CssSelector("#tabelaPesquisa > tbody > tr:nth-child(1) > td.textotab1.text-center.sorting_1 > a"));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", personFind);
                    personFind.Click();


                    // page 5 - Capturar dados
                    var resultadoCorDaPele = driver.FindElement(By.CssSelector("div.col-md-12:nth-child(4) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(7) > td:nth-child(2) > span:nth-child(1)")).Text;
                    var resultadoCorDoCabelo = driver.FindElement(By.CssSelector("div.col-md-12:nth-child(4) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(6) > td:nth-child(5) > span:nth-child(1)")).Text;
                    var resultadoProfissao = driver.FindElement(By.CssSelector("div.col-md-12:nth-child(4) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(7) > td:nth-child(5) > span:nth-child(1)")).Text;

                    Console.WriteLine("SivecCrawler resultado cor da pele {0}, tipo de cabelo {1}; profissao {2}", resultadoCorDaPele, resultadoCorDoCabelo, resultadoProfissao);


                    driver.Close();
                    Console.Write("SivecCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("SivecCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage("SivecCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }

    }
}
