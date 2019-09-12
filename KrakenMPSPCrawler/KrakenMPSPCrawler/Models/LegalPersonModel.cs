using KrakenMPSPCrawler.Enum;

namespace KrakenMPSPCrawler.Models
{
    public class LegalPersonModel : PersonModel
    {
        public long Id { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string CPFDoFundador { get; set; }
        public string Contador { get; set; }

        public override KindPerson getTypePerson()
        {
            return KindPerson.LegalPerson;
        }
    }
}
