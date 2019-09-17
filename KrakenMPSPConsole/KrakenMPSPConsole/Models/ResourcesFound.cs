using System;
using KrakenMPSPCrawler.Models;

namespace KrakenMPSPConsole.Models
{
    public class ResourcesFound
    {
        public Guid Id { get; set; }
        public ArispCrawlerModel Arisp { get; set; }
        public ArpenspCrawlerModel Arpensp { get; set; }
        public SielCrawlerModel Siel { get; set; }
        public SivecCrawlerModel Sivec { get; set; }
    }
}
