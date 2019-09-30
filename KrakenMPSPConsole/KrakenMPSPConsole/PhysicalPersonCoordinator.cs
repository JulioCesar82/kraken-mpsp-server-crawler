using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Models;
using KrakenMPSPConsole.Crawlers;

namespace KrakenMPSPConsole
{
    public class PhysicalPersonCoordinator : Coordinator
    {
        public PhysicalPersonCoordinator(PhysicalPersonModel physicalPerson)
        {
            // Classe de Crawler base, apenas duplique
            //AddModule(new ExampleCrawler("julio+cesar"));

            var arispOut = new object();
            var arispResult = new ArispCrawler(physicalPerson.Type, physicalPerson.CPF).Execute(out arispOut);
            AddModule(arispResult);
            physicalPerson.Arisp = (ArispModel)arispOut;

            var arpenspOut = new object();
            var arpenspResult = new ArpenspCrawler("123456").Execute(out arpenspOut);
            AddModule(arpenspResult);
            physicalPerson.Arpensp = (ArpenspModel)arpenspOut;

            var sielOut = new object();
            var sielResult = new SielCrawler(
                "fiap",
                "fiap123",
                "123456",
                physicalPerson.NomeCompleto,
                physicalPerson.NomeDaMae,
                physicalPerson.DataDeNascimento).Execute(out sielOut);
            AddModule(sielResult);
            physicalPerson.Siel = (SielModel)sielOut;

            var sivecOut = new object();
            var sivecResult =
                new SivecCrawler("fiap", "fiap123", "123456", physicalPerson.NomeCompleto, physicalPerson.RG).Execute(
                    out sivecOut);
            AddModule(sivecResult);
            physicalPerson.Sivec = (SivecModel)sivecOut;

            var cagedOut = new object();
            var cagedResult =
                new CagedCrawler(physicalPerson.Type, "fiap", "fiap123", physicalPerson.CPF).Execute(out cagedOut);
            AddModule(cagedResult);
            physicalPerson.Caged = (CagedPFModel)cagedOut;

            var censecOut = new object();
            var censecResult = new CensecCrawler("fiap", "fiap123", physicalPerson.CPF).Execute(out censecOut);
            AddModule(censecResult);
            physicalPerson.Censec = (CensecModel)censecOut;

            var detranOut = new object();
            var detranResult = new DetranCrawler("12345678", "fiap123", physicalPerson.CPF).Execute(out detranOut);
            AddModule(detranResult);
            //physicalPerson.Detran = (DetranModel)detranOut;
        }
    }
}
