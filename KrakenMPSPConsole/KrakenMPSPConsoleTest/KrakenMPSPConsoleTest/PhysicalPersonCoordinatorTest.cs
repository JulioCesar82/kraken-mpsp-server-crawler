using Microsoft.VisualStudio.TestTools.UnitTesting;

using KrakenMPSPBusiness.Models;

using KrakenMPSPConsole;

namespace KrakenMPSPConsoleTest
{
    [TestClass]
    public class PhysicalPersonCoordinatorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var examplePhysicalPerson = new PhysicalPersonModel()
            {
                CPF = "27133090007",
                RG = "305922622"
            };

            // Act
            var coordinator = new PhysicalPersonCoordinator(examplePhysicalPerson).StartSearch();

            // Assert
            Assert.AreEqual(coordinator.ResultadoFinal.Completed, true);
        }
    }
}
