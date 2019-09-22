using System;
using System.Collections.Generic;
using System.Text;

namespace KrakenMPSPCrawler.Models
{
    class CagedCrawlerModelPJ
    {
        
        public CagedCrawlerModelPJ(string cnpj, string razaoSocial, string logradouro, string bairro, string municipio, string estado, string cep, string nomeContato, string cpfContato, string telefoneContato, string ramalContato, string emailContato, string cnae, string atividadeEconomica, int noFilias, int totalVinculos)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            Logradouro = logradouro;
            Bairro = bairro;
            Municipio = municipio;
            Estado = estado;
            Cep = cep;
            NomeContato = nomeContato;
            CpfContato = cpfContato;
            TelefoneContato = telefoneContato;
            RamalContato = ramalContato;
            EmailContato = emailContato;
            Cnae = cnae;
            AtividadeEconomica = atividadeEconomica;
            NoFilias = noFilias;
            TotalVinculos = totalVinculos;
        }

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

    class CagedCrawlerModelPF
    {

        public CagedCrawlerModelPF(string cpf, string nomeTrabalhador, string pisBaseTrabalhador, string ctpsTrabalhador, string faixaPisTrabalhador, string nacionalidadeTrabalhador, string grauInstrucaoTrabalhador, string deficienteTrabalhador, string dataNascimentoTrabalhador, string sexoTrabalhador, string corTrabalhador, string cepTrabalhador, string tempoTrabalhoCaged, string tempoTrabalhoRais)
        {
            Cpf = cpf;
            NomeTrabalhador = nomeTrabalhador;
            PisBaseTrabalhador = pisBaseTrabalhador;
            CtpsTrabalhador = ctpsTrabalhador;
            FaixaPisTrabalhador = faixaPisTrabalhador;
            NacionalidadeTrabalhador = nacionalidadeTrabalhador;
            GrauInstrucaoTrabalhador = grauInstrucaoTrabalhador;
            DeficienteTrabalhador = deficienteTrabalhador;
            DataNascimentoTrabalhador = dataNascimentoTrabalhador;
            SexoTrabalhador = sexoTrabalhador;
            CorTrabalhador = corTrabalhador;
            CepTrabalhador = cepTrabalhador;
            TempoTrabalhoCaged = tempoTrabalhoCaged;
            TempoTrabalhoRais = tempoTrabalhoRais;
        }

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
