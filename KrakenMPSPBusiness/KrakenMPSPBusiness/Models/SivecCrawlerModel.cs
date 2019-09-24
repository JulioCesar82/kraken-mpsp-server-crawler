namespace KrakenMPSPBusiness.Models
{
    public class SivecCrawlerModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string DataNascimento { get; set; }
        public string RG { get; set; }
        public string NumControle { get; set; }
        public string TipoRG { get; set; }
        public string DataEmissaoRG { get; set; }
        public string Alcunha { get; set; }
        public string EstadoCivil { get; set; }
        public string Naturalidade { get; set; }
        public string Naturalizado { get; set; }
        public string PostoIdentificacao { get; set; }
        public string GrauInstrucao { get; set; }
        public string FormulaFundamental { get; set; }
        public string NomePai { get; set; }
        public string CorOlhos { get; set; }
        public string NomeMae { get; set; }
        public string Cabelo { get; set; }
        public string CorPele { get; set; }
        public string Profissao { get; set; }
        public string EnderecoResidencial { get; set; }
        public string EnderecoTrabalho { get; set; }
        public Outros outros { get; set; }
    }

    public class Outros
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public string DataNascimento { get; set; }
        public string Naturalidade { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
    }
}
