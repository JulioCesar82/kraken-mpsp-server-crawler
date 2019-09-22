using System.Collections.Generic;

using KrakenMPSPCrawler.Enum;
using KrakenMPSPCrawler.Interface;

namespace KrakenMPSPCrawler.Model
{
    public class Coordinator : ICoordinator
    {
        private readonly List<Crawler> _crawlers = new List<Crawler>();

        public Crawler AddModule(Crawler validation)
        {
            _crawlers.Add(validation);
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

            foreach (var crawler in _crawlers)
            {
                var searchResult = crawler.Execute();

                if (searchResult == CrawlerStatus.Error)
                {
                    result.AddError(crawler.Error);
                    continue;
                };

                result.AddInformation(crawler.InformationFound);
            }

            return result;
        }
    }
}
