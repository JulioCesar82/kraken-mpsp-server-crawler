using System;
using System.Collections.Generic;
using System.Text;

namespace KrakenMPSPCrawler.Models
{
    class CensecCrawlerModel
    {
        public string Livro { get; set; }
        public string Carga { get; set; }
        public string Data { get; set; }
        public string Ato { get; set; }
        public string DataAto { get; set; }
        public string Folha { get; set; }
        public string Nomes { get; set; }
        public string CpfsCnpjs { get; set; }
        public string Qualidads { get; set; }
        public string Uf { get; set; }
        public string Municipio { get; set; }
        public string Cartorio { get; set; }
        public string Telefones { get; set; }
        public string TipoTel { get; set; }
        public string Ramal { get; set; }
        public string Contato { get; set; }
        public string Status { get; set; }

        public class DadosCartorio
        {
        }
    }
}
