using System;

using KrakenMPSPConsole.Enums;

namespace KrakenMPSPConsole.Models
{
    public abstract class Crawler
    {
        public abstract CrawlerStatus Execute(out object result);
        public CrawlerError Error { get; protected set; }

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
    }
}