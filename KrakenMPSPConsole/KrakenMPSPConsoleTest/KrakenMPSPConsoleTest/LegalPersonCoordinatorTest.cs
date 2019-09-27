using Microsoft.VisualStudio.TestTools.UnitTesting;

using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole;

namespace KrakenMPSPConsoleTest
{
    [TestClass]
    public class LegalPersonCoordinatorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var exampleLegalPerson = new LegalPersonModel
            {
                NomeFantasia = "PETROBRASIL",
                CNPJ = "11222333000044",
                CPFDoFundador = "22222222222",
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
