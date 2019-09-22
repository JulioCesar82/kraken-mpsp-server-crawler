using Microsoft.EntityFrameworkCore.Migrations;

namespace KrakenMPSPBusiness.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArispCrawlerModel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArispCrawlerModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArpenspCrawlerModel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CartorioRegistro = table.Column<string>(nullable: true),
                    NumeroCNS = table.Column<string>(nullable: true),
                    UF = table.Column<string>(nullable: true),
                    NomeConjugeA1 = table.Column<string>(nullable: true),
                    NovoNomeConjugeA2 = table.Column<string>(nullable: true),
                    NomeConjugeB1 = table.Column<string>(nullable: true),
                    NovoNomeConjugeB2 = table.Column<string>(nullable: true),
                    DataCasamento = table.Column<string>(nullable: true),
                    Matricula = table.Column<string>(nullable: true),
                    DataEntrada = table.Column<string>(nullable: true),
                    DataRegistro = table.Column<string>(nullable: true),
                    Acervo = table.Column<string>(nullable: true),
                    NumeroLivro = table.Column<string>(nullable: true),
                    NumeroFolha = table.Column<string>(nullable: true),
                    NumeroRegistro = table.Column<string>(nullable: true),
                    TipoLivro = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArpenspCrawlerModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CagedCrawlerModelPF",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cpf = table.Column<string>(nullable: true),
                    NomeTrabalhador = table.Column<string>(nullable: true),
                    PisBaseTrabalhador = table.Column<string>(nullable: true),
                    CtpsTrabalhador = table.Column<string>(nullable: true),
                    FaixaPisTrabalhador = table.Column<string>(nullable: true),
                    NacionalidadeTrabalhador = table.Column<string>(nullable: true),
                    GrauInstrucaoTrabalhador = table.Column<string>(nullable: true),
                    DeficienteTrabalhador = table.Column<string>(nullable: true),
                    DataNascimentoTrabalhador = table.Column<string>(nullable: true),
                    SexoTrabalhador = table.Column<string>(nullable: true),
                    CorTrabalhador = table.Column<string>(nullable: true),
                    CepTrabalhador = table.Column<string>(nullable: true),
                    TempoTrabalhoCaged = table.Column<string>(nullable: true),
                    TempoTrabalhoRais = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CagedCrawlerModelPF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CagedCrawlerModelPJ",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cnpj = table.Column<string>(nullable: true),
                    RazaoSocial = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Cep = table.Column<string>(nullable: true),
                    NomeContato = table.Column<string>(nullable: true),
                    CpfContato = table.Column<string>(nullable: true),
                    TelefoneContato = table.Column<string>(nullable: true),
                    RamalContato = table.Column<string>(nullable: true),
                    EmailContato = table.Column<string>(nullable: true),
                    Cnae = table.Column<string>(nullable: true),
                    AtividadeEconomica = table.Column<string>(nullable: true),
                    NoFilias = table.Column<int>(nullable: false),
                    TotalVinculos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CagedCrawlerModelPJ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalPerson",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    NomeFantasia = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    CPFDoFundador = table.Column<string>(nullable: true),
                    Contador = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalPerson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Outros",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<string>(nullable: true),
                    Naturalidade = table.Column<string>(nullable: true),
                    NomePai = table.Column<string>(nullable: true),
                    NomeMae = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalPerson",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    NomeCompleto = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    DataDeNascimento = table.Column<string>(nullable: true),
                    NomeDaMae = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalPerson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SielCrawlerModel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<string>(nullable: true),
                    Zona = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    UF = table.Column<string>(nullable: true),
                    DataDomicilio = table.Column<string>(nullable: true),
                    NomePai = table.Column<string>(nullable: true),
                    NomeMae = table.Column<string>(nullable: true),
                    Naturalidade = table.Column<string>(nullable: true),
                    CodValidacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SielCrawlerModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cidade = table.Column<string>(nullable: true),
                    Cartorio = table.Column<string>(nullable: true),
                    Matricula = table.Column<string>(nullable: true),
                    Arquivo = table.Column<string>(nullable: true),
                    ArispCrawlerModelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processo_ArispCrawlerModel_ArispCrawlerModelId",
                        column: x => x.ArispCrawlerModelId,
                        principalTable: "ArispCrawlerModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SivecCrawlerModel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    NumControle = table.Column<string>(nullable: true),
                    TipoRG = table.Column<string>(nullable: true),
                    DataEmissaoRG = table.Column<string>(nullable: true),
                    Alcunha = table.Column<string>(nullable: true),
                    EstadoCivil = table.Column<string>(nullable: true),
                    Naturalidade = table.Column<string>(nullable: true),
                    Naturalizado = table.Column<string>(nullable: true),
                    PostoIdentificacao = table.Column<string>(nullable: true),
                    GrauInstrucao = table.Column<string>(nullable: true),
                    FormulaFundamental = table.Column<string>(nullable: true),
                    NomePai = table.Column<string>(nullable: true),
                    CorOlhos = table.Column<string>(nullable: true),
                    NomeMae = table.Column<string>(nullable: true),
                    Cabelo = table.Column<string>(nullable: true),
                    CorPele = table.Column<string>(nullable: true),
                    Profissao = table.Column<string>(nullable: true),
                    EnderecoResidencial = table.Column<string>(nullable: true),
                    EnderecoTrabalho = table.Column<string>(nullable: true),
                    outrosId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SivecCrawlerModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SivecCrawlerModel_Outros_outrosId",
                        column: x => x.outrosId,
                        principalTable: "Outros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourcesFound",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    ArquivoReferencia = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ArispId = table.Column<long>(nullable: true),
                    ArpenspId = table.Column<long>(nullable: true),
                    SielId = table.Column<long>(nullable: true),
                    SivecId = table.Column<long>(nullable: true),
                    CagedPJId = table.Column<long>(nullable: true),
                    CagedPFId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcesFound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourcesFound_ArispCrawlerModel_ArispId",
                        column: x => x.ArispId,
                        principalTable: "ArispCrawlerModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourcesFound_ArpenspCrawlerModel_ArpenspId",
                        column: x => x.ArpenspId,
                        principalTable: "ArpenspCrawlerModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourcesFound_CagedCrawlerModelPF_CagedPFId",
                        column: x => x.CagedPFId,
                        principalTable: "CagedCrawlerModelPF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourcesFound_CagedCrawlerModelPJ_CagedPJId",
                        column: x => x.CagedPJId,
                        principalTable: "CagedCrawlerModelPJ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourcesFound_SielCrawlerModel_SielId",
                        column: x => x.SielId,
                        principalTable: "SielCrawlerModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourcesFound_SivecCrawlerModel_SivecId",
                        column: x => x.SivecId,
                        principalTable: "SivecCrawlerModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processo_ArispCrawlerModelId",
                table: "Processo",
                column: "ArispCrawlerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesFound_ArispId",
                table: "ResourcesFound",
                column: "ArispId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesFound_ArpenspId",
                table: "ResourcesFound",
                column: "ArpenspId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesFound_CagedPFId",
                table: "ResourcesFound",
                column: "CagedPFId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesFound_CagedPJId",
                table: "ResourcesFound",
                column: "CagedPJId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesFound_SielId",
                table: "ResourcesFound",
                column: "SielId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesFound_SivecId",
                table: "ResourcesFound",
                column: "SivecId");

            migrationBuilder.CreateIndex(
                name: "IX_SivecCrawlerModel_outrosId",
                table: "SivecCrawlerModel",
                column: "outrosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegalPerson");

            migrationBuilder.DropTable(
                name: "PhysicalPerson");

            migrationBuilder.DropTable(
                name: "Processo");

            migrationBuilder.DropTable(
                name: "ResourcesFound");

            migrationBuilder.DropTable(
                name: "ArispCrawlerModel");

            migrationBuilder.DropTable(
                name: "ArpenspCrawlerModel");

            migrationBuilder.DropTable(
                name: "CagedCrawlerModelPF");

            migrationBuilder.DropTable(
                name: "CagedCrawlerModelPJ");

            migrationBuilder.DropTable(
                name: "SielCrawlerModel");

            migrationBuilder.DropTable(
                name: "SivecCrawlerModel");

            migrationBuilder.DropTable(
                name: "Outros");
        }
    }
}
