using System;

using KrakenMPSPBusiness.Enum;
using KrakenMPSPBusiness.Interface;

namespace KrakenMPSPBusiness.Models
{
    public class PhysicalPersonModel : IPerson
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string DataDeNascimento { get; set; }
        public string NomeDaMae { get; set; }

        public KindPerson Type => KindPerson.PhysicalPerson;
        public bool Completed { get; set; }
    }
}
