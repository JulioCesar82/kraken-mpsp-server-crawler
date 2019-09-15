using System;

using MongoDB.Driver;

using KrakenMPSPCrawler;
using KrakenMPSPCrawler.Models;
using KrakenMPSPConsole.Context;


namespace KrakenMPSPConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
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
                    Console.WriteLine("saving legal person");
                    db.LegalPerson.InsertOne(exampleLegalPerson);

                    
                     var empresas = db.LegalPerson.Find(x => true).ToList();

                    foreach (var empresa in empresas)
                    {
                        Console.WriteLine($"Empresa: {empresa.NomeFantasia}");
                        var crawler = new LegalPersonCoordinator(empresa);
                        var result = crawler.Run();
                        Console.WriteLine("Completou a busca? {0}", result.Completed);
                        Console.WriteLine("Informacoes encontradas: {0}", result.Informations);

                        //Console.WriteLine("Salvando informações obtidas de LegalPerson...");
                        //db.LegalPerson.ReplaceOne(p => p.Id == exampleLegalPerson.Id, exampleLegalPerson);
                    }

                    Console.WriteLine("saving physical person");
                    db.PhysicalPerson.InsertOne(examplePhysicalPerson);
                    
                    var pessoas = db.PhysicalPerson.Find(x => true).ToList();
                    foreach (var pessoa in pessoas)
                    {
                        Console.WriteLine($"Pessoa: {pessoa.NomeCompleto}");
                        var crawler = new PhysicalPersonCoordinator(pessoa);
                        var result = crawler.Run();
                        Console.WriteLine("Completou a busca? {0}", result.Completed);
                        Console.WriteLine("Informacoes encontradas: {0}", result.Informations);
                        
                        //Console.WriteLine("Salvando informações obtidas de LegalPerson...");
                        //db.LegalPerson.ReplaceOne(p => p.Id == exampleLegalPerson.Id, exampleLegalPerson);
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
