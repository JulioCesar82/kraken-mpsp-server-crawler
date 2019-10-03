using System;
using System.Collections.Generic;
using System.Text;

namespace KrakenMPSPBusiness.Models
{
    public class DetranCrawlerModel
    {
        public long Id { get; set; }
        public string Renach { get; set; }
        public string Categoria { get; set; }
        public string Emissao { get; set; }
        public string Nascimento { get; set; }
        public string Nome { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string Registro { get; set; }
        public string Tipografo { get; set; }
        public string Identidade { get; set; }
    }
}
