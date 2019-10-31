using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Services;

namespace KrakenMPSPConsole.Crawlers
{
    public class JucespCrawler : Crawler
    {
        private readonly KindPerson _type;
        private readonly string _identification;

        public JucespCrawler(KindPerson type)
        {
            _type = type;
        }

        public override CrawlerStatus Execute(out object result)
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/jucesp/index.html");

                    // page 1
                    driver.FindElement(By.Id("ctl00_cphContent_frmBuscaSimples_txtPalavraChave")).Click();
                    driver.FindElement(By.Id("ctl00_cphContent_frmBuscaSimples_txtPalavraChave")).SendKeys("Google");
                    driver.FindElement(By.XPath("/html/body/div[4]/form/div[3]/div[4]/div[1]/div/div[1]/table/tbody/tr/td[2]/input")).Click();

                    // page 2
                    driver.FindElement(By.XPath("/html/body/div[4]/div[3]/div[4]/div[2]/div/div/table/tbody/tr[1]/td/div/div[2]/label/input")).Click();
                    driver.FindElement(By.XPath("/html/body/div[4]/div[3]/div[4]/div[2]/div/div/table/tbody/tr[1]/td/div/div[2]/label/input")).SendKeys("Q8TJA");
                    driver.FindElement(By.XPath("/html/body/div[4]/div[3]/div[4]/div[2]/div/div/table/tbody/tr[2]/td/input")).Click();

                    // page 3
                    driver.FindElement(By.Id("ctl00_cphContent_gdvResultadoBusca_gdvContent_ctl02_lbtSelecionar")).Click();


                    // page 4
                   
                    

                    var tituloEmpresa = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblEmpresa")).Text.Trim();
                    var nireMatriz = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblNire")).Text.Trim();
                    var tipoEmpresa = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblDetalhes")).Text.Trim();
                    var dataConstituicao = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblConstituicao")).Text.Trim();
                    var inicioAtividade = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblAtividade")).Text.Trim();
                    var cNPJ = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblCnpj")).Text.Trim();
                    var inscricaoEstadual = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblInscricao")).Text.Trim();
                    var objeto = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblObjeto")).Text.Trim();
                    var capital = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblCapital")).Text.Trim();
                    var logradouro = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblLogradouro")).Text.Trim();
                    var numero = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblNumero")).Text.Trim();
                    var bairro = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblBairro")).Text.Trim();
                    var complemento = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblComplemento")).Text.Trim();
                    var municipio = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblMunicipio")).Text.Trim();
                    var cep = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblCep")).Text.Trim();
                    var uF = driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_lblUf")).Text.Trim();


                    // PDF
                    driver.FindElement(By.XPath("/html/body/div[4]/form/div[3]/div[4]/div/div[1]/div[2]/table/tbody/tr[3]/td/div/input")).Click();

                    ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
                    driver.SwitchTo().Window(windowHandles.Last());

                    IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
                    wait.Until(driver1 =>
                        ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState")
                        .Equals("complete"));

                    var downloadFolderPath = $@"{AppDomain.CurrentDomain.BaseDirectory}temp\jucesp\";
                    if (!Directory.Exists(downloadFolderPath))
                        Directory.CreateDirectory(downloadFolderPath);

                    var nomeArquivo = tituloEmpresa.Replace(" ", string.Empty);

                    var data = DateTime.Now.ToString("yyyyMMddhhmm",
                        System.Globalization.CultureInfo.InvariantCulture);

                    var arquivo = $@"{downloadFolderPath}{nomeArquivo}_{data}.pdf";
                    try
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(new Uri(driver.Url),
                                arquivo);
                            Console.WriteLine($@"PDF baixado com sucesso em {arquivo}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[JUCESP] Ocorreu um erro ao tentar baixar o PDF! \nMensagem de erro: " + e);
                        result = null;
                        return CrawlerStatus.Skipped;
                    }

                    driver.Close();

                    var jucesp = new JucespModel
                    {
                        TituloEmpresa = tituloEmpresa,
                        NireMatriz = nireMatriz,
                        TipoEmpresa = tipoEmpresa,
                        DataConstituicao = dataConstituicao,
                        InicioAtividade = inicioAtividade,
                        CNPJ = cNPJ,
                        InscricaoEstadual = inscricaoEstadual,
                        Objeto = objeto,
                        Capital = capital,
                        Logradouro = logradouro,
                        Numero = numero,
                        Bairro = bairro,
                        Complemento = complemento,
                        Municipio = municipio,
                        Cep = cep,
                        UF = uF
                    };
                    
                    result = jucesp;

                    driver.Close();
                    Console.WriteLine("ArispCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("Fail loading browser caught: {0}", e.Message);
                SetErrorMessage(typeof(JucespCrawler), e.Message);
                result = null;
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}", e.Message);
                SetErrorMessage(typeof(JucespCrawler), e.Message);
                result = null;
                return CrawlerStatus.Error;
            }

        }
    }
}
