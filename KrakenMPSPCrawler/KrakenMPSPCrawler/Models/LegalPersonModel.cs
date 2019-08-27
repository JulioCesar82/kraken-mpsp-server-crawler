using KrakenMPSPCrawler.Enum;

namespace KrakenMPSPCrawler.Models
{
    public class LegalPersonModel : PersonModel
    {
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string CPFDoFundador { get; set; }
        public string Contador { get; set; }

        public LegalPersonModel(string nomeFantasia, string cnpj, string cpfDoFundador, string contador) 
            : base(KindPerson.LegalPerson)
        {
            NomeFantasia = nomeFantasia;
            CNPJ = cnpj;
            CPFDoFundador= cpfDoFundador;
            Contador= contador;
        }
    }
}
