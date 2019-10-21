using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using KrakenMPSPBusiness.Models;

namespace KrakenMPSPConsole
{
    public class Program
    {
        private const string _endpoint = "http://localhost:8784/api";
        private static HttpClient _httpClient;
        private static HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());

        public static void Main(string[] args)
        {

            //while (true)
            //{
                try
                {
                /*
                 * MultiThread
                 *
                List<Task> tasks = new List<Task>();
                // funcoes de busca
                tasks.Add(SearchLegalPerson());
                tasks.Add(SearchPhysicalPerson());

                var buscas = Task.Factory.StartNew(() =>
                {
                    var iBusca = 0;
                    foreach (Task task in tasks)
                    {
                        Task.Factory.StartNew((x) => 
                            task.Wait(),
                            iBusca,
                            TaskCreationOptions.AttachedToParent);

                        iBusca++;
                    }
                });

                buscas.Wait();
                */

                    SearchLegalPerson().Wait();
                    SearchPhysicalPerson().Wait();


                    Console.WriteLine("********");
                    Console.WriteLine("finished search");
                    Console.WriteLine("********");
                    //Thread.Sleep(TimeSpan.FromMinutes(30));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            //}
            Console.ReadLine();
        }

        private static async Task<bool> SearchLegalPerson()
        {
            Console.WriteLine("starting SearchLegalPerson");
            HttpResponseMessage response;

            try
            {
                response = await HttpClient.GetAsync($"{_endpoint}/LegalPerson");
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            List<LegalPersonModel> listaLegalPerson = JsonConvert.DeserializeObject<List<LegalPersonModel>>(responseBodyAsText);

            var empresas = listaLegalPerson.Where(x => x.ResultadoFinal == null || !x.ResultadoFinal.Completed).ToList();
            foreach (var empresa in empresas)
            {
                Console.WriteLine("********");
                Console.WriteLine($"Buscando Empresa: {empresa.CNPJ}");
                Console.WriteLine("********");
                // executando o Crawler
                LegalPersonModel resultCrawler;
                try
                {
                    resultCrawler = new LegalPersonCoordinator(empresa).StartSearch();
                    Console.WriteLine("Completou a busca? {0}", resultCrawler.ResultadoFinal.Completed);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }

                Console.WriteLine("Salvando informações obtidas...");
                var empresaJson = JsonConvert.SerializeObject(resultCrawler);
                var empresaJsonJsonString = new StringContent(empresaJson, Encoding.UTF8, "application/json");

                var response3 = HttpClient.PutAsync($"{_endpoint}/LegalPerson/{empresa.Id}", empresaJsonJsonString).Result;
                Console.WriteLine(response3);
            }

            return true;
        }

        private static async Task<bool> SearchPhysicalPerson()
        {
            Console.WriteLine("starting SearchPhysicalPerson");
            HttpResponseMessage response;

            try
            {
                response = await HttpClient.GetAsync($"{_endpoint}/PhysicalPerson");
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            List<PhysicalPersonModel> listaPhysicalPerson = JsonConvert.DeserializeObject<List<PhysicalPersonModel>>(responseBodyAsText);

            var pessoas = listaPhysicalPerson.Where(x => x.ResultadoFinal == null || !x.ResultadoFinal.Completed).ToList();
            foreach (var pessoa in pessoas)
            {
                Console.WriteLine("********");
                Console.WriteLine($"Buscando Pessoa: {pessoa.CPF}");
                Console.WriteLine("********");
                // executando o Crawler
                PhysicalPersonModel resultCrawler;
                try
                {
                    resultCrawler = new PhysicalPersonCoordinator(pessoa).StartSearch(); ;
                    Console.WriteLine("Completou a busca? {0}", resultCrawler.ResultadoFinal.Completed);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }

                Console.WriteLine("Salvando informações obtidas...");
                var pessoaJson = JsonConvert.SerializeObject(resultCrawler);
                var pessoaJsonJsonString = new StringContent(pessoaJson, Encoding.UTF8, "application/json");

                var response3 = HttpClient.PutAsync($"{_endpoint}/PhysicalPerson/{pessoa.Id}", pessoaJsonJsonString).Result;
                Console.WriteLine(response3);
            }

            return true;
        }
    }
}
