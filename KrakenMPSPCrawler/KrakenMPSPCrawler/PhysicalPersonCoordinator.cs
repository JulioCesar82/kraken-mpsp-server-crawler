using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Business;
using KrakenMPSPCrawler.Crawlers;

namespace KrakenMPSPCrawler
{
    public class PhysicalPersonCoordinator : Coordinator
    {
        public PhysicalPersonCoordinator(PhysicalPersonModel physicalPerson)
        {
            AddModule(new ArispCrawler(physicalPerson.Type, physicalPerson.CPF));
            AddModule(new SielCrawler("fiap", "fiap123", "123456", physicalPerson.NomeCompleto, physicalPerson.NomeDaMae, physicalPerson.DataDeNascimento));
            AddModule(new SivecCrawler());
        }
    }
}
