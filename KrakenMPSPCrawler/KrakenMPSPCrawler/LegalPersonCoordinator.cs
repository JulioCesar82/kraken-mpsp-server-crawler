using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Business;
using KrakenMPSPCrawler.Crawlers;

namespace KrakenMPSPCrawler
{
    public class LegalPersonCoordinator : Coordinator
    {
        public LegalPersonCoordinator(LegalPersonModel legalPerson)
        {
            AddModule(new ArispCrawler(legalPerson.Type, legalPerson.CNPJ));
            AddModule(new SivecCrawler());   
        }
    }
}
