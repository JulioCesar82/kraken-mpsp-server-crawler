using System;

using KrakenMPSPCrawler.Utils;
using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class SivecCrawler : Crawler
    {

        public SivecCrawler()
        {

        }

        public override CrawlerStatus Execute()
        {
            try
            {
                using (var driver = WebDriverFactory.CreateWebDriver(WebBrowser.Firefox))
                {
                    
                    return CrawlerStatus.Success;
                }
            }
            catch (NotSupportedException e)
            {
                Console.Write("{0} Faill loading browser caught.", e.Message);
                SetErrorMessage("SivecCrawler", e.Message);
                return CrawlerStatus.Skipped;
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception caught.", e.Message);
                SetErrorMessage("SivecCrawler", e.Message);
                return CrawlerStatus.Error;
            }
        }

    }
}
