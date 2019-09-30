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
                CPF = "11122233344"
            };

            // Act
            var coordinator = new PhysicalPersonCoordinator(examplePhysicalPerson);

            // Assert
            Assert.AreEqual(coordinator.Completed, true);
        }
    }
}
