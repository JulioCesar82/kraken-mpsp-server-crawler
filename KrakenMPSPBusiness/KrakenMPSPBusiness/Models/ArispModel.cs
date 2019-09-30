using System.Collections.Generic;

namespace KrakenMPSPBusiness.Models
{
    public class ArispModel
    {
        public List<ProcessoModel> Processos { get; set; }
    }

    public class ProcessoModel
    {
        public string Cidade { get; set; }
        public string Cartorio { get; set; }
        public string Matricula { get; set; }
        public string Arquivo { get; set; }
    }
}