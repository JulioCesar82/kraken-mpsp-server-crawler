using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using KrakenMPSPBusiness.Models;

using KrakenMPSPCrawler;

using KrakenMPSPConsole.Helpers;

namespace KrakenMPSPConsole
{
    public class SearchLegalPerson
    {
        private const string _apiaddress = "http://localhost:8784/api";
        private static HttpClient _httpClient;
        private static HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());

        public static void Run()
        {
            Console.WriteLine("starting search");

            var runners = Task.Run(async() => await SearchPending());
            runners.Wait();
        }

        public async static Task SearchPending()
        {
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
                    var response2 = HttpClient.PostAsync($"{_apiaddress}/LegalPerson", resourcesFoundJsonJsonString).Result;
                    Console.WriteLine(response2);

                    empresa.Completed = result.Completed;
                    var empresaJson = JsonConvert.SerializeObject(empresa);
                    var empresaJsonJsonString = new StringContent(empresaJson, Encoding.UTF8, "application/json");
                    var response3 = HttpClient.PutAsync($"{_apiaddress}/LegalPerson/{empresa.Id}", empresaJsonJsonString).Result;
                    Console.WriteLine(response3);
                }
            }

            Console.WriteLine("finished search");
            Console.ReadLine();
        }
    }
}
