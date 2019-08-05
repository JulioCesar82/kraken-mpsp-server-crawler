using System.Collections.Generic;

using KrakenMPSPCrawler.Models;
using KrakenMPSPCrawler.Crawlers;
using KrakenMPSPCrawler.Business;
using KrakenMPSPCrawler.Business.Model;
using KrakenMPSPCrawler.Business.Interface;

namespace KrakenMPSPCrawler
{
    public class PhysicalPersonCoordinator : ISearch
    {
        private readonly List<Coordinator> _validations = new List<Coordinator>();

        public PhysicalPersonCoordinator(PhysicalPersonModel physicalPerson)
        {
            _validations.Add(new GoogleCrawler(physicalPerson.NomeCompleto));
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
