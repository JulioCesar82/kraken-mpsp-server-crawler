using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Business;
using KrakenMPSPCrawler.Business.Model;
using KrakenMPSPCrawler.Crawlers;

namespace KrakenMPSPCrawler
{
    public class PhysicalPersonCoordinator : Coordinator
    {
        public PhysicalPersonCoordinator(PhysicalPersonModel physicalPerson)
        {
            // Classe de Crawler base, apenas duplique
            //AddModule(new ExampleCrawler("julio+cesar"));

            AddModule(new ArispCrawler(physicalPerson.Type, physicalPerson.CPF));
            AddModule(new ArpenspCrawler("123456"));
            AddModule(new SielCrawler("fiap", "fiap123", "123456", physicalPerson.NomeCompleto, physicalPerson.NomeDaMae, physicalPerson.DataDeNascimento));
        }
    }
}
