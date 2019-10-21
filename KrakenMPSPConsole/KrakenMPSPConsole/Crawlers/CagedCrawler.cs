﻿using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Remoting.Channels;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Enums;
using KrakenMPSPConsole.Services;

namespace KrakenMPSPConsole.Crawlers
{
    class CagedCrawler : Crawler
    {
        //Login - Input externo
        private readonly string _usuario;
        private readonly string _senha;

        //Parâmetros de pesquisa
        private readonly string _cnpj;
        private readonly string _cpf;

        public CagedCrawler(string usuario, string senha, KindPerson kind, string identificador)
        {
            _usuario = usuario;
            _senha = senha;

            if(kind.Equals(KindPerson.LegalPerson))
                _cnpj = identificador;
            else if(kind.Equals(KindPerson.PhysicalPerson))
                _cpf = identificador;
        }
        public override CrawlerStatus Execute(out object result)
        {
            try
            {
                using (var driver = WebDriverService.CreateWebDriver(WebBrowser.Firefox))
                {
                    driver.Navigate().GoToUrl(@"http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/caged/login.html");

                    //page 1 - Login
                    driver.FindElement(By.Id("username")).SendKeys(_usuario);
                    driver.FindElement(By.Id("password")).SendKeys(_senha);

                    driver.FindElement(By.Id("btn-submit")).Click();

                    result = null;
                    //page 2 - Autorizado/Responsável
                    IWebElement menuDropDown;
                    if (_cnpj != null)
                    {
                        Actions actionPage2 = new Actions(driver);
                        menuDropDown = driver.FindElement(By.Id("j_idt12:lk_menu_consultas"));
                        actionPage2.MoveToElement(menuDropDown).Build().Perform();

                        driver.FindElement(By.Id("j_idt12:idMenuLinkAutorizado")).Click();

                        //page 3 - Consultar responsável
                        //TODO: BUG NO WEBSITE ao trocar o valor do campo, ele automaticamente passa para a página seguinte
                        /*
                        IWebElement pesquisarDropDown = driver.FindElement(By.Id("formPesquisarAutorizado:slctTipoPesquisaAutorizado"));
                        var dropdown = new SelectElement(pesquisarDropDown);
                        dropdown.SelectByIndex(0);
                        */

                        driver.FindElement(By.Id("formPesquisarAutorizado:txtChavePesquisaAutorizado014")).Click();
                        driver.FindElement(By.Id("formPesquisarAutorizado:txtChavePesquisaAutorizado014"))
                            .SendKeys(Keys.Home + _cnpj);

                        driver.FindElement(By.Id("formPesquisarAutorizado:bt027_8")).Click();


                        //page 4 - Capturar dados responsável
                        var razaoSocial = driver.FindElement(By.Id("txtrazaosocial020_4")).Text;
                        var logradouro = driver.FindElement(By.Id("txt3_logradouro020")).Text;
                        var bairro = driver.FindElement(By.Id("txt4_bairro020")).Text;
                        var municipio = driver.FindElement(By.Id("txt6_municipio020")).Text;
                        var estado = driver.FindElement(By.Id("txt7_uf020")).Text;
                        var cep = driver.FindElement(By.Id("txt8_cep020")).Text;

                        var nomeContato = driver.FindElement(By.Id("txt_nome_contato")).Text;
                        var cpfContato = driver.FindElement(By.Id("txt_contato_cpf")).Text;
                        var telefoneContato = driver.FindElement(By.Id("txt21_ddd020")).Text +
                                              driver.FindElement(By.Id("txt9_telefone020")).Text;
                        var ramalContato = driver.FindElement(By.Id("txt10_ramal020")).Text;
                        var emailContato = driver.FindElement(By.Id("txt11_email")).Text;

                        //--- passar para a próxima página

                        Actions actionPage4 = new Actions(driver);
                        menuDropDown = driver.FindElement(By.Id("j_idt12:lk_menu_consultas"));
                        actionPage4.MoveToElement(menuDropDown).Build().Perform();

                        driver.FindElement(By.Id("j_idt12:idMenuLinkEmpresaCaged")).Click();


                        // page 5 - Consultar Empresa
                        driver.FindElement(By.Id("formPesquisarEmpresaCAGED:txtcnpjRaiz")).Click();
                        driver.FindElement(By.Id("formPesquisarEmpresaCAGED:txtcnpjRaiz"))
                            .SendKeys(Keys.Home + _cnpj.Substring(0, 8));

                        driver.FindElement(By.Id("formPesquisarEmpresaCAGED:btConsultar")).Click();

                        //page 6 - Capturar dados empresa
                        var cnae = driver.FindElement(By.Id("formResumoEmpresaCaged:txtCodigoAtividadeEconomica")).Text;
                        var atividadeEconomica = driver
                            .FindElement(By.Id("formResumoEmpresaCaged:txtDescricaoAtividadeEconomica")).Text;
                        var noFilias =
                            Int32.Parse(driver.FindElement(By.Id("formResumoEmpresaCaged:txtNumFiliais")).Text);
                        var totalVinculos =
                            Int32.Parse(driver.FindElement(By.Id("formResumoEmpresaCaged:txtTotalVinculos")).Text);

                        #region Salvar dados de pessoa jurídica no objeto
                        var pessoaJuridica = new CagedPJModel { 
                            Cnpj = _cnpj,
                            RazaoSocial = razaoSocial,
                            Logradouro = logradouro,
                            Bairro = bairro,
                            Municipio = municipio,
                            Estado = estado,
                            Cep = cep,
                            NomeContato = nomeContato,
                            CpfContato = cpfContato,
                            TelefoneContato = telefoneContato,
                            RamalContato = ramalContato,
                            EmailContato = emailContato,
                            Cnae = cnae,
                            AtividadeEconomica = atividadeEconomica,
                            NoFilias = noFilias,
                            TotalVinculos = totalVinculos
                        };
                        #endregion

                        result = pessoaJuridica;
                    }
                    else if (_cpf != null)
                    {
                        Actions actionPage6 = new Actions(driver);
                        menuDropDown = driver.FindElement(By.Id("j_idt12:lk_menu_consultas"));
                        actionPage6.MoveToElement(menuDropDown).Build().Perform();

                        driver.FindElement(By.Id("j_idt12:idMenuLinkTrabalhador")).Click();


                        //page 7 - Consultar trabalhador
                        IWebElement pesquisarPorDropDown =
                            driver.FindElement(By.Id("formPesquisarTrabalhador:slctTipoPesquisaTrabalhador"));
                        new SelectElement(pesquisarPorDropDown).SelectByIndex(0);

                        driver.FindElement(By.Id("formPesquisarTrabalhador:txtChavePesquisa")).Click();
                        driver.FindElement(By.Id("formPesquisarTrabalhador:txtChavePesquisa"))
                            .SendKeys(Keys.Home + _cpf);

                        driver.FindElement(By.Id("formPesquisarTrabalhador:submitPesqTrab")).Click();


                        //Page 8 - Capturar dados trabalhador
                        var nomeTrabalhador = driver.FindElement(By.Id("txt2_Nome027")).Text;
                        var pisBaseTrabalhador = driver.FindElement(By.Id("txt1_Pis028")).Text;
                        var ctpsTrabalhador = driver.FindElement(By.Id("txt5_Ctps027")).Text;
                        var faixaPisTrabalhador = driver.FindElement(By.Id("txt4_SitPis027")).Text;
                        var nacionalidadeTrabalhador = driver.FindElement(By.Id("txt8_Nac027")).Text;
                        var grauInstrucaoTrabalhador = driver.FindElement(By.Id("txt12_Instr027")).Text;
                        var deficienteTrabalhador = driver.FindElement(By.Id("txt13_Def027")).Text;
                        var dataNascimentoTrabalhador = driver.FindElement(By.Id("txt4_datanasc027")).Text;
                        var sexoTrabalhador = driver.FindElement(By.Id("txt6_Sexo027")).Text;
                        var corTrabalhador = driver.FindElement(By.Id("txt10_Raca027")).Text;
                        var cepTrabalhador = driver.FindElement(By.Id("txtEstabCep91")).Text;
                        var tempoTrabalhoCaged = driver.FindElement(By.Id("txt26_Caged027")).Text + " meses";
                        var tempoTrabalhoRais = driver.FindElement(By.Id("txt27_Rais027")).Text + " meses";
                        

                        driver.Navigate().GoToUrl(driver.FindElement(By.CssSelector(".link > a:nth-child(1)")).GetAttribute("href"));
                        Console.WriteLine();

                        //Page 9 - Salvar PDF
                        ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
                        driver.SwitchTo().Window(windowHandles.Last());

                        IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));
                        wait.Until(driver1 =>
                            ((IJavaScriptExecutor) driver).ExecuteScript("return document.readyState")
                            .Equals("complete"));

                        var downloadFolderPath = $@"{AppDomain.CurrentDomain.BaseDirectory}temp\caged\";
                        if (!Directory.Exists(downloadFolderPath))
                            Directory.CreateDirectory(downloadFolderPath);

                        var nomeArquivo = nomeTrabalhador.Replace(" ", string.Empty);

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
                            Console.WriteLine("[CAGED] Ocorreu um erro ao tentar baixar o PDF! \nMensagem de erro: " + e);
                            result = null;
                            return CrawlerStatus.Skipped;
                        }

                        #region Salvar dados de pessoa física no objeto
                        var pessoaFisica = new CagedPFModel
                        {
                            Cpf = _cpf,
                            NomeTrabalhador = nomeTrabalhador,
                            PisBaseTrabalhador = pisBaseTrabalhador,
                            CtpsTrabalhador = ctpsTrabalhador,
                            FaixaPisTrabalhador = faixaPisTrabalhador,
                            NacionalidadeTrabalhador = nacionalidadeTrabalhador,
                            GrauInstrucaoTrabalhador = grauInstrucaoTrabalhador,
                            DeficienteTrabalhador = deficienteTrabalhador,
                            DataNascimentoTrabalhador = dataNascimentoTrabalhador,
                            SexoTrabalhador = sexoTrabalhador,
                            CorTrabalhador = corTrabalhador,
                            CepTrabalhador = cepTrabalhador,
                            TempoTrabalhoCaged = tempoTrabalhoCaged,
                            TempoTrabalhoRais = tempoTrabalhoRais,
                            Arquivo = arquivo
                        };
                        #endregion

                        result = pessoaFisica;

                    }

                    driver.Close();
                    Console.WriteLine("CagedCrawler OK");
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage(typeof(CagedCrawler), e.Message);
                result = null;
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage(typeof(CagedCrawler), e.Message);
                result = null;
                return CrawlerStatus.Error;
            }
        }
    }
}
