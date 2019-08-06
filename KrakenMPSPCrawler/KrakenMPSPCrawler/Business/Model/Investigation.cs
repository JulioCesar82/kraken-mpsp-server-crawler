using System.Collections.Generic;

namespace KrakenMPSPCrawler.Business.Model
{
    public class Investigation
    {
        public bool Completed => Errors.Count == 0;
        public List<CrawlerError> Errors = new List<CrawlerError>();

        public void AddError(CrawlerError error)
        {
            Errors.Add(error);
        }
        public void AddError(List<CrawlerError> errors)
        {
            Errors.AddRange(errors);
        }
    }
}
