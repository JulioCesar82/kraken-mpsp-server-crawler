using System;
using System.Collections.Generic;

using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Interface;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Business
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
                };
            }

            return result;
        }
    }
}
