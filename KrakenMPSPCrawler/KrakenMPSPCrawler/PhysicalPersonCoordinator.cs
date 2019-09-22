using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Crawlers;
using KrakenMPSPCrawler.Business.Model;

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
            AddModule(new SielCrawler(
                "fiap",
                "fiap123",
                "123456",
                physicalPerson.NomeCompleto,
                physicalPerson.NomeDaMae,
                physicalPerson.DataDeNascimento));
            AddModule(new SivecCrawler("fiap", "fiap123", "123456", physicalPerson.NomeCompleto, physicalPerson.RG));
            AddModule(new CagedCrawler(physicalPerson.Type, "fiap", "fiap123", physicalPerson.CPF));
        }
    }
}
