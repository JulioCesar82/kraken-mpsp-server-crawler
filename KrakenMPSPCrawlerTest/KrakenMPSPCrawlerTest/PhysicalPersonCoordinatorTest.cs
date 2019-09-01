using KrakenMPSPCrawler;
using KrakenMPSPCrawler.Business.Model;
using KrakenMPSPCrawler.Models;
using NUnit.Framework;

namespace KrakenMPSPCrawlerTest
{
    public class PhysicalPersonCoordinatorTest
    {
        [Test]
        public void TestMethod1()
        {
            // Arrange
            PhysicalPersonModel phycalPerson = new PhysicalPersonModel("julio cesar", "11298978699", "10/10/2010", "selma irene");
            PhysicalPersonCoordinator coordinator = new PhysicalPersonCoordinator(phycalPerson);

            // Act
            Investigation investigation = coordinator.Run();

            // Assert
            Assert.Equals(investigation.Completed, true);
        }
    }
}
