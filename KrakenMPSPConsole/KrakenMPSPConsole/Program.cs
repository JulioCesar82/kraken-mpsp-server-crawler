using System;

using Microsoft.EntityFrameworkCore;

using KrakenMPSPCrawler;
using KrakenMPSPCrawler.Models;

namespace KrakenMPSPConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando a busca");

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

            try
            {
                using (var db = new DataBaseContext(true))
                {
                    // Run Migrations
                    db.Database.Migrate();

                    db.LegalPerson.Add(exampleLegalPerson);
                    db.SaveChanges();

                    var empresas = db.LegalPerson;
                    foreach (var empresa in empresas)
                    {
                        Console.WriteLine($"Empresa: {empresa.NomeFantasia}");
                        var crawler = new LegalPersonCoordinator(empresa);
                        var result = crawler.Run();
                        Console.WriteLine("Resultado foi ", result.Completed);

                    }

                    db.PhysicalPerson.Add(examplePhysicalPerson);
                    db.SaveChanges();

                    var pessoas = db.PhysicalPerson;
                    foreach (var pessoa in pessoas)
                    {
                        Console.WriteLine($"Pessoa: {pessoa.NomeCompleto}");
                        var crawler = new PhysicalPersonCoordinator(pessoa);
                        var result = crawler.Run();
                        Console.WriteLine("Resultado foi ", result.Completed);

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Search execution error: {0}", e.Message);
            }
        }
    }
}
