using System;
using System.Text;
using System.Linq;
using System.Net.Http;
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
            List<Task> tasks = new List<Task>();
            tasks.Add(SearchLegalPerson());
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
                            Task.Factory.StartNew((x) => task.Wait()
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

            HttpResponseMessage response = await HttpClient.GetAsync($"{_endpoint}/LegalPerson");
            Console.WriteLine(response);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            List<LegalPersonModel> listaLegalPerson = JsonConvert.DeserializeObject<List<LegalPersonModel>>(responseBodyAsText);
            Console.WriteLine(listaLegalPerson);

            var empresas = listaLegalPerson.Where(x => !x.Completed).ToList();
            foreach (var empresa in empresas)
            {
                Console.WriteLine($"Empresa: {empresa.CNPJ}");
                var result = new LegalPersonCoordinator(empresa);
                Console.WriteLine("Completou a busca? {0}", result.Completed);

                Console.WriteLine("Salvando informações obtidas...");
                empresa.Completed = result.Completed;
                var empresaJson = JsonConvert.SerializeObject(empresa);
                var empresaJsonJsonString = new StringContent(empresaJson, Encoding.UTF8, "application/json");
                var response3 = HttpClient.PutAsync($"{_endpoint}/LegalPerson/{empresa.Id}", empresaJsonJsonString).Result;
                Console.WriteLine(response3);
            }

            return true;
        }

        private static async Task<bool> SearchPhysicalPerson()
        {
            Console.WriteLine("starting SearchPhysicalPerson");

            HttpResponseMessage response = await HttpClient.GetAsync($"{_endpoint}/PhysicalPerson");
            Console.WriteLine(response);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            string responseBodyAsText = await response.Content.ReadAsStringAsync();
            List<PhysicalPersonModel> listaPhysicalPerson = JsonConvert.DeserializeObject<List<PhysicalPersonModel>>(responseBodyAsText);
            Console.WriteLine(listaPhysicalPerson);

            var pessoas = listaPhysicalPerson.Where(x => !x.Completed).ToList();
            foreach (var pessoa in pessoas)
            {
                Console.WriteLine($"PESSOA: {pessoa.CPF}");
                var result = new PhysicalPersonCoordinator(pessoa);
                Console.WriteLine("Completou a busca? {0}", result.Completed);

                Console.WriteLine("Salvando informações obtidas...");
                pessoa.Completed = result.Completed;
                var pessoaJson = JsonConvert.SerializeObject(pessoa);
                var pessoaJsonJsonString = new StringContent(pessoaJson, Encoding.UTF8, "application/json");
                var response3 = HttpClient.PutAsync($"{_endpoint}/PhysicalPerson/{pessoa.Id}", pessoaJsonJsonString).Result;
                Console.WriteLine(response3);
            }

            return true;
        }
    }
}
