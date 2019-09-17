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
            /*
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingResourcesFound());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            */

            Console.WriteLine("Iniciando a busca");

            #region objetos de teste
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
            #endregion

            try
            {
                using (var db = new MongoDbContext())
                {
                    db.LegalPerson.InsertOne(exampleLegalPerson);
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
                            //resourcesFound = mapper.Map<ArispCrawlerModel>(resourcesFound);
                            if (item2.GetType().ToString() == typeof(ArispCrawlerModel).ToString())
                            {
                                resourcesFound.Arisp = (ArispCrawlerModel)item2;
                            }
                            else if (item2.GetType().ToString() == typeof(ArpenspCrawlerModel).ToString())
                            {
                                resourcesFound.Arpensp = (ArpenspCrawlerModel)item2;
                            }
                            else if (item2.GetType().ToString() == typeof(SielCrawlerModel).ToString())
                            {
                                resourcesFound.Siel = (SielCrawlerModel)item2;
                            }
                            else if (item2.GetType().ToString() == typeof(SivecCrawlerModel).ToString())
                            {
                                resourcesFound.Sivec = (SivecCrawlerModel)item2;
                            }
                        }
                        db.ResourcesFound.InsertOne(resourcesFound);
                        exampleLegalPerson.Completed = true;
                        db.LegalPerson.ReplaceOne(p => p.Id == exampleLegalPerson.Id, exampleLegalPerson);
                     }

                    db.PhysicalPerson.InsertOne(examplePhysicalPerson);
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
                            //resourcesFound = mapper.Map<ArispCrawlerModel>(resourcesFound);
                            if (item2.GetType().ToString() == typeof(ArispCrawlerModel).ToString())
                            {
                                resourcesFound.Arisp = (ArispCrawlerModel)item2;
                            }
                            else if (item2.GetType().ToString() == typeof(ArpenspCrawlerModel).ToString())
                            {
                                resourcesFound.Arpensp = (ArpenspCrawlerModel)item2;
                            }
                            else if (item2.GetType().ToString() == typeof(SielCrawlerModel).ToString())
                            {
                                resourcesFound.Siel = (SielCrawlerModel)item2;
                            }
                            else if (item2.GetType().ToString() == typeof(SivecCrawlerModel).ToString())
                            {
                                resourcesFound.Sivec = (SivecCrawlerModel)item2;
                            }
                        }
                        db.ResourcesFound.InsertOne(resourcesFound);
                        exampleLegalPerson.Completed = true;
                        db.LegalPerson.ReplaceOne(p => p.Id == exampleLegalPerson.Id, exampleLegalPerson);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Search execution error: {0}", e.Message);
            }

            Console.WriteLine("finished application");
            Console.ReadKey();
        }
    }
}
