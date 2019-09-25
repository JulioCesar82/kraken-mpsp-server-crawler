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
    public class SearchPhysicalPerson
    {
        private const string _apiaddress = "http://localhost:8784/api";
        private static HttpClient _httpClient;
        private static HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());

        public static void Run()
        {
            Console.WriteLine("starting search");

            var runners = Task.Run(async () => await SearchPending());
            runners.Wait();
        }

        public async static Task SearchPending()
        {
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
            }

            Console.WriteLine("finished search");
            Console.ReadLine();
        }
    }
}
