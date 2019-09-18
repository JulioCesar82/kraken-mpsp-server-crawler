using System;

using KrakenMPSPCrawler.Enum;
using KrakenMPSPCrawler.Models;

namespace KrakenMPSPConsole.Models
{
    public class ResourcesFound
    {
        public Guid Id { get; set; }
        public Guid ArquivoReferencia { get; set; }
        public KindPerson Type { get; set; }
        public ArispCrawlerModel Arisp { get; set; }
        public ArpenspCrawlerModel Arpensp { get; set; }
        public SielCrawlerModel Siel { get; set; }
        public SivecCrawlerModel Sivec { get; set; }
    }
}
