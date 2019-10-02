using System;
using System.Linq;

using KrakenMPSPBusiness.Enums;
using KrakenMPSPBusiness.Interfaces;

namespace KrakenMPSPBusiness.Models
{
    public class PhysicalPersonModel : PersonModel, IPerson
    {
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public KindPerson Type => KindPerson.PhysicalPerson;

        public ArispModel Arisp;
        public ArpenspModel Arpensp;
        public SielModel Siel;
        public SivecModel Sivec;
        public CagedPFModel Caged;
        public CensecModel Censec;
        //public DetranModel Detran;

        public string GetNomeCompleto()
        {
            string[] listNomes = new string[] {
                Arpensp?.NomeConjugeA1,
                Arpensp?.NovoNomeConjugeA2,
                Siel?.Nome,
                Sivec?.Nome,
                Sivec?.Outros.Nome,
                Caged?.NomeTrabalhador
            };
            return listNomes.FirstOrDefault(x => !string.IsNullOrEmpty(x));
        }

        public string GetNomeDaMae()
        {
            string[] listNomesMae = new string[] {
                Siel?.NomeMae,
                Sivec?.NomeMae,
                Sivec?.Outros.NomeMae
            };
            return listNomesMae.FirstOrDefault(x => !string.IsNullOrEmpty(x));
        }

        public string GetDataDeNascimento()
        {
            string[] listDatasNascimento = new string[] {
                Siel?.DataNascimento,
                Sivec?.DataNascimento,
                Sivec?.Outros.DataNascimento,
                Caged?.DataNascimentoTrabalhador
            };
            return listDatasNascimento.FirstOrDefault(x => !string.IsNullOrEmpty(x));
        }
    }
}
