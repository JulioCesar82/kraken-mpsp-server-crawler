using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using KrakenMPSPCrawler.Enum;
using System.Threading;

namespace KrakenMPSPCrawler.Crawlers
{
    public class DetranCrawler : Crawler
    {
        private KindPerson type;
        private String identification;

        public DetranCrawler(KindPerson type, String identification)
        {
            this.type = type;
            this.identification = identification;
        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    
                    var client = new System.Net.WebClient();
                    var tabs = driver.WindowHandles;
                    
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/login.html");

                    // page 1

                    IWebElement campoCpf = driver.FindElement(By.Id("form:j_id563205015_44efc1ab"));
                    campoCpf.SendKeys("11111111111");
                    IWebElement campoSenha = driver.FindElement(By.Id("form:j_id563205015_44efc191"));
                    campoSenha.SendKeys("senha123");
                    driver.FindElement(By.Id("form:j_id563205015_44efc15b")).Click();

                    // page 2                    

                    Actions builder = new Actions(driver);
                    var menuDropDown = driver.FindElement(By.Id("navigation_a_M_16"));
                    builder.MoveToElement(menuDropDown).Build().Perform();
                    driver.FindElement(By.Id("navigation_a_F_16")).Click();

                    // page 3

                    IWebElement campoCpf2 = driver.FindElement(By.Id("form:cpf"));
                    campoCpf2.SendKeys("11111111111");

                    var pdfUrl = driver.FindElement(By.CssSelector("#form\\:j_id2049423534_c43228e_content > table:nth-child(3) > tbody > tr > td > a")).GetAttribute("href");

                    driver.FindElement(By.CssSelector("#form\\:j_id2049423534_c43228e_content > table:nth-child(3) > tbody > tr > td > a")).Click();

                    // page 4
                    
                    //client.DownloadFile(pdfUrl, @"c:\Users\logonrmlocal\Downloads\linhadeVida.pdf");
                    client.DownloadFile(pdfUrl, "linhadeVida.pdf");


                    // page 5
                    //driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/pagina2-pesquisa.html?form%3AinRespostaDesafio=&form%3AinCertificadoDigital=&form%3Aj_id563205015_44efc1ab=11111111111&form%3Aj_id563205015_44efc191=senha123&form%3Aj_id563205015_44efc15b=&form_SUBMIT=1&javax.faces.ViewState=A9g%2FmZ8AAqg1xF33sX8CZnyZ2ZPOS5BbEjQv0AIokQn0IvkkXC%2FeQvW7b4Dfr%2BROrL5G%2BGTxXERTaqFBKWKrVvKU42GcGLDj4eKRk563z8sTcu%2BEzO0DGSu%2FqrQ%3D");
                    driver.SwitchTo().Window(tabs[0]);

                    Actions builder2 = new Actions(driver);
                    var menuDropDown2 = driver.FindElement(By.CssSelector("#navigation_a_M_16"));
                    builder2.MoveToElement(menuDropDown2).Build().Perform();
                    driver.FindElement(By.CssSelector("#navigation_ul_M_16 > li:nth-child(2) > a:nth-child(1)")).Click();

                    // page 6

                    IWebElement campoCpfCondutor = driver.FindElement(By.Id("form:cpf"));
                    campoCpfCondutor.SendKeys("11111111111");
                    driver.FindElement(By.CssSelector("a.ui-button > span:nth-child(1)")).Click();

                    // page 7 

                    //driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/pagina7-imagem-cnh.html");

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


                    client.DownloadFile(fotoSrc, "foto.png");
                    client.DownloadFile(assinaturaSrc, "assinatura.png");

                    // page 8

                    Console.WriteLine();
                    driver.SwitchTo().Window(tabs[0]);

                    Actions builder3 = new Actions(driver);
                    var menuDropDown3 = driver.FindElement(By.Id("navigation_a_M_18"));
                    builder3.MoveToElement(menuDropDown3).Build().Perform();
                    driver.FindElement(By.CssSelector("#navigation_a_F_18")).Click();

                    // page 9

                    IWebElement campoPlaca = driver.FindElement(By.Id("form:j_id2124610415_1b3be1bd"));
                    campoPlaca.SendKeys("ABC1234");
                    IWebElement campoCpfVeiculo = driver.FindElement(By.Id("form:j_id2124610415_1b3be1e3"));
                    campoCpfVeiculo.SendKeys("11111111111");
                    
                    var pdfUrl2 = driver.FindElement(By.CssSelector("a.ui-button")).GetAttribute("href");

                    driver.FindElement(By.CssSelector("a.ui-button > span:nth-child(1)")).Click();

                    // page 10

                    //client.DownloadFile(pdfUrl, @"c:\Users\logonrmlocal\Downloads\relatorioveiculo.pdf");
                    client.DownloadFile(pdfUrl, "relatorioveiculo.pdf");



                    Console.WriteLine("DetranCrawler OK");
                    Console.ReadKey(intercept: true);
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("DetranCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage("DetranCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}