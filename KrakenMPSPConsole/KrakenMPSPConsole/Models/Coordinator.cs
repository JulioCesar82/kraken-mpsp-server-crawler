using System.Collections.Generic;

using KrakenMPSPConsole.Enums;

namespace KrakenMPSPConsole.Models
{
    public class Coordinator
    {
        private readonly List<CrawlerStatus> _crawlers = new List<CrawlerStatus>();
        public void AddModule(CrawlerStatus crawler)
        {
            _crawlers.Add(crawler);

            /*if (crawler.Error != null)
            {
                Errors.Add(crawler.Error);
            }*/
        }

        public List<CrawlerError> Errors = new List<CrawlerError>();
        public bool Completed => Errors.Count == 0;
    }
}
