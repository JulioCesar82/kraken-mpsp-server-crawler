using System.Collections.Generic;

namespace KrakenMPSPBusiness.Models
{
    public class ArispCrawlerModel
    {
        public long Id { get; set; }
        public List<Processo> Processos { get; set; }
    }

    public class Processo
    {
        public long Id { get; set; }
        public string Cidade { get; set; }
        public string Cartorio { get; set; }
        public string Matricula { get; set; }
        public string Arquivo { get; set; }
    }
}
