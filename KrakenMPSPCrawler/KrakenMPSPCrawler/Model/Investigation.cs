using System.Collections.Generic;

namespace KrakenMPSPCrawler.Model
{
    public class Investigation
    {
        public bool Completed => Errors.Count == 0;

        public List<CrawlerError> Errors = new List<CrawlerError>();
        public List<object> Informations = new List<object>();

        public void AddError(CrawlerError error)
        {
            Errors.Add(error);
        }
        public void AddError(List<CrawlerError> errors)
        {
            Errors.AddRange(errors);
        }

        public void AddInformation(object information)
        {
            Informations.Add(information);
        }
        public void AddInformation(List<object> informations)
        {
            Informations.AddRange(informations);
        }
    }
}
