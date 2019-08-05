using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Models
{
    public class PhysicalPersonModel : SearchResult
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string DataDeNascimento { get; set; }
        public string NomeDaMae { get; set; }
    }
}
