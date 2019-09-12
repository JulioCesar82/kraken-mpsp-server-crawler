using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace KrakenMPSPConsole.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("KrakenMPSPCrawler.Models.LegalPersonModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CNPJ");

                    b.Property<string>("CPFDoFundador");

                    b.Property<string>("Contador");

                    b.Property<string>("NomeFantasia");

                    b.HasKey("Id");

                    b.ToTable("LegalPerson");
                });

            modelBuilder.Entity("KrakenMPSPCrawler.Models.PhysicalPersonModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CPF");

                    b.Property<string>("DataDeNascimento");

                    b.Property<string>("NomeCompleto");

                    b.Property<string>("NomeDaMae");

                    b.Property<string>("RG");

                    b.HasKey("Id");

                    b.ToTable("PhysicalPerson");
                });
        }
    }
}
