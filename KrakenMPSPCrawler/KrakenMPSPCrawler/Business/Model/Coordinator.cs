using System.Collections.Generic;

using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Interface;

namespace KrakenMPSPCrawler.Business.Model
{
    public class Coordinator : ICoordinator
    {
        private readonly List<Crawler> Crawlers = new List<Crawler>();

        public Crawler AddModule(Crawler validation)
        {
            Crawlers.Add(validation);
            return validation;
        }

        public Investigation Run()
        {
            var result = new Investigation();
            return Run(result);
        }

        public Investigation Run(Investigation validationContext)
        {
            var result = validationContext ?? new Investigation();

            foreach (var crawler in Crawlers)
            {
                var searchResult = crawler.Execute();

                if (searchResult == CrawlerStatus.Error)
                {
                    result.AddError(crawler.Error);
                };
            }

            return result;
        }
    }
}
