using System;

using KrakenMPSPCrawler.Enum;
using KrakenMPSPCrawler.Interface;

namespace KrakenMPSPCrawler.Models
{
    public class LegalPersonModel : IPerson
    {
        public Guid Id { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string CPFDoFundador { get; set; }
        public string Contador { get; set; }

        public KindPerson Type => KindPerson.LegalPerson;
    }
}
