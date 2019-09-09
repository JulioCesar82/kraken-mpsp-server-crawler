using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using KrakenMPSPCrawler.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace KrakenMPSPCrawler.Crawlers
{
    class CagedCrawler : Crawler
    {
        private readonly string Usuario = "fiap";
        private readonly string Senha = "MPSP";
        private readonly string ChavePesquisaResponsavel = "00111222333344";
        private readonly string ChavePesquisaTrabalhador = "00011111223";
        private readonly string CNPJRaiz = "00111222";

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/caged/login.html");

                    //page 1 - Login
                    driver.FindElement(By.Id("username")).SendKeys(Usuario);
                    driver.FindElement(By.Id("password")).SendKeys(Senha);

                    driver.FindElement(By.Id("btn-submit")).Click();

                    //page 2 - Autorizado/Responsável
                    Actions actionPage2 = new Actions(driver);
                    var menuDropDown = driver.FindElement(By.Id("j_idt12:lk_menu_consultas"));
                    actionPage2.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.Id("j_idt12:idMenuLinkAutorizado")).Click();

                    //page 3 - Consultar responsável

                    //BUG NO WEBSITE : Ao trocar o valor do campo, ele automaticamente passa para a página seguinte
                    /*
                    IWebElement pesquisarDropDown = driver.FindElement(By.Id("formPesquisarAutorizado:slctTipoPesquisaAutorizado"));
                    var dropdown = new SelectElement(pesquisarDropDown);
                    dropdown.SelectByIndex(1);
                    dropdown.SelectByIndex(0);*/

                    driver.FindElement(By.Id("formPesquisarAutorizado:txtChavePesquisaAutorizado014")).Click();
                    driver.FindElement(By.Id("formPesquisarAutorizado:txtChavePesquisaAutorizado014")).SendKeys(Keys.Home + ChavePesquisaResponsavel);

                    driver.FindElement(By.Id("formPesquisarAutorizado:bt027_8")).Click();

                    //page 4 - Capturar dados responsável

                    //--- passar para a próxima página

                    Actions actionPage4 = new Actions(driver);
                    menuDropDown = driver.FindElement(By.Id("j_idt12:lk_menu_consultas"));
                    actionPage4.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.Id("j_idt12:idMenuLinkEmpresaCaged")).Click();

                    // page 5 - Consultar Empresa
                    driver.FindElement(By.Id("formPesquisarEmpresaCAGED:txtcnpjRaiz")).Click();
                    driver.FindElement(By.Id("formPesquisarEmpresaCAGED:txtcnpjRaiz")).SendKeys(Keys.Home + CNPJRaiz);

                    driver.FindElement(By.Id("formPesquisarEmpresaCAGED:btConsultar")).Click();

                    //page 6 - Capturar dados empresa

                    //--- passar para a próxima página
                    Actions actionPage6 = new Actions(driver);
                    menuDropDown = driver.FindElement(By.Id("j_idt12:lk_menu_consultas"));
                    actionPage6.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.Id("j_idt12:idMenuLinkTrabalhador")).Click();

                    //page 7 - Consultar trabalhador
                    IWebElement pesquisarPorDropDown = driver.FindElement(By.Id("formPesquisarTrabalhador:slctTipoPesquisaTrabalhador"));
                    new SelectElement(pesquisarPorDropDown).SelectByIndex(1);
                    new SelectElement(pesquisarPorDropDown).SelectByIndex(0);

                    driver.FindElement(By.Id("formPesquisarTrabalhador:txtChavePesquisa")).Click();
                    driver.FindElement(By.Id("formPesquisarTrabalhador:txtChavePesquisa")).SendKeys(Keys.Home + ChavePesquisaTrabalhador);

                    driver.FindElement(By.Id("formPesquisarTrabalhador:submitPesqTrab")).Click();

                    //Page 8 - Capturar dados trabalhador

                    //--- passar para a próxima página

                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/caged/pagina9-dados-trabalhador.html?formPesquisarTrabalhador=formPesquisarTrabalhador&formPesquisarTrabalhador%3AslctTipoPesquisaTrabalhador=1&formPesquisarTrabalhador%3AperfilRaisCaged=1&formPesquisarTrabalhador%3AtxtChavePesquisa=&formPesquisarTrabalhador%3AsubmitPesqTrab=Consultar&formPesquisarTrabalhador%3Aj_idt65=&javax.faces.ViewState=5832164626526760368%3A-4226066932030001535");
                    //Page 8/9 - Salvar PDF
                    //Pegar o nome do trabalhador capturado na página 8
                    var nomeTrabalhador = "teste";
                    //TODO : Alterar caminho
                    var downloadFolderPath = "d:\\mpsp\\pdfs\\caged\\" + nomeTrabalhador;
                    var wgetDriver = $"{Environment.CurrentDirectory}\\Libs\\Wget\\wget.exe";
                    var pdfLink = driver.FindElement(By.CssSelector(".link > a:nth-child(1)")).GetAttribute("href");

                    var strCmdText = "/C " + wgetDriver + " -x -nd -P " + downloadFolderPath + " " + pdfLink;
                    Console.WriteLine();
                    
                    try
                    {
                        Process.Start("CMD.exe", strCmdText)?.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }


                    driver.Close();
                    Console.Write("CagedCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("CagedCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage("CagedCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
