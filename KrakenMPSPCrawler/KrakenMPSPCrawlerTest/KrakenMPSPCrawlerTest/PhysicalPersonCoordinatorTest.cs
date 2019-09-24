using NUnit.Framework;

using KrakenMPSPCrawler;
using KrakenMPSPBusiness.Models;

namespace KrakenMPSPCrawlerTest
{
    public class PhysicalPersonCoordinatorTest
    {
        [Test]
        public void TestMethod1()
        {
            // Arrange
            var examplePhysicalPerson = new PhysicalPersonModel()
            {
                NomeCompleto = "JULIO AVILA",
                CPF = "11122233344",
                RG = "22222222222",
                DataDeNascimento = "23/01/1997",
                NomeDaMae = "SELMA AVILA"
            };
            var coordinator = new PhysicalPersonCoordinator(examplePhysicalPerson);

            // Act
            var investigation = coordinator.Run();

            // Assert
            Assert.AreEqual(investigation.Completed, true);
        }
    }
}
