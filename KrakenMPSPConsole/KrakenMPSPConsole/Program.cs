using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

using KrakenMPSPBusiness.Models;

using KrakenMPSPCrawler;

using KrakenMPSPConsole.Helpers;

namespace KrakenMPSPConsole
{
    public class Program
    {
        private const string _apiaddress = "http://localhost:8784/api";
        private static HttpClient _httpClient;
        private static HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());

        public static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();
            //tasks.Add(SearchLegalPerson());
            tasks.Add(SearchPhysicalPerson());

            //while (true)
            //{
                try
                {
                    var buscas = Task.Factory.StartNew(() =>
                    {
                        var iBusca = 0;
                        foreach (Task task in tasks)
                        {
                            Task.Factory.StartNew(async (x) => task.Wait()
                                , iBusca, TaskCreationOptions.AttachedToParent);
                            iBusca++;
                        }
                    });

                    buscas.Wait();
                    Console.WriteLine("finished search");
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

            HttpResponseMessage response = await HttpClient.GetAsync($"{_apiaddress}/LegalPerson");
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                List<LegalPersonModel> listaLegalPerson = JsonConvert.DeserializeObject<List<LegalPersonModel>>(responseBodyAsText);
                Console.WriteLine(listaLegalPerson);

                var empresas = listaLegalPerson.Where(x => !x.Completed).ToList();
                foreach (var empresa in empresas)
                {
                    Console.WriteLine($"Empresa: {empresa.NomeFantasia}");
                    var crawler = new LegalPersonCoordinator(empresa);
                    var result = crawler.Run();
                    Console.WriteLine("Completou a busca? {0}", result.Completed);

                    // gerando arquivo com os resultados
                    var resourcesFound = new ResourcesFoundModel
                    {
                        ArquivoReferencia = empresa.Id,
                        Type = empresa.Type
                    };
                    foreach (var information in result.Informations)
                    {
                        ManagerObjectHelper.CopyValues(resourcesFound, information);
                    }

                    Console.WriteLine("Salvando informações obtidas...");
                    var resourcesFoundJson = JsonConvert.SerializeObject(resourcesFound);
                    var resourcesFoundJsonJsonString = new StringContent(resourcesFoundJson, Encoding.UTF8, "application/json");
                    var response2 = HttpClient.PostAsync($"{_apiaddress}/ResourcesFound", resourcesFoundJsonJsonString).Result;
                    Console.WriteLine(response2);

                    empresa.Completed = result.Completed;
                    var empresaJson = JsonConvert.SerializeObject(empresa);
                    var empresaJsonJsonString = new StringContent(empresaJson, Encoding.UTF8, "application/json");
                    var response3 = HttpClient.PutAsync($"{_apiaddress}/LegalPerson/{empresa.Id}", empresaJsonJsonString).Result;
                    Console.WriteLine(response3);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private static async Task<bool> SearchPhysicalPerson()
        {
            Console.WriteLine("starting SearchPhysicalPerson");

            HttpResponseMessage response = await HttpClient.GetAsync($"{_apiaddress}/PhysicalPerson");
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                List<PhysicalPersonModel> listaPhysicalPerson = JsonConvert.DeserializeObject<List<PhysicalPersonModel>>(responseBodyAsText);
                Console.WriteLine(listaPhysicalPerson);

                var pessoas = listaPhysicalPerson.Where(x => !x.Completed).ToList();
                foreach (var pessoa in pessoas)
                {
                    Console.WriteLine($"PESSOA: {pessoa.NomeCompleto}");
                    var crawler = new PhysicalPersonCoordinator(pessoa);
                    var result = crawler.Run();
                    Console.WriteLine("Completou a busca? {0}", result.Completed);

                    // gerando arquivo com os resultados
                    var resourcesFound = new ResourcesFoundModel
                    {
                        ArquivoReferencia = pessoa.Id,
                        Type = pessoa.Type
                    };
                    foreach (var information in result.Informations)
                    {
                        ManagerObjectHelper.CopyValues(resourcesFound, information);
                    }

                    Console.WriteLine("Salvando informações obtidas...");
                    var resourcesFoundJson = JsonConvert.SerializeObject(resourcesFound);
                    var resourcesFoundJsonJsonString = new StringContent(resourcesFoundJson, Encoding.UTF8, "application/json");
                    var response2 = HttpClient.PostAsync($"{_apiaddress}/ResourcesFound", resourcesFoundJsonJsonString).Result;
                    Console.WriteLine(response2);

                    pessoa.Completed = result.Completed;
                    var pessoaJson = JsonConvert.SerializeObject(pessoa);
                    var pessoaJsonJsonString = new StringContent(pessoaJson, Encoding.UTF8, "application/json");
                    var response3 = HttpClient.PutAsync($"{_apiaddress}/PhysicalPerson/{pessoa.Id}", pessoaJsonJsonString).Result;
                    Console.WriteLine(response3);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
