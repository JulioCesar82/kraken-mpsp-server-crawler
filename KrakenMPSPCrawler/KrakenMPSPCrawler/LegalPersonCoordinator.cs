using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Crawlers;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler
{
    public class LegalPersonCoordinator : Coordinator
    {
        public LegalPersonCoordinator(LegalPersonModel legalPerson)
        {
            // Classe de Crawler base, apenas duplique
            // AddModule(new ExampleCrawler("julio+cesar"));

            AddModule(new ArispCrawler(legalPerson.Type, legalPerson.CNPJ));
        }
    }
}
