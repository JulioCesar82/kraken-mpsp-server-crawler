namespace KrakenMPSPBusiness.Models
{
    public class CagedCrawlerModelPJ
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string NomeContato { get; set; }
        public string CpfContato { get; set; }
        public string TelefoneContato { get; set; }
        public string RamalContato { get; set; }
        public string EmailContato { get; set; }
        public string Cnae { get; set; }
        public string AtividadeEconomica { get; set; }
        public int NoFilias { get; set; }
        public int TotalVinculos { get; set; }
    }

    public class CagedCrawlerModelPF
    {
        public string Cpf { get; set; }
        public string NomeTrabalhador { get; set; }
        public string PisBaseTrabalhador { get; set; }
        public string CtpsTrabalhador { get; set; }
        public string FaixaPisTrabalhador { get; set; }
        public string NacionalidadeTrabalhador { get; set; }
        public string GrauInstrucaoTrabalhador { get; set; }
        public string DeficienteTrabalhador { get; set; }
        public string DataNascimentoTrabalhador { get; set; }
        public string SexoTrabalhador { get; set; }
        public string CorTrabalhador { get; set; }
        public string CepTrabalhador { get; set; }
        public string TempoTrabalhoCaged { get; set; }
        public string TempoTrabalhoRais { get; set; }
    }
}
