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
                CNPJ = "11222333000044"
            };

            // Act
            var coordinator = new LegalPersonCoordinator(exampleLegalPerson);

            // Assert
            Assert.AreEqual(coordinator.Completed, true);
        }
    }
}
