using NUnit.Framework;

using KrakenMPSPCrawler;
using KrakenMPSPBusiness.Models;

namespace KrakenMPSPCrawlerTest
{
    public class LegalPersonCoordinatorTest
    {
        [Test]
        public void TestMethod1()
        {
            // Arrange
            var exampleLegalPerson = new LegalPersonModel
            {
                NomeFantasia = "PETROBRASIL",
                CNPJ = "1111111111",
                CPFDoFundador = "2222222222",
                Contador = "333333333",
            };
            var coordinator = new LegalPersonCoordinator(exampleLegalPerson);

            // Act
            var investigation = coordinator.Run();

            // Assert
            Assert.AreEqual(investigation.Completed, true);
        }
    }
}
