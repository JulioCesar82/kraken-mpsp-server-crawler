﻿using KrakenMPSPBusiness.Models;

using KrakenMPSPCrawler.Model;
using KrakenMPSPCrawler.Crawlers;

namespace KrakenMPSPCrawler
{
    public class LegalPersonCoordinator : Coordinator
    {
        public LegalPersonCoordinator(LegalPersonModel legalPerson)
        {
            // Classe de Crawler base, apenas duplique
            // AddModule(new ExampleCrawler("julio+cesar"));

            AddModule(new ArispCrawler(legalPerson.Type, legalPerson.CNPJ));
            AddModule(new CagedCrawler(legalPerson.Type, "fiap", "senha", legalPerson.CNPJ));
            AddModule(new CensecCrawler("fiap", "fiap123", legalPerson.CNPJ));
            AddModule(new DetranCrawler("12345678", "fiap123", legalPerson.CNPJ));
        }
    }
}
