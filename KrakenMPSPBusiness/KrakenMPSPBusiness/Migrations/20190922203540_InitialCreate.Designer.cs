﻿// <auto-generated />
using KrakenMPSPBusiness.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrakenMPSPBusiness.Migrations
{
    [DbContext(typeof(SqlLiteContext))]
    [Migration("20190922203540_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("KrakenMPSPBusiness.Models.ArispCrawlerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("ArispCrawlerModel");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.ArpenspCrawlerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Acervo");

                    b.Property<string>("CartorioRegistro");

                    b.Property<string>("DataCasamento");

                    b.Property<string>("DataEntrada");

                    b.Property<string>("DataRegistro");

                    b.Property<string>("Matricula");

                    b.Property<string>("NomeConjugeA1");

                    b.Property<string>("NomeConjugeB1");

                    b.Property<string>("NovoNomeConjugeA2");

                    b.Property<string>("NovoNomeConjugeB2");

                    b.Property<string>("NumeroCNS");

                    b.Property<string>("NumeroFolha");

                    b.Property<string>("NumeroLivro");

                    b.Property<string>("NumeroRegistro");

                    b.Property<string>("TipoLivro");

                    b.Property<string>("UF");

                    b.HasKey("Id");

                    b.ToTable("ArpenspCrawlerModel");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.CagedCrawlerModelPF", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CepTrabalhador");

                    b.Property<string>("CorTrabalhador");

                    b.Property<string>("Cpf");

                    b.Property<string>("CtpsTrabalhador");

                    b.Property<string>("DataNascimentoTrabalhador");

                    b.Property<string>("DeficienteTrabalhador");

                    b.Property<string>("FaixaPisTrabalhador");

                    b.Property<string>("GrauInstrucaoTrabalhador");

                    b.Property<string>("NacionalidadeTrabalhador");

                    b.Property<string>("NomeTrabalhador");

                    b.Property<string>("PisBaseTrabalhador");

                    b.Property<string>("SexoTrabalhador");

                    b.Property<string>("TempoTrabalhoCaged");

                    b.Property<string>("TempoTrabalhoRais");

                    b.HasKey("Id");

                    b.ToTable("CagedCrawlerModelPF");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.CagedCrawlerModelPJ", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AtividadeEconomica");

                    b.Property<string>("Bairro");

                    b.Property<string>("Cep");

                    b.Property<string>("Cnae");

                    b.Property<string>("Cnpj");

                    b.Property<string>("CpfContato");

                    b.Property<string>("EmailContato");

                    b.Property<string>("Estado");

                    b.Property<string>("Logradouro");

                    b.Property<string>("Municipio");

                    b.Property<int>("NoFilias");

                    b.Property<string>("NomeContato");

                    b.Property<string>("RamalContato");

                    b.Property<string>("RazaoSocial");

                    b.Property<string>("TelefoneContato");

                    b.Property<int>("TotalVinculos");

                    b.HasKey("Id");

                    b.ToTable("CagedCrawlerModelPJ");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.LegalPersonModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CNPJ");

                    b.Property<string>("CPFDoFundador");

                    b.Property<bool>("Completed");

                    b.Property<string>("Contador");

                    b.Property<string>("NomeFantasia");

                    b.HasKey("Id");

                    b.ToTable("LegalPerson");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.Outros", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DataNascimento");

                    b.Property<string>("Naturalidade");

                    b.Property<string>("Nome");

                    b.Property<string>("NomeMae");

                    b.Property<string>("NomePai");

                    b.Property<string>("RG");

                    b.HasKey("Id");

                    b.ToTable("Outros");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.PhysicalPersonModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CPF");

                    b.Property<bool>("Completed");

                    b.Property<string>("DataDeNascimento");

                    b.Property<string>("NomeCompleto");

                    b.Property<string>("NomeDaMae");

                    b.Property<string>("RG");

                    b.HasKey("Id");

                    b.ToTable("PhysicalPerson");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.Processo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ArispCrawlerModelId");

                    b.Property<string>("Arquivo");

                    b.Property<string>("Cartorio");

                    b.Property<string>("Cidade");

                    b.Property<string>("Matricula");

                    b.HasKey("Id");

                    b.HasIndex("ArispCrawlerModelId");

                    b.ToTable("Processo");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.ResourcesFound", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ArispId");

                    b.Property<long?>("ArpenspId");

                    b.Property<long>("ArquivoReferencia");

                    b.Property<long?>("CagedPFId");

                    b.Property<long?>("CagedPJId");

                    b.Property<long?>("SielId");

                    b.Property<long?>("SivecId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ArispId");

                    b.HasIndex("ArpenspId");

                    b.HasIndex("CagedPFId");

                    b.HasIndex("CagedPJId");

                    b.HasIndex("SielId");

                    b.HasIndex("SivecId");

                    b.ToTable("ResourcesFound");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.SielCrawlerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodValidacao");

                    b.Property<string>("DataDomicilio");

                    b.Property<string>("DataNascimento");

                    b.Property<string>("Endereco");

                    b.Property<string>("Municipio");

                    b.Property<string>("Naturalidade");

                    b.Property<string>("Nome");

                    b.Property<string>("NomeMae");

                    b.Property<string>("NomePai");

                    b.Property<string>("Titulo");

                    b.Property<string>("UF");

                    b.Property<string>("Zona");

                    b.HasKey("Id");

                    b.ToTable("SielCrawlerModel");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.SivecCrawlerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alcunha");

                    b.Property<string>("Cabelo");

                    b.Property<string>("CorOlhos");

                    b.Property<string>("CorPele");

                    b.Property<string>("DataEmissaoRG");

                    b.Property<string>("DataNascimento");

                    b.Property<string>("EnderecoResidencial");

                    b.Property<string>("EnderecoTrabalho");

                    b.Property<string>("EstadoCivil");

                    b.Property<string>("FormulaFundamental");

                    b.Property<string>("GrauInstrucao");

                    b.Property<string>("Naturalidade");

                    b.Property<string>("Naturalizado");

                    b.Property<string>("Nome");

                    b.Property<string>("NomeMae");

                    b.Property<string>("NomePai");

                    b.Property<string>("NumControle");

                    b.Property<string>("PostoIdentificacao");

                    b.Property<string>("Profissao");

                    b.Property<string>("RG");

                    b.Property<string>("Sexo");

                    b.Property<string>("TipoRG");

                    b.Property<long?>("outrosId");

                    b.HasKey("Id");

                    b.HasIndex("outrosId");

                    b.ToTable("SivecCrawlerModel");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.Processo", b =>
                {
                    b.HasOne("KrakenMPSPBusiness.Models.ArispCrawlerModel")
                        .WithMany("Processos")
                        .HasForeignKey("ArispCrawlerModelId");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.ResourcesFound", b =>
                {
                    b.HasOne("KrakenMPSPBusiness.Models.ArispCrawlerModel", "Arisp")
                        .WithMany()
                        .HasForeignKey("ArispId");

                    b.HasOne("KrakenMPSPBusiness.Models.ArpenspCrawlerModel", "Arpensp")
                        .WithMany()
                        .HasForeignKey("ArpenspId");

                    b.HasOne("KrakenMPSPBusiness.Models.CagedCrawlerModelPF", "CagedPF")
                        .WithMany()
                        .HasForeignKey("CagedPFId");

                    b.HasOne("KrakenMPSPBusiness.Models.CagedCrawlerModelPJ", "CagedPJ")
                        .WithMany()
                        .HasForeignKey("CagedPJId");

                    b.HasOne("KrakenMPSPBusiness.Models.SielCrawlerModel", "Siel")
                        .WithMany()
                        .HasForeignKey("SielId");

                    b.HasOne("KrakenMPSPBusiness.Models.SivecCrawlerModel", "Sivec")
                        .WithMany()
                        .HasForeignKey("SivecId");
                });

            modelBuilder.Entity("KrakenMPSPBusiness.Models.SivecCrawlerModel", b =>
                {
                    b.HasOne("KrakenMPSPBusiness.Models.Outros", "outros")
                        .WithMany()
                        .HasForeignKey("outrosId");
                });
        }
    }
}