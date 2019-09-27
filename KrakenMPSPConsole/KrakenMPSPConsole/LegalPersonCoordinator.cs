using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Models;
using KrakenMPSPConsole.Crawlers;

namespace KrakenMPSPConsole
{
    public class LegalPersonCoordinator : Coordinator
    {
        public LegalPersonCoordinator(LegalPersonModel legalPerson)
        {
            // Classe de Crawler base, apenas duplique
            // AddModule(new ExampleCrawler("julio+cesar"));

            AddModule(new ArispCrawler(legalPerson.Type, legalPerson.CNPJ));
            AddModule(new CagedCrawler(legalPerson.Type, "fiap", "senha", legalPerson.CNPJ));
            AddModule(new CensecCrawler("fiap", "fiap123", legalPerson.CNPJ));
            AddModule(new DetranCrawler("12345678", "fiap123", legalPerson.CNPJ));
        }
    }
}
