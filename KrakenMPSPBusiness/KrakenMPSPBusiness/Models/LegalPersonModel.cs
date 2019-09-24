using System;

using KrakenMPSPBusiness.Enum;
using KrakenMPSPBusiness.Interface;

namespace KrakenMPSPBusiness.Models
{
    public class LegalPersonModel : IPerson
    {
        public Guid Id { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string CPFDoFundador { get; set; }
        public string Contador { get; set; }

        public KindPerson Type => KindPerson.LegalPerson;
        public bool Completed { get; set; }
    }
}
