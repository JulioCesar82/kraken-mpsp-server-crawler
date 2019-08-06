using System;

using KrakenMPSPCrawler.Business.Enum;
using KrakenMPSPCrawler.Business.Interface;

namespace KrakenMPSPCrawler.Business.Model
{
    public abstract class Crawler : ICrawler
    {
        public virtual CrawlerError Error { get; protected set; }

        public virtual CrawlerStatus Execute()
        {
            throw new NotImplementedException();
        }

        public virtual void SetErrorMessage(string errorMessage)
        {
            if (Error != null)
            {
                Error.Message = errorMessage;
                return;
            }

            Error = new CrawlerError(errorMessage);
        }

        public virtual void SetErrorMessage(Type type, string errorMessage)
        {
            Error = new CrawlerError(type, errorMessage);
        }

    }
}