using KrakenMPSPBusiness.Models;

using KrakenMPSPCrawler.Model;
using KrakenMPSPCrawler.Crawlers;

namespace KrakenMPSPCrawler
{
    public class LegalPersonCoordinator : Coordinator
    {
        public LegalPersonCoordinator(LegalPersonModel legalPerson)
        {
            // Classe de Crawler base, apenas duplique
            // AddModule(new ExampleCrawler("julio+cesar"));

            AddModule(new ArispCrawler(legalPerson.Type, legalPerson.CNPJ));
            AddModule(new CagedCrawler(legalPerson.Type, "fiap", "senha", legalPerson.CNPJ));
        }
    }
}
