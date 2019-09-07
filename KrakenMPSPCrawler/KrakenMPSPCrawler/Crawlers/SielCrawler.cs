using System;

using OpenQA.Selenium;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class SielCrawler : Crawler
    {
        private readonly string Usuario;
        private readonly string Senha;

        private readonly string NumeroProcesso;
        private readonly string NomeCompleto;
        private readonly string NomeDaMae;
        private readonly string DataNascimento;
        private readonly string TituloEleitor;

        public SielCrawler(string usuario, string senha, string numeroProcesso, string tituloEleitor)
        {
            Usuario = usuario;
            Senha = senha;

            NumeroProcesso = numeroProcesso;
            TituloEleitor = tituloEleitor;
        }

        public SielCrawler(string usuario, string senha, string numeroProcesso, string nomeCompleto, string nomeDaMae, string dataNascimento)
        {
            Usuario = usuario;
            Senha = senha;

            NumeroProcesso = numeroProcesso;
            NomeCompleto = nomeCompleto;
            NomeDaMae = nomeDaMae;
            DataNascimento = dataNascimento;
        }

        public SielCrawler(string usuario, string senha, string numeroProcesso, string nomeCompleto, string nomeDaMae, string dataNascimento, string tituloEleitor)
        {
            Usuario = usuario;
            Senha = senha;

            NumeroProcesso = numeroProcesso;
            NomeCompleto = nomeCompleto;
            NomeDaMae = nomeDaMae;
            DataNascimento = dataNascimento;
            TituloEleitor = tituloEleitor;
        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/siel/login.html");

                    // page 1 - Login
                    // Inserir usuário e senha -> Clicar em Enviar                    
                    driver.FindElement(By.CssSelector("div.mioloInterna.apps > form > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys(Usuario);
                    driver.FindElement(By.CssSelector("div.mioloInterna.apps > form > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=password]")).SendKeys(Senha);

                    driver.FindElement(By.CssSelector("div.mioloInterna.apps > form > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=submit]")).Click();


                    // page 2 - Pesquisa
                    // Inserir Nome e Número do processo -> Clicar em Enviar
                    if (!string.IsNullOrEmpty(NomeCompleto) && !string.IsNullOrEmpty(NomeDaMae) && !string.IsNullOrEmpty(DataNascimento))
                    {
                        driver.FindElement(By.CssSelector("form.formulario > fieldset:nth-child(1) > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys(NomeCompleto);
                        driver.FindElement(By.CssSelector("form.formulario > fieldset:nth-child(1) > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=text]")).SendKeys(NomeDaMae);
                        driver.FindElement(By.CssSelector("form.formulario > fieldset:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=text]")).SendKeys(DataNascimento);
                    }

                    if (!string.IsNullOrEmpty(TituloEleitor))
                    {
                        driver.FindElement(By.CssSelector("form.formulario > fieldset:nth-child(1) > table > tbody > tr:nth-child(4) > td:nth-child(2) > input[type=text]")).SendKeys(TituloEleitor);
                    }

                    driver.FindElement(By.Id("num_processo")).SendKeys(NumeroProcesso);
                    driver.FindElement(By.CssSelector("form.formulario > table > tbody > tr > td:nth-child(2) > input")).Click();


                    // page 3 - Capturar dados
                    var resultados = driver.FindElements(By.CssSelector(".lista tbody > tr > td:nth-child(2)"));
                    foreach (IWebElement resultado in resultados)
                    {
                        Console.Write("SielCrawler resultado {0}", resultado.Text);
                    }


                    driver.Close();
                    Console.Write("SielCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("SielCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage("SielCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
