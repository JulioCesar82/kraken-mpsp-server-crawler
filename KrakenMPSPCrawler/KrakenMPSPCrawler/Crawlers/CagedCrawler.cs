using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using KrakenMPSPCrawler.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
// ReSharper disable All

namespace KrakenMPSPCrawler.Crawlers
{
    class CagedCrawler : Crawler
    {
        //Login - Input externo
        private readonly string Usuario = "fiap";
        private readonly string Senha = "MPSP";

        //Parâmetros de pesquisa
        private readonly string cnpj = "20501854000169";


        //Autorizado / Responsável
        private string razaoSocial = "";
        private string logradouro = "";
        private string bairro = "";
        private string municipio = "";
        private string estado = "";
        private string cep = "";

        private string nomeContato = "";
        private string cpfContato = "";
        private string telefoneContato = "";
        private string ramalContato = "";
        private string emailContato = "";

        private string cnae = "";
        private string atividadeEconomica = "";
        private int noFilias = 0;
        private int totalVinculos = 0;

        //Trabalhador
        private readonly string cpf = "00011111223";

        private string nomeTrabalhador = "";
        private string pisBaseTrabalhador = "";
        private string ctpsTrabalhador = "";
        private string faixaPisTrabalhador = "";
        private string nacionalidadeTrabalhador = "";
        private string grauInstrucaoTrabalhador = "";
        private string deficienteTrabalhador = "";

        private string dataNascimentoTrabalhador = "";
        //private string ufCtpsTrabalhador = "";
        private string sexoTrabalhador = "";
        private string corTrabalhador = "";

        private string cepTrabalhador = "";
        private string tempoTrabalhoCaged = "";
        private string tempoTrabalhoRais = "";


        //Empresa
        

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
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
                    dropdown.SelectByIndex(0);*/

                    driver.FindElement(By.Id("formPesquisarAutorizado:txtChavePesquisaAutorizado014")).Click();
                    driver.FindElement(By.Id("formPesquisarAutorizado:txtChavePesquisaAutorizado014")).SendKeys(Keys.Home + cnpj);

                    driver.FindElement(By.Id("formPesquisarAutorizado:bt027_8")).Click();

                    //page 4 - Capturar dados responsável

                    razaoSocial = driver.FindElement(By.Id("txtrazaosocial020_4")).Text;
                    logradouro = driver.FindElement(By.Id("txt3_logradouro020")).Text;
                    bairro = driver.FindElement(By.Id("txt4_bairro020")).Text;
                    municipio = driver.FindElement(By.Id("txt6_municipio020")).Text;
                    estado = driver.FindElement(By.Id("txt7_uf020")).Text;
                    cep = driver.FindElement(By.Id("txt8_cep020")).Text;

                    nomeContato = driver.FindElement(By.Id("txt_nome_contato")).Text;
                    cpfContato = driver.FindElement(By.Id("txt_contato_cpf")).Text;
                    telefoneContato = driver.FindElement(By.Id("txt21_ddd020")).Text +
                                      driver.FindElement(By.Id("txt9_telefone020")).Text;
                    ramalContato = driver.FindElement(By.Id("txt10_ramal020")).Text;
                    emailContato = driver.FindElement(By.Id("txt11_email")).Text;

                    //--- passar para a próxima página

                    Actions actionPage4 = new Actions(driver);
                    menuDropDown = driver.FindElement(By.Id("j_idt12:lk_menu_consultas"));
                    actionPage4.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.Id("j_idt12:idMenuLinkEmpresaCaged")).Click();

                    // page 5 - Consultar Empresa

                    driver.FindElement(By.Id("formPesquisarEmpresaCAGED:txtcnpjRaiz")).Click();
                    driver.FindElement(By.Id("formPesquisarEmpresaCAGED:txtcnpjRaiz")).SendKeys(Keys.Home + cnpj.Substring(0, 8));

                    driver.FindElement(By.Id("formPesquisarEmpresaCAGED:btConsultar")).Click();

                    //page 6 - Capturar dados empresa

                    cnae = driver.FindElement(By.Id("formResumoEmpresaCaged:txtCodigoAtividadeEconomica")).Text;
                    atividadeEconomica = driver.FindElement(By.Id("formResumoEmpresaCaged:txtDescricaoAtividadeEconomica")).Text;
                    noFilias = Int32.Parse(driver.FindElement(By.Id("formResumoEmpresaCaged:txtNumFiliais")).Text);
                    totalVinculos = Int32.Parse(driver.FindElement(By.Id("formResumoEmpresaCaged:txtTotalVinculos")).Text);

                    //--- passar para a próxima página
                    Actions actionPage6 = new Actions(driver);
                    menuDropDown = driver.FindElement(By.Id("j_idt12:lk_menu_consultas"));
                    actionPage6.MoveToElement(menuDropDown).Build().Perform();

                    driver.FindElement(By.Id("j_idt12:idMenuLinkTrabalhador")).Click();

                    //page 7 - Consultar trabalhador
                    IWebElement pesquisarPorDropDown = driver.FindElement(By.Id("formPesquisarTrabalhador:slctTipoPesquisaTrabalhador"));
                    new SelectElement(pesquisarPorDropDown).SelectByIndex(0);

                    driver.FindElement(By.Id("formPesquisarTrabalhador:txtChavePesquisa")).Click();
                    driver.FindElement(By.Id("formPesquisarTrabalhador:txtChavePesquisa")).SendKeys(Keys.Home + cpf);

                    driver.FindElement(By.Id("formPesquisarTrabalhador:submitPesqTrab")).Click();


                    //Page 8 - Capturar dados trabalhador
                    nomeTrabalhador = driver.FindElement(By.Id("txt2_Nome027")).Text;
                    pisBaseTrabalhador = driver.FindElement(By.Id("txt1_Pis028")).Text;
                    ctpsTrabalhador = driver.FindElement(By.Id("txt5_Ctps027")).Text;
                    faixaPisTrabalhador = driver.FindElement(By.Id("txt4_SitPis027")).Text;
                    nacionalidadeTrabalhador = driver.FindElement(By.Id("txt8_Nac027")).Text;
                    grauInstrucaoTrabalhador = driver.FindElement(By.Id("txt12_Instr027")).Text;
                    deficienteTrabalhador = driver.FindElement(By.Id("txt13_Def027")).Text;
                    
                    dataNascimentoTrabalhador = driver.FindElement(By.Id("txt4_datanasc027")).Text;
                    //ufCtpsTrabalhador = driver.FindElement(By.Id("")).Text;
                    sexoTrabalhador = driver.FindElement(By.Id("txt6_Sexo027")).Text;
                    corTrabalhador = driver.FindElement(By.Id("txt10_Raca027")).Text;

                    cepTrabalhador = driver.FindElement(By.Id("txtEstabCep91")).Text;
                    tempoTrabalhoCaged = driver.FindElement(By.Id("txt26_Caged027")).Text + " meses";
                    
                    tempoTrabalhoRais = driver.FindElement(By.Id("txt27_Rais027")).Text + " meses";
                    
                    //Page 9 - Salvar PDF

                    driver.FindElement(By.CssSelector(".link > a:nth-child(1)")).Click();

                    IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));

                    wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                    
                   var downloadFolderPath = $@"{AppDomain.CurrentDomain.BaseDirectory}temp\caged\";
                   if (!Directory.Exists(downloadFolderPath))
                       Directory.CreateDirectory(downloadFolderPath);
                   

                    ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
                    driver.SwitchTo().Window(windowHandles.Last());


                    try
                    {
                        using (var client = new WebClient())
                        {

                            client.DownloadFileAsync(new Uri(driver.Url), $@"{downloadFolderPath}{nomeTrabalhador}.pdf");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[CAGED] Ocorreu um erro ao tentar baixar o PDF! \nMensagem de erro: " + e);
                    }

                    
                    driver.Close();
                    Console.Write("CagedCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage(typeof(CagedCrawler), e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage(typeof(CagedCrawler), e.Message);
                return CrawlerStatus.Error;
            }
        }
    }
}
