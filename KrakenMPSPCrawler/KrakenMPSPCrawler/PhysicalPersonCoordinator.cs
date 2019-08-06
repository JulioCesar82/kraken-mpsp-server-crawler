using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Business;
using KrakenMPSPCrawler.Crawlers;

namespace KrakenMPSPCrawler
{
    public class PhysicalPersonCoordinator : Coordinator
    {

        public PhysicalPersonCoordinator(PhysicalPersonModel physicalPerson)
        {
            AddModule(new GoogleCrawler(physicalPerson.NomeCompleto));
        }
    }
}
