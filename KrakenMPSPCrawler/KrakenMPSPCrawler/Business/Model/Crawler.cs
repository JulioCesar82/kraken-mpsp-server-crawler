using System;

using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Interface;

namespace KrakenMPSPCrawler.Business.Model
{
    public abstract class Crawler : ICrawler
    {
        public abstract CrawlerStatus Execute();

        public CrawlerError Error { get; protected set; }
        public object InformationFound { get; protected set; }

        protected void SetErrorMessage(string errorMessage)
        {
            if (Error != null)
            {
                Error.Message = errorMessage;
                return;
            }

            Error = new CrawlerError(errorMessage);
        }

        protected void SetErrorMessage(Type source, string errorMessage)
        {
            Error = new CrawlerError(source, errorMessage);
        }

        protected void SetInformationFound(object information)
        {
            InformationFound = information;
        }
    }
}