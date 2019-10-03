using System.Linq;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole.Crawlers;

namespace KrakenMPSPConsole
{
    public class PhysicalPersonCoordinator : Coordinator
    {
        private PhysicalPersonModel _find;
        //private CrawlerResult Result { get; set; }

        public PhysicalPersonCoordinator(PhysicalPersonModel physicalPerson)
        {
            _find = physicalPerson;
        }

        public PhysicalPersonModel StartSearch()
        {
            // Classe de Crawler base, apenas duplique
            //AddModule(new ExampleCrawler("julio+cesar"));

            var arispOut = new object();
            var arispResult = new ArispCrawler(_find.Type, _find.CPF).Execute(out arispOut);
            AddModule(arispResult);
            _find.Arisp = (ArispModel)arispOut;

            var arpenspOut = new object();
            var arpenspResult = new ArpenspCrawler("123456").Execute(out arpenspOut);
            AddModule(arpenspResult);
            _find.Arpensp = (ArpenspModel)arpenspOut;

            var cagedOut = new object();
            var cagedResult =
                new CagedCrawler("fiap", "fiap123", _find.Type, _find.CPF).Execute(out cagedOut);
            AddModule(cagedResult);
            _find.Caged = (CagedPFModel)cagedOut;

            var censecOut = new object();
            var censecResult = new CensecCrawler("fiap", "fiap123", _find.CPF).Execute(out censecOut);
            AddModule(censecResult);
            _find.Censec = (CensecModel)censecOut;

            var sivecOut = new object();
            var sivecResult =
                new SivecCrawler("fiap", "fiap123", "123456", _find.GetNomeCompleto(), _find.RG).Execute(
                    out sivecOut);
            AddModule(sivecResult);
            _find.Sivec = (SivecModel)sivecOut;

            var detranOut = new object();
            var detranResult = new DetranCrawler("12345678", "fiap123", "123456", _find.Type, _find.CPF).Execute(out detranOut);
            AddModule(detranResult);
            _find.Detran = (DetranModel)detranOut;

            var sielOut = new object();
            var sielResult = new SielCrawler(
                "fiap",
                "fiap123",
                "123456",
                _find.GetNomeCompleto(),
                _find.GetNomeDaMae(),
                _find.GetDataDeNascimento()
            ).Execute(out sielOut);
            AddModule(sielResult);
            _find.Siel = (SielModel)sielOut;

            _find.ResultadoFinal = new CrawlerResult
            {
                FindTotal = _portais.Count,
                TotalErrors = _portais.Where(portal  => portal == CrawlerStatus.Error).ToList().Count
            };
            return _find;
        }
    }
}
