using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KrakenMPSPCrawler.Enum;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArpenpCrawler : Crawler
    {
        private KindPerson type;
        private String identification;

        public ArpenpCrawler(KindPerson type, String identification)
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
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/login.html");

                    // page 1
                    driver.FindElement(By.CssSelector("#main > div > div > div > div > a")).Click();

                    // page 2
                    /**
                    Actions builder = new Actions(driver);
                    var menuDropDown = driver.FindElement(By.Id("arrumaMenu"));
                    builder.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.CssSelector("ul > li:nth-child(1) > a")).Click();

                    **/


                    //CERTIDAO DE NASCIMENTO


                    //REMOVER LINK APÓS CORRIGIR O CÓDIGO COMENTADO ACIMA
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/pagina3-busca.html");

                    // page 3
                    driver.FindElement(By.Id("n")).Click();
                    /**
                    IWebElement campo = driver.FindElement(By.CssSelector("tr > td > td"));
                    campo.Click();
                    campo.SendKeys("1");
                    **/
                    var campoNasc = new SelectElement(driver.FindElement(By.Id("vara_juiz_id")));
                    campoNasc.SelectByValue("297");
                    //driver.FindElement(By.Id("btn_pesquisar")).Click();

                    //REMOVER LINK APÓS CORRIGIR O CÓDIGO COMENTADO ACIMA
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/pagina4-resultado.html?tipo_registro=N%2CTN&numero_processo=&vara_juiz_id=0&outra_vara=&uf=0&cidade_id=0&cartorio_id=0&flag_conjuge=TD&nome_registrado=&cpf_registrado=&nome_pai=&nome_mae=&data_ocorrido_ini=&data_ocorrido_fim=&data_registro_ini=&data_registro_fim=&num_livro=&num_folha=&num_registro=&btn_pesquisar=Pesquisar");

                    var resultadosNasc = driver.FindElements(By.CssSelector("#corpo > div > div > div > div > div > form"));


                    //CERTIDAO DE CASAMENTO


                    //REMOVER LINK APÓS CORRIGIR O CÓDIGO COMENTADO ACIMA
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/pagina3-busca.html");

                    // page 3
                    driver.FindElement(By.Id("c")).Click();
                    /**
                    IWebElement campo = driver.FindElement(By.CssSelector("tr > td > td"));
                    campo.Click();
                    campo.SendKeys("1");
                    **/
                    var campoCasa = new SelectElement(driver.FindElement(By.Id("vara_juiz_id")));
                    campoCasa.SelectByValue("297");
                    //driver.FindElement(By.Id("btn_pesquisar")).Click();

                    //REMOVER LINK APÓS CORRIGIR O CÓDIGO COMENTADO ACIMA
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/pagina4-resultado.html?tipo_registro=N%2CTN&numero_processo=&vara_juiz_id=0&outra_vara=&uf=0&cidade_id=0&cartorio_id=0&flag_conjuge=TD&nome_registrado=&cpf_registrado=&nome_pai=&nome_mae=&data_ocorrido_ini=&data_ocorrido_fim=&data_registro_ini=&data_registro_fim=&num_livro=&num_folha=&num_registro=&btn_pesquisar=Pesquisar");

                    var resultadosCasa = driver.FindElements(By.CssSelector("#corpo > div > div > div > div > div > form"));


                    //CERTIDAO DE OBITO


                    //REMOVER LINK APÓS CORRIGIR O CÓDIGO COMENTADO ACIMA
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/pagina3-busca.html");

                    // page 3
                    driver.FindElement(By.Id("o")).Click();
                    /**
                    IWebElement campo = driver.FindElement(By.CssSelector("tr > td > td"));
                    campo.Click();
                    campo.SendKeys("1");
                    **/
                    var campoObt = new SelectElement(driver.FindElement(By.Id("vara_juiz_id")));
                    campoObt.SelectByValue("297");
                    //driver.FindElement(By.Id("btn_pesquisar")).Click();

                    //REMOVER LINK APÓS CORRIGIR O CÓDIGO COMENTADO ACIMA
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/pagina4-resultado.html?tipo_registro=N%2CTN&numero_processo=&vara_juiz_id=0&outra_vara=&uf=0&cidade_id=0&cartorio_id=0&flag_conjuge=TD&nome_registrado=&cpf_registrado=&nome_pai=&nome_mae=&data_ocorrido_ini=&data_ocorrido_fim=&data_registro_ini=&data_registro_fim=&num_livro=&num_folha=&num_registro=&btn_pesquisar=Pesquisar");

                    var resultadosObt = driver.FindElements(By.CssSelector("#corpo > div > div > div > div > div > form"));

                    driver.Close();
                    Console.WriteLine("ArpenpCrawler OK");
                    Console.ReadKey(intercept: true);
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("ArispCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e.Message);
                SetErrorMessage("ArispCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
