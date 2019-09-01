using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace KrakenMPSPCrawler.Crawlers
{
    class SielCrawler : Crawler
    {

        private String usuario;
        private String senha;

        public SielCrawler(String usuario, String senha)
        {
            this.usuario = usuario;
            this.senha = senha;
        }
        public override CrawlerStatus Execute()
        {
            using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                try
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/siel/login.html");

                    //page 1 - Login
                    //Inserir usuário e senha -> Clicar em Enviar                    
                    driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form/table/tbody/tr[1]/td[2]/input")).SendKeys(usuario);
                    driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form/table/tbody/tr[2]/td[2]/input")).SendKeys(senha);
                    
                    driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form/table/tbody/tr[3]/td[2]/input")).Click();


                    //page 2 - Pesquisa
                    //Inserir Nome e Número do processo -> Clicar em Enviar
                    driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form[2]/fieldset[1]/table/tbody/tr[1]/td[2]/input")).SendKeys("NOME COMPLETO"); //TODO - Externalizar variável
                    driver.FindElement(By.Id("num_processo")).SendKeys("000000000"); //TODO - Externalizar variável
                    driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form[2]/table/tbody/tr/td[2]/input")).Click();


                    //page 3 - Capturar dados
                    //Salvar dados da tabela
                    var tabela = "";
                    
                        //Registra todas as informações dentro de tags TD dentro da tabela
                    IWebElement tableElement = driver.FindElement(By.CssSelector(".lista"));
                    IList<IWebElement> tableDataElements = tableElement.FindElements(By.TagName("td"));
                    var tableCount = 0;
                    foreach (IWebElement tableRow in tableDataElements)
                    {
                        //Ignora a primeira coluna da tabela
                        tableCount++;
                        if (tableCount % 2 != 0) continue;

                        //Armazena os dados como texto
                        tabela += tableRow.Text + "\n";
                    }

                    Console.WriteLine(tabela);


                    //[EXTRA] page 4 - Impressão de página
                    //Salvar PDF da página


                    Console.WriteLine("SielCrawler OK");
                    Console.ReadKey(intercept: true);
                    return CrawlerStatus.Success;
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
