using System.Collections.Generic;

using KrakenMPSPBusiness.Enums;

namespace KrakenMPSPBusiness.Models
{
    public abstract class Coordinator
    {
        protected readonly List<CrawlerStatus> _portais = new List<CrawlerStatus>();
        protected void AddModule(CrawlerStatus portal)
        {
            _portais.Add(portal);
        }
    }
}
