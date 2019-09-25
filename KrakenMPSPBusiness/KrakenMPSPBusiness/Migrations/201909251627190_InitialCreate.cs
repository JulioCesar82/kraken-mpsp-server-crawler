namespace KrakenMPSPBusiness.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LegalPersonModel",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NomeFantasia = c.String(),
                        CNPJ = c.String(),
                        CPFDoFundador = c.String(),
                        Contador = c.String(),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhysicalPersonModel",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NomeCompleto = c.String(),
                        CPF = c.String(),
                        RG = c.String(),
                        DataDeNascimento = c.String(),
                        NomeDaMae = c.String(),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResourcesFoundModel",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ArquivoReferencia = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Arisp_Id = c.Long(),
                        Arpensp_Id = c.Long(),
                        CagedPF_Id = c.Long(),
                        CagedPJ_Id = c.Long(),
                        Censesc_Id = c.Long(),
                        Siel_Id = c.Long(),
                        Sivec_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArispCrawlerModel", t => t.Arisp_Id)
                .ForeignKey("dbo.ArpenspCrawlerModel", t => t.Arpensp_Id)
                .ForeignKey("dbo.CagedCrawlerModelPF", t => t.CagedPF_Id)
                .ForeignKey("dbo.CagedCrawlerModelPJ", t => t.CagedPJ_Id)
                .ForeignKey("dbo.CensecCrawlerModel", t => t.Censesc_Id)
                .ForeignKey("dbo.SielCrawlerModel", t => t.Siel_Id)
                .ForeignKey("dbo.SivecCrawlerModel", t => t.Sivec_Id)
                .Index(t => t.Arisp_Id)
                .Index(t => t.Arpensp_Id)
                .Index(t => t.CagedPF_Id)
                .Index(t => t.CagedPJ_Id)
                .Index(t => t.Censesc_Id)
                .Index(t => t.Siel_Id)
                .Index(t => t.Sivec_Id);
            
            CreateTable(
                "dbo.ArispCrawlerModel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Processo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Cidade = c.String(),
                        Cartorio = c.String(),
                        Matricula = c.String(),
                        Arquivo = c.String(),
                        ArispCrawlerModel_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArispCrawlerModel", t => t.ArispCrawlerModel_Id)
                .Index(t => t.ArispCrawlerModel_Id);
            
            CreateTable(
                "dbo.ArpenspCrawlerModel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CartorioRegistro = c.String(),
                        NumeroCNS = c.String(),
                        UF = c.String(),
                        NomeConjugeA1 = c.String(),
                        NovoNomeConjugeA2 = c.String(),
                        NomeConjugeB1 = c.String(),
                        NovoNomeConjugeB2 = c.String(),
                        DataCasamento = c.String(),
                        Matricula = c.String(),
                        DataEntrada = c.String(),
                        DataRegistro = c.String(),
                        Acervo = c.String(),
                        NumeroLivro = c.String(),
                        NumeroFolha = c.String(),
                        NumeroRegistro = c.String(),
                        TipoLivro = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CagedCrawlerModelPF",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Cpf = c.String(),
                        NomeTrabalhador = c.String(),
                        PisBaseTrabalhador = c.String(),
                        CtpsTrabalhador = c.String(),
                        FaixaPisTrabalhador = c.String(),
                        NacionalidadeTrabalhador = c.String(),
                        GrauInstrucaoTrabalhador = c.String(),
                        DeficienteTrabalhador = c.String(),
                        DataNascimentoTrabalhador = c.String(),
                        SexoTrabalhador = c.String(),
                        CorTrabalhador = c.String(),
                        CepTrabalhador = c.String(),
                        TempoTrabalhoCaged = c.String(),
                        TempoTrabalhoRais = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CagedCrawlerModelPJ",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Cnpj = c.String(),
                        RazaoSocial = c.String(),
                        Logradouro = c.String(),
                        Bairro = c.String(),
                        Municipio = c.String(),
                        Estado = c.String(),
                        Cep = c.String(),
                        NomeContato = c.String(),
                        CpfContato = c.String(),
                        TelefoneContato = c.String(),
                        RamalContato = c.String(),
                        EmailContato = c.String(),
                        Cnae = c.String(),
                        AtividadeEconomica = c.String(),
                        NoFilias = c.Int(nullable: false),
                        TotalVinculos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CensecCrawlerModel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Livro = c.String(),
                        Carga = c.String(),
                        Data = c.String(),
                        Ato = c.String(),
                        DataAto = c.String(),
                        Folha = c.String(),
                        Nomes = c.String(),
                        CpfsCnpjs = c.String(),
                        Qualidads = c.String(),
                        Uf = c.String(),
                        Municipio = c.String(),
                        Cartorio = c.String(),
                        Telefones = c.String(),
                        TipoTel = c.String(),
                        Ramal = c.String(),
                        Contato = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SielCrawlerModel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Titulo = c.String(),
                        DataNascimento = c.String(),
                        Zona = c.String(),
                        Endereco = c.String(),
                        Municipio = c.String(),
                        UF = c.String(),
                        DataDomicilio = c.String(),
                        NomePai = c.String(),
                        NomeMae = c.String(),
                        Naturalidade = c.String(),
                        CodValidacao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SivecCrawlerModel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        Sexo = c.String(),
                        DataNascimento = c.String(),
                        RG = c.String(),
                        NumControle = c.String(),
                        TipoRG = c.String(),
                        DataEmissaoRG = c.String(),
                        Alcunha = c.String(),
                        EstadoCivil = c.String(),
                        Naturalidade = c.String(),
                        Naturalizado = c.String(),
                        PostoIdentificacao = c.String(),
                        GrauInstrucao = c.String(),
                        FormulaFundamental = c.String(),
                        NomePai = c.String(),
                        CorOlhos = c.String(),
                        NomeMae = c.String(),
                        Cabelo = c.String(),
                        CorPele = c.String(),
                        Profissao = c.String(),
                        EnderecoResidencial = c.String(),
                        EnderecoTrabalho = c.String(),
                        outros_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Outros", t => t.outros_Id)
                .Index(t => t.outros_Id);
            
            CreateTable(
                "dbo.Outros",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        RG = c.String(),
                        DataNascimento = c.String(),
                        Naturalidade = c.String(),
                        NomePai = c.String(),
                        NomeMae = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResourcesFoundModel", "Sivec_Id", "dbo.SivecCrawlerModel");
            DropForeignKey("dbo.SivecCrawlerModel", "outros_Id", "dbo.Outros");
            DropForeignKey("dbo.ResourcesFoundModel", "Siel_Id", "dbo.SielCrawlerModel");
            DropForeignKey("dbo.ResourcesFoundModel", "Censesc_Id", "dbo.CensecCrawlerModel");
            DropForeignKey("dbo.ResourcesFoundModel", "CagedPJ_Id", "dbo.CagedCrawlerModelPJ");
            DropForeignKey("dbo.ResourcesFoundModel", "CagedPF_Id", "dbo.CagedCrawlerModelPF");
            DropForeignKey("dbo.ResourcesFoundModel", "Arpensp_Id", "dbo.ArpenspCrawlerModel");
            DropForeignKey("dbo.ResourcesFoundModel", "Arisp_Id", "dbo.ArispCrawlerModel");
            DropForeignKey("dbo.Processo", "ArispCrawlerModel_Id", "dbo.ArispCrawlerModel");
            DropIndex("dbo.SivecCrawlerModel", new[] { "outros_Id" });
            DropIndex("dbo.Processo", new[] { "ArispCrawlerModel_Id" });
            DropIndex("dbo.ResourcesFoundModel", new[] { "Sivec_Id" });
            DropIndex("dbo.ResourcesFoundModel", new[] { "Siel_Id" });
            DropIndex("dbo.ResourcesFoundModel", new[] { "Censesc_Id" });
            DropIndex("dbo.ResourcesFoundModel", new[] { "CagedPJ_Id" });
            DropIndex("dbo.ResourcesFoundModel", new[] { "CagedPF_Id" });
            DropIndex("dbo.ResourcesFoundModel", new[] { "Arpensp_Id" });
            DropIndex("dbo.ResourcesFoundModel", new[] { "Arisp_Id" });
            DropTable("dbo.Outros");
            DropTable("dbo.SivecCrawlerModel");
            DropTable("dbo.SielCrawlerModel");
            DropTable("dbo.CensecCrawlerModel");
            DropTable("dbo.CagedCrawlerModelPJ");
            DropTable("dbo.CagedCrawlerModelPF");
            DropTable("dbo.ArpenspCrawlerModel");
            DropTable("dbo.Processo");
            DropTable("dbo.ArispCrawlerModel");
            DropTable("dbo.ResourcesFoundModel");
            DropTable("dbo.PhysicalPersonModel");
            DropTable("dbo.LegalPersonModel");
        }
    }
}
