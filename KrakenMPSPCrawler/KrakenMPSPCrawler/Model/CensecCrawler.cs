using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using KrakenMPSPCrawler.Enum;
using KrakenMPSPCrawler.Models;

namespace KrakenMPSPCrawler.Crawlers
{
    public class CensecCrawler : Crawler
    {
        private KindPerson type;
        private String identification;
        private readonly string _usuario;
        private readonly string _senha;
        private readonly string _cpf;

        
        public CensecCrawler(string usuario, string senha, string cpf)
        {
            _usuario = usuario;
            _senha = senha;
            _cpf = cpf;
        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/censec/login.html");

                    // page 1
                    driver.FindElement(By.Id("LoginTextBox")).SendKeys(_usuario);
                    driver.FindElement(By.Id("LoginTextBox")).SendKeys(_usuario);
                    driver.FindElement(By.Id("SenhaTextBox")).SendKeys(_senha);
                    driver.FindElement(By.Id("EntrarButton")).Click();

                    // page 2 

                    Actions builder = new Actions(driver);

                    //desfazer o menu ja aberto
                    var saifora = driver.FindElement(By.Id("menuadministrativo"));
                    builder.MoveToElement(saifora).Build().Perform();

                    var menuDropDown = driver.FindElement(By.Id("menucentrais"));
                    builder.MoveToElement(menuDropDown).Build().Perform();

                    var menuDropDown2 = driver.FindElement(By.Id("ctl00_CESDILi"));
                    builder.MoveToElement(menuDropDown2).Build().Perform();

                    driver.FindElement(By.Id("ctl00_CESDIConsultaAtoHyperLink")).Click();

                    // page 3
                    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DocumentoTextBox")).SendKeys(_cpf);
                    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_BuscarButton")).Click();

                    // page 4

                    driver.FindElement(By.CssSelector("tr.linha1Tabela:nth-child(2) > td:nth-child(1) > input:nth-child(1)")).Click();
                    driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_VisualizarButton")).Click();
                    

                    // page 5 
                   
                    //recuperar dados
                    var nome = "";
                    var cpf_cnpj = "";
                    var qualidade = "";
                    var telefone = "";
                    var tipoTel = "";
                    var ramal = "";
                    var contato = "";
                    var status = "";

                    //dados dos campos

                    var resultadoCarga = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_CodigoTextBox")).GetAttribute("value");
                    var resultadoMes = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_MesReferenciaDropDownList")).Text;
                    var resultadoAno = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_AnoReferenciaDropDownList")).Text;
                    var resultadoAto = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_TipoAtoDropDownList")).Text;
                    var resultadoDiaAto = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DiaAtoTextBox")).GetAttribute("value");
                    var resultadoMesAto = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_MesAtoTextBox")).GetAttribute("value");
                    var resultadoAnoAto = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_AnoAtoTextBox")).GetAttribute("value");
                    var resultadoLivro = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_LivroTextBox")).GetAttribute("value");
                    var resultadoComplementoLivro = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_LivroComplementoTextBox")).GetAttribute("value");
                    var resultadoFolha = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_FolhaTextBox")).GetAttribute("value");
                    var resultadoComplementoFolha = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_FolhaComplementoTextBox")).GetAttribute("value");
                    
                    //dados da tabela
                    var resultados = driver.FindElements(By.CssSelector("#ctl00_ContentPlaceHolder1_PartesUpdatePanel > table > tbody > tr"));                    
                        for (int i = 1; i <= resultados.Count; i++)
                        {
                            nome += driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_PartesUpdatePanel > table > tbody > tr:nth-child(" + i + ") > td:nth-child(2)")).Text + "\n";
                            cpf_cnpj += driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_PartesUpdatePanel > table > tbody > tr:nth-child(" + i + ") > td:nth-child(3)")).Text + "\n";
                            qualidade += driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_PartesUpdatePanel > table > tbody > tr:nth-child(" + i + ") > td:nth-child(4)")).Text + "\n";
                        }

                    var resultadoUf = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_CartorioUFTextBox")).GetAttribute("value");
                    var resultadoMunicipio = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_CartorioMunicipioTextBox")).GetAttribute("value");
                    var resultadoCartorio = driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_CartorioNomeTextBox")).GetAttribute("value");

                    var resultados2 = driver.FindElements(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_DivTelefonesCartorioListView > div > table > tbody > tr"));
                    for (int i = 1; i <= resultados2.Count; i++)
                    {
                        telefone += driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_DivTelefonesCartorioListView > div > table > tbody > tr:nth-child(" + i + ") > td:nth-child(1)")).Text + "\n";
                        tipoTel += driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_DivTelefonesCartorioListView > div > table > tbody > tr:nth-child(" + i + ") > td:nth-child(2)")).Text + "\n";
                        ramal += driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_DivTelefonesCartorioListView > div > table > tbody > tr:nth-child(" + i + ") > td:nth-child(3)")).Text + "\n";
                        contato += driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_DivTelefonesCartorioListView > div > table > tbody > tr:nth-child(" + i + ") > td:nth-child(4)")).Text + "\n";
                        status += driver.FindElement(By.CssSelector("#ctl00_ContentPlaceHolder1_DadosCartorio_DivTelefonesCartorioListView > div > table > tbody > tr:nth-child(" + i + ") > td:nth-child(5)")).Text + "\n";
                    }

                    #region Objeto com os dados capturados
                    var resultado = new CensecCrawlerModel
                    {
                        Carga = resultadoCarga,
                        Data = resultadoMes+"/"+resultadoAno,
                        Ato = resultadoAto,
                        DataAto = resultadoDiaAto+"/"+resultadoMesAto+"/"+resultadoAnoAto,
                        Livro = resultadoLivro+"-"+resultadoComplementoLivro,
                        Folha = resultadoFolha+"-"+resultadoComplementoFolha,
                        Nomes = nome,
                        CpfsCnpjs = cpf_cnpj,
                        Qualidads = qualidade,
                        Uf = resultadoUf,
                        Municipio = resultadoMunicipio,
                        Cartorio = resultadoCartorio,
                        Telefones = telefone,
                        TipoTel = tipoTel,
                        Ramal = ramal,
                        Contato = contato,
                        Status = status,
                    };
                    #endregion


                    Console.WriteLine("CensecCrawler OK");
                    Console.ReadKey(intercept: true);
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("CensecCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e.Message);
                SetErrorMessage("CensecCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
