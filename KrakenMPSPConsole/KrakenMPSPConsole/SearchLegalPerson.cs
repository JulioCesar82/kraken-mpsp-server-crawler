using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using KrakenMPSPBusiness.Models;

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
            }

            Console.WriteLine("finished search");
            Console.ReadLine();
        }
    }
}
