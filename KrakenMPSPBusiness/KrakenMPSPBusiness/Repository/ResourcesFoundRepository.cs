using KrakenMPSPBusiness.Enum;

namespace KrakenMPSPBusiness.Repository
{
    public class ResourcesFoundRepository
    {
        public ResourcesFoundRepository(DatabaseContext context)
        {

        }

        public void InserindoDados()
        {
            /*
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
            try
            {
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
            */
        }

        public void PercorrendoLista()
        {
            /*
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
            */
        }

        /*
        IList<TType> GetAll(int take, int page);

        void Create(TType entity);

        void Save(TType entity);
        void Update(TType entity);
        void SaveOrUpdate(TType entity);

        void Delete(TType entity);

        void Merge(TType entity);

        TType Get(Expression<Func<TType, bool>> whereExpression);

        bool Exists(Expression<Func<TType, bool>> whereExpression);
        */
    }
}
