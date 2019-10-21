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
                CNPJ = "87676557000125"
            };

            // Act
            var coordinator = new LegalPersonCoordinator(exampleLegalPerson).StartSearch();

            // Assert
            Assert.AreEqual(coordinator.ResultadoFinal.Completed, true);
        }
    }
}
