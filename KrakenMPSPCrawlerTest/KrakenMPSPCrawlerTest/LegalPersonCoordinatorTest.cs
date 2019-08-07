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
            //var driver = new PhantomJSDriver();

            // Arrange
            LegalPersonModel legalPerson = new LegalPersonModel("JCGETSOFTWARE", "1233333", "11298978699", "12321321");
            LegalPersonCoordinator coordinator = new LegalPersonCoordinator(legalPerson);

            // Act
            Investigation investigation = coordinator.Run();

            // Assert
            Assert.Equals(investigation.Completed, true);
        }
    }
}
