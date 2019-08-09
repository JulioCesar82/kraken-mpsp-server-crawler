using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;
using KrakenMPSPCrawler.Utils;

namespace KrakenMPSPCrawler.Crawlers
{
    public class ArispCrawler : Crawler
    {
        public override CrawlerStatus Execute()
        {
            using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
            {
                driver.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
                driver.Manage().Window.Maximize();
            }

            return CrawlerStatus.Success;
        }
    }
}
