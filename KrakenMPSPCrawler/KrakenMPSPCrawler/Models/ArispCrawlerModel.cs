using System.Collections.Generic;

namespace KrakenMPSPCrawler.Models
{
    public class ArispCrawlerModel
    {
        private List<Processo> Processos { get; set; }
    }

    public class Processo
    {
        public string Cidade { get; set; }
        public string Cartorio { get; set; }
        public string Matricula { get; set; }
        public string Arquivo { get; set; }
    }
}
