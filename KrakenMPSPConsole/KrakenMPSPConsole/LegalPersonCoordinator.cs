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

            var arispOut = new object();
            var arispResult = new ArispCrawler(legalPerson.Type, legalPerson.CNPJ).Execute(out arispOut);
            AddModule(arispResult);
            legalPerson.Arisp = (ArispModel) arispOut;

            var cagedOut = new object();
            var cagedResult =
                new CagedCrawler(legalPerson.Type, "fiap", "senha", legalPerson.CNPJ).Execute(out cagedOut);
            AddModule(cagedResult);
            legalPerson.Caged = (CagedPJModel)cagedOut;

            var censecOut = new object();
            var censecResult = new CensecCrawler("fiap", "fiap123", legalPerson.CNPJ).Execute(out censecOut);
            AddModule(censecResult);
            legalPerson.Censec = (CensecModel)censecOut;

            var detranOut = new object();
            var detranResult = new DetranCrawler(legalPerson.Type, "12345678", "fiap123", legalPerson.CNPJ).Execute(out detranOut);
            AddModule(detranResult);
            //legalPerson.Detral = (DetralModel)detranOut;
        }
    }
}
