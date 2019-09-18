using System;
using System.Linq;

using MongoDB.Driver;

using KrakenMPSPCrawler;
using KrakenMPSPCrawler.Models;
using KrakenMPSPConsole.Models;
using KrakenMPSPConsole.Context;

namespace KrakenMPSPConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            MockData();
            Search();
        }

        static void Search()
        {
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

                        // gerando arquivo com os resultados
                        var resourcesFound = new ResourcesFound
                        {
                            ArquivoReferencia = empresa.Id,
                            Type = empresa.Type
                        };
                        foreach (var information in result.Informations)
                        {
                            CopyValues<ResourcesFound>(resourcesFound, information);
                        }

                        Console.WriteLine("Salvando informações obtidas...");
                        db.ResourcesFound.InsertOne(resourcesFound);

                        empresa.Completed = result.Completed;
                        db.LegalPerson.ReplaceOne(p => p.Id == empresa.Id, empresa);
                    }

                    var pessoas = db.PhysicalPerson.Find(x => !x.Completed).ToList();
                    foreach (var pessoa in pessoas)
                    {
                        Console.WriteLine($"Pessoa: {pessoa.NomeCompleto}");
                        var crawler = new PhysicalPersonCoordinator(pessoa);
                        var result = crawler.Run();
                        Console.WriteLine("Completou a busca? {0}", result.Completed);

                        // gerando arquivo com os resultados
                        var resourcesFound = new ResourcesFound
                        {
                            ArquivoReferencia = pessoa.Id,
                            Type = pessoa.Type
                        };
                        foreach (var information in result.Informations)
                        {
                            CopyValues<ResourcesFound>(resourcesFound, information);
                        }

                        Console.WriteLine("Salvando informações obtidas...");
                        db.ResourcesFound.InsertOne(resourcesFound);

                        pessoa.Completed = result.Completed;
                        db.PhysicalPerson.ReplaceOne(p => p.Id == pessoa.Id, pessoa);
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

        static void CopyValues<T>(T target, object source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var targetType = prop.PropertyType.ToString();
                var sourceType = source.GetType().ToString();
                if (targetType == sourceType)
                {
                    prop.SetValue(target, source);
                }
            }
        }

        static void MockData()
        {
            var exampleLegalPerson = new LegalPersonModel
            {
                NomeFantasia = "PETROBRASIL",
                CNPJ = "1111111111",
                CPFDoFundador = "2222222222",
                Contador = "333333333"
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
