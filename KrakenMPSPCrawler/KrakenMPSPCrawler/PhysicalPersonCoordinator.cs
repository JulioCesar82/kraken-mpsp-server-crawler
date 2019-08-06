using System.Collections.Generic;

using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Business;
using KrakenMPSPCrawler.Crawlers;
using KrakenMPSPCrawler.Business.Model;

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
