using System;

using AutoMapper;
using MongoDB.Driver;

using KrakenMPSPCrawler;
using KrakenMPSPCrawler.Models;
using KrakenMPSPConsole.Models;
using KrakenMPSPConsole.Mapper;
using KrakenMPSPConsole.Context;

namespace KrakenMPSPConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            MockData();
            Search();

            Console.WriteLine("finished application");
            Console.ReadKey();
        }

        static void Search()
        {
            // configurando o AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingResourcesFound());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            try
            {
                using (var db = new MongoDbContext())
                {
                    Console.WriteLine("Iniciando a busca");
                    var empresas = db.LegalPerson.Find(x => !x.Completed).ToList();
                    foreach (var empresa in empresas)
                    {
                        Console.WriteLine($"Empresa: {empresa.NomeFantasia}");
                        var crawler = new LegalPersonCoordinator(empresa);
                        var result = crawler.Run();
                        Console.WriteLine("Completou a busca? {0}", result.Completed);

                        Console.WriteLine("Salvando informações obtidas...");
                        var resourcesFound = new ResourcesFound();
                        foreach (var (item1, item2) in result.Informations)
                        {
                            Console.WriteLine("Recebido informacoes do {0}", item1);
                            resourcesFound = mapper.Map<SielCrawlerModel, ResourcesFound>((SielCrawlerModel)item2);
                            //resourcesFound = mapper.Map<SielCrawlerModel, ResourcesFound>((SielCrawlerModel)item2);
                        }
                        db.ResourcesFound.InsertOne(resourcesFound);
                        empresa.Completed = true;
                        db.LegalPerson.ReplaceOne(p => p.Id == empresa.Id, empresa);
                    }

                    var pessoas = db.PhysicalPerson.Find(x => !x.Completed).ToList();
                    foreach (var pessoa in pessoas)
                    {
                        Console.WriteLine($"Pessoa: {pessoa.NomeCompleto}");
                        var crawler = new PhysicalPersonCoordinator(pessoa);
                        var result = crawler.Run();
                        Console.WriteLine("Completou a busca? {0}", result.Completed);

                        Console.WriteLine("Salvando informações obtidas...");
                        var resourcesFound = new ResourcesFound();
                        foreach (var (item1, item2) in result.Informations)
                        {
                            Console.WriteLine("Recebido informacoes do {0}", item1);
                            //resourcesFound = mapper.Map<SielCrawlerModel, ResourcesFound>((SielCrawlerModel)item2);
                        }
                        db.ResourcesFound.InsertOne(resourcesFound);
                        pessoa.Completed = true;
                        db.PhysicalPerson.ReplaceOne(p => p.Id == pessoa.Id, pessoa);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Search execution error: {0}", e.Message);
            }
        }

        static void MockData()
        {
            var exampleLegalPerson = new LegalPersonModel
            {
                NomeFantasia = "PETROBRASIL",
                CNPJ = "1111111111",
                CPFDoFundador = "2222222222",
                Contador = "333333333",
            };

            var examplePhysicalPerson = new PhysicalPersonModel()
            {
                NomeCompleto = "JULIO AVILA",
                CPF = "1111111111",
                RG = "22222222222",
                DataDeNascimento = "23/01/1997",
                NomeDaMae = "SELMA AVILA"
            };
            try {
                using (var db = new MongoDbContext())
                {
                    Console.WriteLine("Inserindo buscas de teste");
                    db.LegalPerson.InsertOne(exampleLegalPerson);
                    db.PhysicalPerson.InsertOne(examplePhysicalPerson);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("MockData execution error: {0}", e.Message);
            }
        }
    }
}
