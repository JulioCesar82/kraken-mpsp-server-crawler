﻿using System;
using OpenQA.Selenium;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Services;

namespace KrakenMPSPConsole.Crawlers
{
    public class SielCrawler : Crawler
    {
        private readonly string _usuario;
        private readonly string _senha;

        private readonly string _numeroProcesso;
        private readonly string _nomeCompleto;
        private readonly string _nomeDaMae;
        private readonly string _dataNascimento;
        private readonly string _tituloEleitor;

        public SielCrawler(string usuario, string senha, string numeroProcesso, string tituloEleitor)
        {
            _usuario = usuario;
            _senha = senha;

            _numeroProcesso = numeroProcesso;
            _tituloEleitor = tituloEleitor;
        }

        public SielCrawler(string usuario, string senha, string numeroProcesso, string nomeCompleto, string nomeDaMae, string dataNascimento, string tituloEleito = "")
        {
            _usuario = usuario;
            _senha = senha;

            _numeroProcesso = numeroProcesso;
            _nomeCompleto = nomeCompleto;
            _nomeDaMae = nomeDaMae;
            _dataNascimento = dataNascimento;
            _tituloEleitor = tituloEleito;
        }

        public override CrawlerStatus Execute(out object result)
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/siel/login.html");

                    // page 1 - Login
                    // Inserir usuário e senha -> Clicar em Enviar                    
                    driver.FindElement(By.CssSelector("div.mioloInterna.apps > form > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys(_usuario);
                    driver.FindElement(By.CssSelector("div.mioloInterna.apps > form > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=password]")).SendKeys(_senha);

                    driver.FindElement(By.CssSelector("div.mioloInterna.apps > form > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=submit]")).Click();


                    // page 2 - Pesquisa
                    // Inserir Nome e Número do processo -> Clicar em Enviar
                    if (!string.IsNullOrEmpty(_nomeCompleto) && !string.IsNullOrEmpty(_nomeDaMae) && !string.IsNullOrEmpty(_dataNascimento))
                    {
                        driver.FindElement(By.CssSelector("form.formulario > fieldset:nth-child(1) > table > tbody > tr:nth-child(1) > td:nth-child(2) > input[type=text]")).SendKeys(_nomeCompleto);
                        driver.FindElement(By.CssSelector("form.formulario > fieldset:nth-child(1) > table > tbody > tr:nth-child(2) > td:nth-child(2) > input[type=text]")).SendKeys(_nomeDaMae);
                        driver.FindElement(By.CssSelector("form.formulario > fieldset:nth-child(1) > table > tbody > tr:nth-child(3) > td:nth-child(2) > input[type=text]")).SendKeys(_dataNascimento);
                    }

                    if (!string.IsNullOrEmpty(_tituloEleitor))
                    {
                        driver.FindElement(By.CssSelector("form.formulario > fieldset:nth-child(1) > table > tbody > tr:nth-child(4) > td:nth-child(2) > input[type=text]")).SendKeys(_tituloEleitor);
                    }

                    driver.FindElement(By.Id("num_processo")).SendKeys(_numeroProcesso);
                    driver.FindElement(By.CssSelector("form.formulario > table > tbody > tr > td:nth-child(2) > input")).Click();


                    // page 3 - Capturar dados
                    var dados = driver.FindElements(By.CssSelector(".lista tbody > tr > td:nth-child(2)"));

                    #region Objeto com os dados capturados
                    var resultado = new SielModel
                    {
                        Nome = dados[0].Text,
                        Titulo = dados[1].Text,
                        DataNascimento = dados[2].Text,
                        Zona = dados[3].Text,
                        Endereco = dados[4].Text,
                        Municipio = dados[5].Text,
                        UF = dados[6].Text,
                        DataDomicilio = dados[7].Text,
                        NomePai = dados[8].Text,
                        NomeMae = dados[9].Text,
                        Naturalidade = dados[10].Text,
                        CodValidacao = dados[11].Text
                    };
                    #endregion

                    result = resultado;

                    driver.Close();
                    Console.WriteLine("SielCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(SielCrawler), e.Message);
                result = null;
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(SielCrawler), e.Message);
                result = null;
                return CrawlerStatus.Error;
            }
        }
    }
}
