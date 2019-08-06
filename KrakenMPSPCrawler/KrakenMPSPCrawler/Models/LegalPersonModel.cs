namespace KrakenMPSPCrawler.Models
{
    public class LegalPersonModel
    {
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string CPFDoFundador { get; set; }
        public string Contador { get; set; }

        public LegalPersonModel(string nomeFantasia, string cnpj, string cpfDoFundador, string contador)
        {
            NomeFantasia = nomeFantasia;
            CNPJ = cnpj;
            CPFDoFundador= cpfDoFundador;
            Contador= contador;
        }
    }
}
