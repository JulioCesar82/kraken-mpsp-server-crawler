using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Models
{
    public class LegalPersonModel : SearchResult
    {
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string CPFDoFundador { get; set; }
        public string Contador { get; set; }
    }
}
