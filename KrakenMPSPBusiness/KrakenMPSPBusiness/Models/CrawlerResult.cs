using System.Collections.Generic;

using KrakenMPSPBusiness.Enums;

namespace KrakenMPSPBusiness.Models
{
    public class CrawlerResult
    {
        public int FindTotal { get; set; }
        public List<CrawlerStatus> TotalErrors { get; set; }
        public bool Completed => TotalErrors.Count == 0;
    }
}
