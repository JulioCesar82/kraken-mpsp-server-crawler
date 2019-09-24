using System;

using KrakenMPSPBusiness.Enum;

namespace KrakenMPSPBusiness.Models
{
    public class ResourcesFoundModel
    {
        public Guid Id { get; set; }
        public Guid ArquivoReferencia { get; set; }
        public KindPerson Type { get; set; }
        public ArispCrawlerModel Arisp { get; set; }
        public ArpenspCrawlerModel Arpensp { get; set; }
        public SielCrawlerModel Siel { get; set; }
        public SivecCrawlerModel Sivec { get; set; }
        public CagedCrawlerModelPJ CagedPJ { get; set; }
        public CagedCrawlerModelPF CagedPF { get; set; }
        public CensecCrawlerModel Censesc { get; set; }
    }
}
