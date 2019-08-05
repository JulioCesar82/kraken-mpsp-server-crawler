using System.Collections.Generic;

using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Crawlers;
using KrakenMPSPCrawler.Business;
using KrakenMPSPCrawler.Business.Model;
using KrakenMPSPCrawler.Business.Interface;

namespace KrakenMPSPCrawler
{
    public class LegalPersonCoordinator : ISearch
    {
        private readonly List<Coordinator> _validations = new List<Coordinator>();

        public LegalPersonCoordinator(LegalPersonModel legalPerson)
        {
            _validations.Add(new GoogleCrawler(legalPerson.NomeFantasia));
        }

        Investigation ISearch.Run()
        {
            var validationContext = new Investigation();

            foreach (var validation in _validations)
            {
                //validation.Run(validationContext);
                validation.Start();
            }

            return validationContext;
        }
    }
}
