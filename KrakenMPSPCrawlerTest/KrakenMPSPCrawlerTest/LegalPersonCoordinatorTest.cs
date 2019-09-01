using NUnit.Framework;

using KrakenMPSPCrawler;
using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawlerTest
{
    public class LegalPersonCoordinatorTest
    {
        [Test]
        public void TestMethod1()
        {
            // Arrange
            LegalPersonModel legalPerson = new LegalPersonModel("JCGETSOFTWARE", "1233333", "11298978699", "12321321");
            LegalPersonCoordinator coordinator = new LegalPersonCoordinator(legalPerson);

            // Act
            Investigation investigation = coordinator.Run();

            // Assert
            Assert.AreEqual(investigation.Completed, true);
        }
    }
}
