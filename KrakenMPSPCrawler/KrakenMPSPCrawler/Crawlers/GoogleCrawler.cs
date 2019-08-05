using IronWebScraper;
using System;

namespace KrakenMPSPCrawler.Crawlers
{
    public class GoogleCrawler : WebScraper
    {
        private String url = "https://www.google.com/search?hl=pt&q=";
        private String search;

        public GoogleCrawler(String textFind)
        {
            search = textFind;
        }

        public override void Init()
        {
            License.LicenseKey = "LicenseKey"; // Write License Key
            this.LoggingLevel = WebScraper.LogLevel.All; // All Events Are Logged
            this.Request(url + search, Parse);
        }

        public override void Parse(Response response)
        {
            this.WorkingDirectory = AppContext.BaseDirectory + @"\Output\";
            // Loop on all Links
            foreach (var title_link in response.Css(".rc .r h3"))
            {
                // Read Link Text
                string strTitle = title_link.TextContentClean;
                // Save Result to File
                Scrape(new ScrapedData() { { "Title", strTitle } }, "history-google.json");
            }
        }

    }
}
