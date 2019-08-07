namespace KrakenMPSPCrawler.Models
{
    public class PhysicalPersonModel
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string DataDeNascimento { get; set; }
        public string NomeDaMae { get; set; }

        public PhysicalPersonModel(string nomeCompleto, string cpf, string dataDeNascimento, string nomeDaMae)
        {
            NomeCompleto = nomeCompleto;
            CPF = cpf;
            DataDeNascimento = dataDeNascimento;
            NomeDaMae = nomeDaMae;
        }

    }
}
