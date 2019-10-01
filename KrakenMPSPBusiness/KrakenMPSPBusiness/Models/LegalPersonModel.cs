using System;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Interfaces;

namespace KrakenMPSPBusiness.Models
{
    public class LegalPersonModel : PersonModel, IPerson
    {
        public Guid Id { get; set; }
        public string CNPJ { get; set; }
        public KindPerson Type => KindPerson.LegalPerson;

        public ArispModel Arisp;
        public CagedPJModel Caged;
        public CensecModel Censec;
        //public DetranModel Detran;
    }
}
