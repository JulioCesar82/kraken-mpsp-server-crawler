using System.Collections.Generic;

using KrakenMPSPBusiness.Enums;

namespace KrakenMPSPBusiness.Models
{
    public class CrawlerResult
    {
        public int FindTotal { get; set; }
        public int TotalErrors { get; set; }
        public bool Completed => TotalErrors == 0;
    }
}
