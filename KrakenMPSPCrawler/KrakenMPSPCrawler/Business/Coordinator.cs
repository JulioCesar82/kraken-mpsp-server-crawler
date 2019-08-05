using System;
using System.Collections.Generic;

using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Interface;
using KrakenMPSPCrawler.Business.Model;

namespace KrakenMPSPCrawler.Business
{
    class Coordinator : IInvestigation
    {
        private readonly List<Search> _searches = new List<Search>();

        public Search AddRule(Search validation)
        {
            _searches.Add(validation);
            return validation;
        }

        public Investigation AddSearch(Investigation validation)
        {
            throw new NotImplementedException();
        }

        public Investigation Run()
        {
            var result = new Investigation();
            return Run(result);
        }

        public Investigation Run(Investigation validationContext)
        {
            var result = validationContext ?? new Investigation();

            foreach (var search in _searches)
            {
                var searchResult = search.Completed();

                if (searchResult == SearchStatus.Success) continue;
                if (searchResult == SearchStatus.Skipped) break;

                result.AddError(search.Error);
            }

            return result;
        }
    }
}
