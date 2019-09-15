using System;
using System.Collections.Generic;

namespace KrakenMPSPCrawler.Business.Model
{
    public class Investigation
    {
        public bool Completed => Errors.Count == 0;

        public List<CrawlerError> Errors = new List<CrawlerError>();
        public List<Tuple<Type, object>> Informations = new List<Tuple<Type, object>>();

        public void AddError(CrawlerError error)
        {
            Errors.Add(error);
        }
        public void AddError(List<CrawlerError> errors)
        {
            Errors.AddRange(errors);
        }

        public void AddInformation(Tuple<Type, object> information)
        {
            Informations.Add(information);
        }
        public void AddInformation(List<Tuple<Type, object>> informations)
        {
            Informations.AddRange(informations);
        }
    }
}
