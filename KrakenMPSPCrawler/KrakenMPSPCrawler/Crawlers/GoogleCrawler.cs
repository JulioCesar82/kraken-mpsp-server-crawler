using System;

using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Crawlers
{
    public class GoogleCrawler : Crawler
    {
        private String url = "https://www.google.com/search?hl=pt&q=";
        private String search;

        public GoogleCrawler(String textFind)
        {
            search = textFind;
        }

        public override CrawlerStatus Execute()
        {
            return CrawlerStatus.Success;
        }
    }
}
