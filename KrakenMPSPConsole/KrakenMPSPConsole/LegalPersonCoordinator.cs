using System.Linq;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Crawlers;

namespace KrakenMPSPConsole
{
    public class LegalPersonCoordinator : Coordinator
    {
        private LegalPersonModel _find;
        //private CrawlerResult Result { get; set; }

        public LegalPersonCoordinator(LegalPersonModel legalPerson)
        {
            _find = legalPerson;
        }

        public LegalPersonModel StartSearch()
        {
            // Classe de Crawler base, apenas duplique
            // AddModule(new ExampleCrawler("julio+cesar"));

            var arispOut = new object();
            var arispResult = new ArispCrawler(_find.Type, _find.CNPJ).Execute(out arispOut);
            AddModule(arispResult);
            _find.Arisp = (ArispModel) arispOut;

            var cagedOut = new object();
            var cagedResult =
                new CagedCrawler("fiap", "senha", _find.Type, _find.CNPJ).Execute(out cagedOut);
            AddModule(cagedResult);
            _find.Caged = (CagedPJModel)cagedOut;

            var censecOut = new object();
            var censecResult = new CensecCrawler("fiap", "fiap123", _find.CNPJ).Execute(out censecOut);
            AddModule(censecResult);
            _find.Censec = (CensecModel)censecOut;

            var detranOut = new object();
            var detranResult = new DetranCrawler("12345678", "fiap123", _find.Type, _find.CNPJ).Execute(out detranOut);
            AddModule(detranResult);
            //legalPerson.Detral = (DetralModel)detranOut;

            _find.ResultadoFinal = new CrawlerResult
            {
                FindTotal = _portais.Count,
                TotalErrors = _portais.Where(portal => portal == CrawlerStatus.Error).ToList().Count
            };
            return _find;
        }
    }
}
