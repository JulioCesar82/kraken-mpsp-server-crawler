using KrakenMPSPCrawler.Enum;

namespace KrakenMPSPCrawler.Models
{
    public class PhysicalPersonModel : PersonModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string DataDeNascimento { get; set; }
        public string NomeDaMae { get; set; }

        public override KindPerson getTypePerson()
        {
            return KindPerson.PhysicalPerson;
        }
    }
}
