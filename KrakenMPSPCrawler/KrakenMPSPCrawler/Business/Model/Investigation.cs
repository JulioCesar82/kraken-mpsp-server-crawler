using System.Collections.Generic;

namespace KrakenMPSPCrawler.Business.Model
{
    public class Investigation
    {
        public bool Completed => Errors.Count == 0;
        public List<SearchError> Errors = new List<SearchError>();

        public void AddError(SearchError error)
        {
            Errors.Add(error);
        }
        public void AddError(List<SearchError> errors)
        {
            Errors.AddRange(errors);
        }
    }
}
